using System.Globalization;

namespace EnterpriseDT.Net.Ftp;

public abstract class FTPFileParser
{
	private CultureInfo parserCulture = CultureInfo.InvariantCulture;

	public CultureInfo ParsingCulture
	{
		get
		{
			return parserCulture;
		}
		set
		{
			parserCulture = value;
		}
	}

	public abstract bool TimeIncludesSeconds { get; }

	public virtual bool IsValidFormat(string[] listing)
	{
		return false;
	}

	public virtual bool IsMultiLine()
	{
		return false;
	}

	public abstract FTPFile Parse(string raw);
}
