using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using EnterpriseDT.Util.Debug;

namespace EnterpriseDT.Net.Ftp;

public class FTPFileFactory
{
	internal const string WINDOWS_STR = "WINDOWS";

	internal const string UNIX_STR = "UNIX";

	internal const string VMS_STR = "VMS";

	internal const string OS400_STR = "OS/400";

	private Logger log = Logger.GetLogger("FTPFileFactory");

	private string system;

	private WindowsFileParser windows = new WindowsFileParser();

	private UnixFileParser unix = new UnixFileParser();

	private VMSFileParser vms = new VMSFileParser();

	private OS400FileParser os400 = new OS400FileParser();

	private FTPFileParser parser = null;

	private ArrayList parsers = new ArrayList();

	private bool userSetParser = false;

	private bool parserDetected = false;

	private CultureInfo parserCulture = CultureInfo.InvariantCulture;

	protected TimeSpan timeDiff = default(TimeSpan);

	public CultureInfo ParsingCulture
	{
		get
		{
			return parserCulture;
		}
		set
		{
			parserCulture = value;
			windows.ParsingCulture = value;
			unix.ParsingCulture = value;
			vms.ParsingCulture = value;
			os400.ParsingCulture = value;
			if (parser != null)
			{
				parser.ParsingCulture = value;
			}
		}
	}

	public bool ParserSetExplicitly => userSetParser;

	[DefaultValue(null)]
	public FTPFileParser FileParser
	{
		get
		{
			return parser;
		}
		set
		{
			parser = value;
			if (value != null)
			{
				userSetParser = true;
				return;
			}
			userSetParser = false;
			SetParser(system);
		}
	}

	public VMSFileParser VMSParser => vms;

	public TimeSpan TimeDifference
	{
		get
		{
			return timeDiff;
		}
		set
		{
			timeDiff = value;
		}
	}

	public bool TimeIncludesSeconds
	{
		get
		{
			if (parser != null)
			{
				return parser.TimeIncludesSeconds;
			}
			return false;
		}
	}

	[DefaultValue(null)]
	public string System
	{
		get
		{
			return system;
		}
		set
		{
			SetParser(value);
		}
	}

	public FTPFileFactory(string system)
	{
		InitializeParsers();
		SetParser(system);
	}

	public FTPFileFactory(FTPFileParser parser)
	{
		InitializeParsers();
		this.parser = parser;
	}

	private void InitializeParsers()
	{
		parsers.Add(unix);
		parsers.Add(windows);
		parsers.Add(os400);
		parsers.Add(vms);
		parser = unix;
	}

	public FTPFileFactory()
	{
		InitializeParsers();
	}

	public void AddParser(FTPFileParser parser)
	{
		parsers.Add(parser);
	}

	public void SetParser(string system)
	{
		parserDetected = false;
		this.system = system?.Trim();
		if (system != null)
		{
			if (system.ToUpper().StartsWith("WINDOWS"))
			{
				log.Debug("Selected Windows parser");
				parser = windows;
			}
			else if (system.ToUpper().IndexOf("UNIX") >= 0)
			{
				log.Debug("Selected UNIX parser");
				parser = unix;
			}
			else if (system.ToUpper().IndexOf("VMS") >= 0)
			{
				log.Debug("Selected VMS parser");
				parser = vms;
			}
			else if (system.ToUpper().IndexOf("OS/400") >= 0)
			{
				log.Debug("Selected OS/400 parser");
				parser = os400;
			}
			else
			{
				parser = unix;
				log.Warn("Unknown SYST '" + system + "' - defaulting to Unix parsing");
			}
		}
		else
		{
			parser = unix;
			log.Debug("Defaulting to Unix parsing");
		}
	}

	private void DetectParser(string[] files)
	{
		if (parser.IsValidFormat(files))
		{
			log.Debug("Confirmed format " + parser.ToString());
			parserDetected = true;
			return;
		}
		IEnumerator enumerator = parsers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			FTPFileParser fTPFileParser = (FTPFileParser)enumerator.Current;
			if (fTPFileParser.IsValidFormat(files))
			{
				parser = fTPFileParser;
				log.Debug("Detected format " + parser.ToString());
				parserDetected = true;
				return;
			}
		}
		parser = unix;
		log.Warn("Could not detect format. Using default " + parser.ToString());
	}

	public virtual FTPFile[] Parse(string[] fileStrings)
	{
		log.Debug("Parse() called using culture: " + parserCulture.EnglishName);
		FTPFile[] array = new FTPFile[fileStrings.Length];
		if (fileStrings.Length == 0)
		{
			return array;
		}
		if (!userSetParser && !parserDetected)
		{
			DetectParser(fileStrings);
		}
		int num = 0;
		for (int i = 0; i < fileStrings.Length; i++)
		{
			if (fileStrings[i] == null || fileStrings[i].Trim().Length == 0)
			{
				continue;
			}
			try
			{
				FTPFile fTPFile = null;
				if (parser.IsMultiLine())
				{
					StringBuilder stringBuilder = new StringBuilder(fileStrings[i]);
					for (; i + 1 < fileStrings.Length && fileStrings[i + 1].IndexOf(';') < 0; i++)
					{
						stringBuilder.Append(" ").Append(fileStrings[i + 1]);
					}
					fTPFile = parser.Parse(stringBuilder.ToString());
				}
				else
				{
					fTPFile = parser.Parse(fileStrings[i]);
				}
				if (fTPFile != null)
				{
					if (timeDiff.Ticks != 0)
					{
						fTPFile.ApplyTimeDifference(timeDiff);
					}
					array[num++] = fTPFile;
				}
			}
			catch (RestartParsingException)
			{
				log.Debug("Restarting parsing from first entry in list");
				i = -1;
				num = 0;
			}
		}
		FTPFile[] array2 = new FTPFile[num];
		Array.Copy(array, 0, array2, 0, num);
		return array2;
	}

	[Obsolete("Use the System property.")]
	public string GetSystem()
	{
		return system;
	}
}
