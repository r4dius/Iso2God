using System;
using System.Globalization;
using System.Text;
using EnterpriseDT.Util;
using EnterpriseDT.Util.Debug;

namespace EnterpriseDT.Net.Ftp;

public class UnixFileParser : FTPFileParser
{
	private const string SYMLINK_ARROW = "->";

	private const char SYMLINK_CHAR = 'l';

	private const char ORDINARY_FILE_CHAR = '-';

	private const char DIRECTORY_CHAR = 'd';

	private const string format1a = "MMM'-'d'-'yyyy";

	private const string format1b = "MMM'-'dd'-'yyyy";

	private const string format2a = "MMM'-'d'-'yyyy'-'HH':'mm";

	private const string format2b = "MMM'-'dd'-'yyyy'-'HH':'mm";

	private const string format2c = "MMM'-'d'-'yyyy'-'H':'mm";

	private const string format2d = "MMM'-'dd'-'yyyy'-'H':'mm";

	private const int MIN_FIELD_COUNT = 8;

	private string[] format1 = new string[2] { "MMM'-'d'-'yyyy", "MMM'-'dd'-'yyyy" };

	private string[] format2 = new string[4] { "MMM'-'d'-'yyyy'-'HH':'mm", "MMM'-'dd'-'yyyy'-'HH':'mm", "MMM'-'d'-'yyyy'-'H':'mm", "MMM'-'dd'-'yyyy'-'H':'mm" };

	private Logger log;

	public override bool TimeIncludesSeconds => false;

	public UnixFileParser()
	{
		log = Logger.GetLogger("UnixFileParser");
	}

	public override string ToString()
	{
		return "UNIX";
	}

	public override bool IsValidFormat(string[] listing)
	{
		int num = Math.Min(listing.Length, 10);
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < num; i++)
		{
			if (listing[i].Trim().Length == 0)
			{
				continue;
			}
			string[] array = StringSplitter.Split(listing[i]);
			if (array.Length >= 8)
			{
				char c = char.ToLower(array[0][0]);
				if (c == '-' || c == 'l' || c == 'd')
				{
					flag = true;
				}
				char c2 = char.ToLower(array[0][1]);
				if (c2 == 'r' || c2 == '-')
				{
					flag2 = true;
				}
				if (!flag2 && array[0].IndexOf('-', 2) > 0)
				{
					flag2 = true;
				}
			}
		}
		if (flag && flag2)
		{
			return true;
		}
		log.Debug("Not in UNIX format");
		return false;
	}

	public static bool IsUnix(string raw)
	{
		char c = raw[0];
		if (c == '-' || c == 'd' || c == 'l')
		{
			return true;
		}
		return false;
	}

	private bool IsNumeric(string field)
	{
		for (int i = 0; i < field.Length; i++)
		{
			if (!char.IsDigit(field[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override FTPFile Parse(string raw)
	{
		char c = raw[0];
		if (c != '-' && c != 'd' && c != 'l')
		{
			return null;
		}
		string[] array = StringSplitter.Split(raw);
		if (array.Length < 8)
		{
			StringBuilder stringBuilder = new StringBuilder("Unexpected number of fields in listing '");
			stringBuilder.Append(raw).Append("' - expected minimum ").Append(8)
				.Append(" fields but found ")
				.Append(array.Length)
				.Append(" fields");
			log.Warn(stringBuilder.ToString());
			return null;
		}
		int num = 0;
		string text = array[num++];
		c = text[0];
		bool isDir = false;
		bool flag = false;
		switch (c)
		{
		case 'd':
			isDir = true;
			break;
		case 'l':
			flag = true;
			break;
		}
		int linkCount = 0;
		if (char.IsDigit(array[num][0]))
		{
			string text2 = array[num++];
			try
			{
				linkCount = int.Parse(text2);
			}
			catch (FormatException)
			{
				log.Warn("Failed to parse link count: " + text2);
			}
		}
		else if (array[num][0] == '-')
		{
			num++;
		}
		string text3 = array[num++];
		string text4 = array[num++];
		long size = 0L;
		string text5 = array[num];
		if (!char.IsDigit(text5[0]) && char.IsDigit(text4[0]))
		{
			text5 = text4;
			text4 = text3;
			text3 = "";
		}
		else
		{
			num++;
		}
		try
		{
			size = long.Parse(text5);
		}
		catch (FormatException)
		{
			log.Warn("Failed to parse size: " + text5);
		}
		int num2 = -1;
		if (IsNumeric(array[num]))
		{
			if (array.Length - num < 5)
			{
				try
				{
					char[] trimChars = new char[1] { '0' };
					array[num].TrimStart(trimChars);
					num2 = int.Parse(array[num]);
					if (num2 > 31)
					{
						num2 = -1;
					}
				}
				catch (FormatException)
				{
				}
			}
			num++;
		}
		int num3 = num;
		DateTime lastModifiedTime = DateTime.MinValue;
		StringBuilder stringBuilder2 = new StringBuilder(array[num++]);
		stringBuilder2.Append('-');
		if (num2 > 0)
		{
			stringBuilder2.Append(num2);
		}
		else
		{
			stringBuilder2.Append(array[num++]);
		}
		stringBuilder2.Append('-');
		string text6 = array[num++];
		if (text6.IndexOf(':') < 0)
		{
			stringBuilder2.Append(text6);
			try
			{
				lastModifiedTime = DateTime.ParseExact(stringBuilder2.ToString(), format1, base.ParsingCulture.DateTimeFormat, DateTimeStyles.None);
			}
			catch (FormatException)
			{
				log.Warn("Failed to parse date string '" + stringBuilder2.ToString() + "'");
			}
		}
		else
		{
			int year = base.ParsingCulture.Calendar.GetYear(DateTime.Now);
			stringBuilder2.Append(year).Append('-').Append(text6);
			try
			{
				lastModifiedTime = DateTime.ParseExact(stringBuilder2.ToString(), format2, base.ParsingCulture.DateTimeFormat, DateTimeStyles.None);
			}
			catch (FormatException)
			{
				log.Warn("Failed to parse date string '" + stringBuilder2.ToString() + "'");
			}
			if (lastModifiedTime > DateTime.Now.AddDays(2.0))
			{
				lastModifiedTime = lastModifiedTime.AddYears(-1);
			}
		}
		string name = null;
		string linkedName = null;
		int num4 = 0;
		bool flag2 = true;
		int num5 = ((num2 > 0) ? 2 : 3);
		for (int i = num3; i < num3 + num5; i++)
		{
			num4 = raw.IndexOf(array[i], num4);
			if (num4 < 0)
			{
				flag2 = false;
				break;
			}
			num4 += array[i].Length;
		}
		if (flag2)
		{
			string text7 = raw.Substring(num4).Trim();
			if (!flag)
			{
				name = text7;
			}
			else
			{
				num4 = text7.IndexOf("->");
				if (num4 <= 0)
				{
					name = text7;
				}
				else
				{
					int length = "->".Length;
					name = text7.Substring(0, num4).Trim();
					if (num4 + length < text7.Length)
					{
						linkedName = text7.Substring(num4 + length);
					}
				}
			}
		}
		else
		{
			log.Warn("Failed to retrieve name: " + raw);
		}
		FTPFile fTPFile = new FTPFile(1, raw, name, size, isDir, ref lastModifiedTime);
		fTPFile.Group = text4;
		fTPFile.Owner = text3;
		fTPFile.Link = flag;
		fTPFile.LinkCount = linkCount;
		fTPFile.LinkedName = linkedName;
		fTPFile.Permissions = text;
		return fTPFile;
	}
}
