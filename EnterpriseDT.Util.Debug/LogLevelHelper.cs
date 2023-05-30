namespace EnterpriseDT.Util.Debug;

public class LogLevelHelper
{
    public static LogLevel GetLogLevel(string level)
    {
        level = level.ToUpper();
        if (level == LogLevel.Off.ToString().ToUpper())
        {
            return LogLevel.Off;
        }
        if (level == LogLevel.Fatal.ToString().ToUpper())
        {
            return LogLevel.Fatal;
        }
        if (level == LogLevel.Error.ToString().ToUpper())
        {
            return LogLevel.Error;
        }
        if (level == LogLevel.Warning.ToString().ToUpper())
        {
            return LogLevel.Warning;
        }
        if (level == LogLevel.Information.ToString().ToUpper())
        {
            return LogLevel.Information;
        }
        if (level == LogLevel.Debug.ToString().ToUpper())
        {
            return LogLevel.Debug;
        }
        if (level == LogLevel.All.ToString().ToUpper())
        {
            return LogLevel.All;
        }
        return LogLevel.Information;
    }
}
