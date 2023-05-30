using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace EnterpriseDT.Util.Debug;

public class Logger
{
    private static Level globalLevel;

    private static readonly string format;

    private static readonly string LEVEL_PARAM;

    private static Hashtable loggers;

    private static ArrayList appenders;

    private DateTime ts;

    private string clazz;

    private static bool showClassNames;

    private static bool showTimestamp;

    private static FileAppender mainFileAppender;

    private static StandardOutputAppender mainConsoleAppender;

    private static TraceAppender mainTraceAppender;

    public static Level CurrentLevel
    {
        get
        {
            return globalLevel;
        }
        set
        {
            globalLevel = value;
        }
    }

    public static bool ShowClassNames
    {
        get
        {
            return showClassNames;
        }
        set
        {
            showClassNames = value;
        }
    }

    public static bool ShowTimestamp
    {
        get
        {
            return showTimestamp;
        }
        set
        {
            showTimestamp = value;
        }
    }

    public virtual bool DebugEnabled => IsEnabledFor(Level.DEBUG);

    public virtual bool InfoEnabled => IsEnabledFor(Level.INFO);

    public static string PrimaryLogFile
    {
        get
        {
            if (mainFileAppender == null)
            {
                return null;
            }
            return mainFileAppender.FileName;
        }
        set
        {
            string text = ((mainFileAppender != null) ? mainFileAppender.FileName : null);
            if (text != value)
            {
                if (mainFileAppender != null)
                {
                    RemoveAppender(mainFileAppender);
                }
                if (value != null)
                {
                    AddAppender(new FileAppender(value));
                }
            }
        }
    }

    public static bool LogToConsole
    {
        get
        {
            return mainConsoleAppender != null;
        }
        set
        {
            if (value)
            {
                if (mainConsoleAppender == null)
                {
                    AddAppender(new StandardOutputAppender());
                }
            }
            else if (mainConsoleAppender != null)
            {
                RemoveAppender(mainConsoleAppender);
            }
        }
    }

    public static bool LogToTrace
    {
        get
        {
            return mainTraceAppender != null;
        }
        set
        {
            if (value)
            {
                if (mainTraceAppender == null)
                {
                    AddAppender(new TraceAppender());
                }
            }
            else if (mainTraceAppender != null)
            {
                RemoveAppender(mainTraceAppender);
            }
        }
    }

    public static event LogMessageHandler LogMessageReceived;

    private Logger(string clazz)
    {
        this.clazz = clazz;
    }

    public static Logger GetLogger(Type clazz)
    {
        return GetLogger(clazz.FullName);
    }

    public static Logger GetLogger(string clazz)
    {
        Logger logger = (Logger)loggers[clazz];
        if (logger == null)
        {
            logger = new Logger(clazz);
            loggers[clazz] = logger;
        }
        return logger;
    }

    public static void AddAppender(Appender newAppender)
    {
        appenders.Add(newAppender);
        if (newAppender is FileAppender && mainFileAppender == null)
        {
            mainFileAppender = (FileAppender)newAppender;
        }
        if (newAppender is StandardOutputAppender && mainConsoleAppender == null)
        {
            mainConsoleAppender = (StandardOutputAppender)newAppender;
        }
        if (newAppender is TraceAppender && mainTraceAppender == null)
        {
            mainTraceAppender = (TraceAppender)newAppender;
        }
    }

    public static void RemoveAppender(Appender appender)
    {
        appenders.Remove(appender);
        if (appender == mainFileAppender)
        {
            mainFileAppender = null;
        }
        if (appender == mainConsoleAppender)
        {
            mainConsoleAppender = null;
        }
        if (appender == mainTraceAppender)
        {
            mainTraceAppender = null;
        }
    }

    public static void Shutdown()
    {
        ClearAppenders();
        loggers.Clear();
    }

