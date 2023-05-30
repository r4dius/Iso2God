using System;

namespace EnterpriseDT.Util.Debug;

public class LogMessageEventArgs : EventArgs
{
    private string loggerName;

    private Level level;

    private string text;

    private Exception e;

    private object[] args;

    public Level LogLevel => level;

    public string LoggerName => loggerName;

    public string Text => text;

    public string FormattedText
    {
        get
        {
            if (args != null)
            {
                return string.Format(text, args);
            }
            return text;
        }
    }

    public Exception Exception => e;

    public object[] Arguments => args;

    internal LogMessageEventArgs(string loggerName, Level level, string text, params object[] args)
    {
        this.loggerName = loggerName;
        this.level = level;
        this.text = text;
        this.args = args;
        if (args != null && args.Length == 1 && args[0] is Exception)
        {
            e = (Exception)args[0];
        }
    }
}
