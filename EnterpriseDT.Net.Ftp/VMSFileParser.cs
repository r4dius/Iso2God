using System;
using System.ComponentModel;
using System.Text;
using EnterpriseDT.Util;
using EnterpriseDT.Util.Debug;

namespace EnterpriseDT.Net.Ftp;

public class VMSFileParser : FTPFileParser
{
	private const int DEFAULT_BLOCKSIZE = 524288;

	private static readonly string DIR = ".DIR";

	private static readonly string HDR = "Directory";

	private static readonly string TOTAL = "Total";

	private Logger log = Logger.GetLogger("VMSFileParser");

	private static readonly int MIN_EXPECTED_FIELD_COUNT = 4;

	private bool versionInName = false;

	private int blocksize = 524288;

	public override bool TimeIncludesSeconds => false;

	[DefaultValue(false)]
	public bool VersionInName
	{
		get
		{
			return versionInName;
		}
		set
		{
			versionInName = value;
		}
	}

	[DefaultValue(524288)]
	public int BlockSize
	{
		get
		{
			return blocksize;
		}
		set
		{
			blocksize = value;
		}
	}

	public override bool IsMultiLine()
	{
		return true;
	}

	public override string ToString()
	{
		return "VMS";
	}

	public override bool IsValidFormat(string[] listing)
	{
		int num = Math.Min(listing.Length, 10);
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		for (int i = 0; i < num; i++)
		{
			if (listing[i].Trim().Length != 0)
			{
				int num2 = 0;
				if ((num2 = listing[i].IndexOf(';')) > 0 && ++num2 < listing[i].Length && char.IsDigit(listing[i][num2]))
				{
					flag = true;
				}
				if (listing[i].IndexOf(',') > 0)
				{
					flag2 = true;
				}
				if (listing[i].IndexOf('[') > 0)
				{
					flag3 = true;
				}
				if (listing[i].IndexOf(']') > 0)
				{
					flag4 = true;
				}
			}
		}
		if (flag && flag2 && flag3 && flag4)
		{
			return true;
		}
		log.Debug("Not in VMS format");
		return false;
	}

	public override FTPFile Parse(string raw)
	{
		string[] array = StringSplitter.Split(raw);
		if (array.Length <= 0)
		{
			return null;
		}
		if (array.Length >= 2 && array[0].Equals(HDR))
		{
			return null;
		}
		if (array.Length > 0 && array[0].Equals(TOTAL))
		{
			return null;
		}
		if (array.Length < MIN_EXPECTED_FIELD_COUNT)
		{
			return null;
		}
		string text = array[0];
		int num = text.LastIndexOf(';');
		if (num <= 0)
		{
			log.Warn("File version number not found in name '" + text + "'");
			return null;
		}
		string text2 = text.Substring(0, num);
		string s = array[0].Substring(num + 1);
		try
		{
			long.Parse(s);
		}
		catch (FormatException)
		{
		}
		bool flag = false;
		if (text2.EndsWith(DIR))
		{
			flag = true;
			text = text2.Substring(0, text2.Length - DIR.Length);
		}
		if (!versionInName && !flag)
		{
			text = text2;
		}
		int num2 = array[1].IndexOf('/');
		string s2 = array[1];
		if (num2 > 0)
		{
			s2 = array[1].Substring(0, num2);
		}
		long size = long.Parse(s2) * blocksize;
		string text3 = TweakDateString(array);
		DateTime lastModifiedTime = DateTime.MinValue;
		try
		{
			lastModifiedTime = DateTime.Parse(text3.ToString(), base.ParsingCulture.DateTimeFormat);
		}
		catch (FormatException)
		{
			log.Warn("Failed to parse date string '" + text3 + "'");
		}
		string group = null;
		string owner = null;
		if (array.Length >= 5 && array[4][0] == '[' && array[4][array[4].Length - 1] == ']')
		{
			int num3 = array[4].IndexOf(',');
			if (num3 < 0)
			{
				owner = array[4].Substring(1, array[4].Length - 2);
				group = "";
			}
			else
			{
				group = array[4].Substring(1, num3 - 1);
				owner = array[4].Substring(num3 + 1, array[4].Length - num3 - 2);
			}
		}
		string permissions = null;
		if (array.Length >= 6 && array[5][0] == '(' && array[5][array[5].Length - 1] == ')')
		{
			permissions = array[5].Substring(1, array[5].Length - 2);
		}
		FTPFile fTPFile = new FTPFile(2, raw, text, size, flag, ref lastModifiedTime);
		fTPFile.Group = group;
		fTPFile.Owner = owner;
		fTPFile.Permissions = permissions;
		return fTPFile;
	}

	private string TweakDateString(string[] fields)
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = false;
		for (int i = 0; i < fields[2].Length; i++)
		{
			if (!char.IsLetter(fields[2][i]))
			{
				stringBuilder.Append(fields[2][i]);
			}
			else if (!flag)
			{
				stringBuilder.Append(fields[2][i]);
				flag = true;
			}
			else
			{
				stringBuilder.Append(char.ToLower(fields[2][i]));
			}
		}
		stringBuilder.Append(" ").Append(fields[3]);
		return stringBuilder.ToString();
	}
}