    public static void ClearAppenders()
    {
        lock (appenders.SyncRoot)
        {
            for (int i = 0; i < appenders.Count; i++)
            {
                Appender appender = (Appender)appenders[i];
                try
                {
                    appender.Close();
                }
                catch (Exception)
                {
                }
            }
        }
        appenders.Clear();
    }

    public virtual void Log(Level level, string message, params object[] args)
    {
        if (Logger.LogMessageReceived != null)
        {
            Logger.LogMessageReceived(this, new LogMessageEventArgs(clazz, level, message, args));
        }
        if (IsEnabledFor(level))
        {
            if (args != null && args.Length == 1 && args[0] is Exception)
            {
                OurLog(level, message, (Exception)args[0]);
            }
            else if (args != null)
            {
                OurLog(level, string.Format(message, args), null);
            }
            else
            {
                OurLog(level, message, null);
            }
        }
    }

    private void OurLog(Level level, string message, Exception t)
    {
        ts = DateTime.Now;
        string value = ts.ToString(format, CultureInfo.CurrentCulture.DateTimeFormat);
        StringBuilder stringBuilder = new StringBuilder(level.ToString());
        if (showClassNames)
        {
            stringBuilder.Append(" [");
            stringBuilder.Append(clazz);
            stringBuilder.Append("]");
        }
        if (showTimestamp)
        {
            stringBuilder.Append(" ");
            stringBuilder.Append(value);
        }
        stringBuilder.Append(" : ");
        string text = stringBuilder.ToString();
        stringBuilder.Append(message);
        if (t != null)
        {
            stringBuilder.Append(" : ").Append(((object)t).GetType().FullName).Append(": ")
                .Append(t.Message);
        }
        if (appenders.Count == 0)
        {
            Console.Out.WriteLine(stringBuilder.ToString());
            if (t == null)
            {
                return;
            }
            if (t.StackTrace != null)
            {
                string[] array = t.StackTrace.Replace("\r", "").Split('\n');
                foreach (string text2 in array)
                {
                    OurLog(level, text + text2, null);
                }
            }
            if (t.InnerException == null)
            {
                return;
            }
            Console.Out.WriteLine($"{text}CAUSED BY - {((object)t.InnerException).GetType().FullName}: {t.InnerException.Message}");
            if (t.InnerException.StackTrace != null)
            {
                string[] array = t.InnerException.StackTrace.Replace("\r", "").Split('\n');
                foreach (string text3 in array)
                {
                    OurLog(level, text + text3, null);
                }
            }
            return;
        }
        bool flag = globalLevel.IsGreaterOrEqual(level);
        lock (appenders.SyncRoot)
        {
            for (int j = 0; j < appenders.Count; j++)
            {
                Appender appender = (Appender)appenders[j];
                bool flag2 = false;
                if (appender is CustomLogLevelAppender)
                {
                    CustomLogLevelAppender customLogLevelAppender = (CustomLogLevelAppender)appender;
                    flag2 = customLogLevelAppender.CurrentLevel.IsGreaterOrEqual(level);
                }
                if (!flag && !flag2)
                {
                    continue;
                }
                appender.Log(text + message);
                if (t == null)
                {
                    continue;
                }
                appender.Log(text + ((object)t).GetType().FullName + ": " + t.Message);
                if (t.StackTrace != null)
                {
                    string[] array = t.StackTrace.Replace("\r", "").Split('\n');
                    foreach (string text4 in array)
                    {
                        appender.Log(text + text4);
                    }
                }
                if (t.InnerException == null)
                {
                    continue;
                }
                appender.Log(text + "CAUSED BY - " + ((object)t.InnerException).GetType().FullName + ": " + t.Message);
                if (t.InnerException.StackTrace != null)
                {
                    string[] array = t.InnerException.StackTrace.Replace("\r", "").Split('\n');
                    foreach (string text5 in array)
                    {
                        appender.Log(text + text5);
                    }
                }
            }
        }
    }

    public virtual void Info(string message)
    {
        Log(Level.INFO, message, null);
    }

