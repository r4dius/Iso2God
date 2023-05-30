namespace EnterpriseDT.Util.Debug;

public class Level
{
	private const string OFF_STR = "OFF";

	private const string FATAL_STR = "FATAL";

	private const string ERROR_STR = "ERROR";

	private const string WARN_STR = "WARN";

	private const string INFO_STR = "INFO";

	private const string DEBUG_STR = "DEBUG";

	private const string ALL_STR = "ALL";

	public static Level OFF = new Level(LogLevel.Off, "OFF");

	public static Level FATAL = new Level(LogLevel.Fatal, "FATAL");

	public static Level ERROR = new Level(LogLevel.Error, "ERROR");

	public static Level WARN = new Level(LogLevel.Warning, "WARN");

	public static Level INFO = new Level(LogLevel.Information, "INFO");

	public static Level DEBUG = new Level(LogLevel.Debug, "DEBUG");

	public static Level ALL = new Level(LogLevel.All, "ALL");

	private LogLevel level = LogLevel.Off;

	private string levelStr;

	private Level(LogLevel level, string levelStr)
	{
		this.level = level;
		this.levelStr = levelStr;
	}

	public LogLevel GetLevel()
	{
		return level;
	}

	public bool IsGreaterOrEqual(Level l)
	{
		if (level >= l.level)
		{
			return true;
		}
		return false;
	}

	public static Level GetLevel(string level)
	{
		return level.ToUpper() switch
		{
			"OFF" => OFF, 
			"FATAL" => FATAL, 
			"ERROR" => ERROR, 
			"WARN" => WARN, 
			"INFO" => INFO, 
			"DEBUG" => DEBUG, 
			"ALL" => ALL, 
			_ => null, 
		};
	}

	public static Level GetLevel(LogLevel level)
	{
		return level switch
		{
			LogLevel.Off => OFF, 
			LogLevel.Fatal => FATAL, 
			LogLevel.Error => ERROR, 
			LogLevel.Warning => WARN, 
			LogLevel.Information => INFO, 
			LogLevel.Debug => DEBUG, 
			LogLevel.All => ALL, 
			_ => OFF, 
		};
	}

	public override string ToString()
	{
		return levelStr;
	}
}