    public virtual void Info(string message, Exception t)
    {
        Log(Level.INFO, message, t);
    }

    public virtual void Info(string message, params object[] args)
    {
        if (IsEnabledFor(Level.INFO))
        {
            Log(Level.INFO, string.Format(message, args), null);
        }
    }

    public virtual void Warn(string message)
    {
        Log(Level.WARN, message, null);
    }

    public virtual void Warn(string message, Exception t)
    {
        Log(Level.WARN, message, t);
    }

    public virtual void Error(string message)
    {
        Log(Level.ERROR, message, null);
    }

    public virtual void Error(string message, Exception t)
    {
        Log(Level.ERROR, message, t);
    }

    public virtual void Error(string message, Exception t, params object[] args)
    {
        Log(Level.ERROR, string.Format(message, args), t);
    }

    public virtual void Error(Exception t)
    {
        Log(Level.ERROR, t.Message, t);
    }

    public virtual void Fatal(string message)
    {
        Log(Level.FATAL, message, null);
    }

    public virtual void Fatal(string message, Exception t)
    {
        Log(Level.FATAL, message, t);
    }

    public virtual void Debug(string message)
    {
        Log(Level.DEBUG, message, null);
    }

    public virtual void Debug(string message, params object[] args)
    {
        if (IsEnabledFor(Level.DEBUG))
        {
            Log(Level.DEBUG, string.Format(message, args), null);
        }
    }

    public virtual void Debug(string message, Exception t)
    {
        Log(Level.DEBUG, message, t);
    }

    public virtual bool IsEnabledFor(Level level)
    {
        if (globalLevel.IsGreaterOrEqual(level))
        {
            return true;
        }
        lock (appenders.SyncRoot)
        {
            foreach (Appender appender in appenders)
            {
                if (appender is CustomLogLevelAppender)
                {
                    CustomLogLevelAppender customLogLevelAppender = (CustomLogLevelAppender)appender;
                    if (customLogLevelAppender.CurrentLevel.IsGreaterOrEqual(level))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    static Logger()
    {
        format = "d MMM yyyy HH:mm:ss.fff";
        LEVEL_PARAM = "edtftp.log.level";
        loggers = Hashtable.Synchronized(new Hashtable(10));
        appenders = ArrayList.Synchronized(new ArrayList(2));
        showClassNames = true;
        showTimestamp = true;
        mainFileAppender = null;
        mainConsoleAppender = null;
        mainTraceAppender = null;
        globalLevel = null;
        string text = null;
        try
        {
            text = ConfigurationSettings.AppSettings[LEVEL_PARAM];
        }
        catch (Exception ex)
        {
            Console.WriteLine("WARN: Failure reading configuration file: " + ex.Message);
        }
        if (text != null)
        {
            globalLevel = Level.GetLevel(text);
            if (globalLevel == null)
            {
                try
                {
                    LogLevel level = (LogLevel)Enum.Parse(typeof(LogLevel), text, ignoreCase: true);
                    globalLevel = Level.GetLevel(level);
                }
                catch (Exception)
                {
                }
            }
        }
        if (globalLevel == null)
        {
            globalLevel = Level.OFF;
            if (text != null)
            {
                Console.Out.WriteLine("WARN: '" + LEVEL_PARAM + "' configuration property invalid. Unable to parse '" + text + "' - logging switched off");
            }
        }
    }

    public void LogObject(Level level, string prefix, object obj)
    {
        if (!IsEnabledFor(level))
        {
            return;
        }
        if (obj == null)
        {
            Log(level, prefix + "(null)", null);
        }
        obj.GetType();
        bool flag = true;
        PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        PropertyInfo[] array = properties;
        foreach (PropertyInfo p in array)
        {
            if (RequiresLongFormat(p, obj))
            {
                flag = false;
                break;
            }
        }
        StringBuilder stringBuilder = new StringBuilder();
        array = properties;
        foreach (PropertyInfo propertyInfo in array)
        {
            object value = propertyInfo.GetValue(obj, null);
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(flag ? ", " : "\n  ");
            }
            stringBuilder.Append(propertyInfo.Name).Append("=");
            DumpValue(value, stringBuilder, "    ");
        }
        Log(level, prefix + stringBuilder, null);
    }

    private ArrayList GetAllProperties(Type t)
    {
        ArrayList arrayList = new ArrayList();
        while (t != typeof(object))
        {
            arrayList.AddRange(t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
            t = t.BaseType;
        }
        return arrayList;
    }

    private bool RequiresLongFormat(PropertyInfo p, object obj)
    {
        object value = p.GetValue(obj, null);
        if (value == null || value is string || value.GetType().IsPrimitive)
        {
            return false;
        }
        if (value.GetType().IsArray && value.GetType().GetElementType().IsPrimitive)
        {
            return ((Array)value).Length > 16;
        }
        if (value is StringDictionary)
        {
            return ((StringDictionary)value).Count > 1;
        }
        if (value is ICollection)
        {
            return ((ICollection)value).Count > 1;
        }
        return typeof(IEnumerable).IsAssignableFrom(p.PropertyType);
    }

    private void DumpValue(object value, StringBuilder valueStr, string indent)
    {
        if (value == null)
        {
            valueStr.Append("(null)");
            return;
        }
        if (value.GetType().IsArray && value.GetType().GetElementType().IsPrimitive)
        {
            int num = 0;
            Array array = (Array)value;
            if (array.Length > 16)
            {
                valueStr.Append("(").Append(array.Length).Append(" items)")
                    .Append("\n")
                    .Append(indent)
                    .Append("  ");
            }
            {
                foreach (object item in array)
                {
                    if (item is byte)
                    {
                        byte b = (byte)item;
                        valueStr.Append(b.ToString("X2"));
                    }
                    else
                    {
                        valueStr.Append(item);
                    }
                    num++;
                    if (num % 16 != 0)
                    {
                        valueStr.Append(" ");
                    }
                    else
                    {
                        valueStr.Append("\n").Append(indent).Append("  ");
                    }
                }
                return;
            }
        }
        if (value is IDictionary)
        {
            IDictionary dictionary = (IDictionary)value;
            bool flag = dictionary.Count > 1;
            if (flag)
            {
                valueStr.Append("(").Append(dictionary.Count).Append(" items)");
            }
            {
                foreach (object key in dictionary.Keys)
                {
                    if (flag)
                    {
                        valueStr.Append("\n").Append(indent);
                    }
                    valueStr.Append(key).Append("=");
                    DumpValue(dictionary[key], valueStr, indent + "  ");
                }
                return;
            }
        }
        if (value is StringDictionary)
        {
            StringDictionary stringDictionary = (StringDictionary)value;
            bool flag2 = stringDictionary.Count > 1;
            if (flag2)
            {
                valueStr.Append("(").Append(stringDictionary.Count).Append(" items)");
            }
            {
                foreach (string key2 in stringDictionary.Keys)
                {
                    if (flag2)
                    {
                        valueStr.Append("\n").Append(indent);
                    }
                    valueStr.Append(key2).Append("=");
                    DumpValue(stringDictionary[key2], valueStr, indent + "  ");
                }
                return;
            }
        }
        if (!(value is string) && value is IEnumerable)
        {
            bool flag3 = true;
            if (value is ICollection)
            {
                ICollection collection = (ICollection)value;
                flag3 = collection.Count > 1;
                if (flag3)
                {
                    valueStr.Append("(").Append(collection.Count).Append(" items)");
                }
            }
            {
                foreach (object item2 in (IEnumerable)value)
                {
                    if (flag3)
                    {
                        valueStr.Append("\n").Append(indent);
                    }
                    DumpValue(item2, valueStr, indent + "  ");
                }
                return;
            }
        }
        valueStr.Append(value.ToString());
    }
}
