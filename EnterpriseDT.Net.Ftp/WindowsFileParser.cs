using EnterpriseDT.Util;
using EnterpriseDT.Util.Debug;
using System;
using System.Globalization;

namespace EnterpriseDT.Net.Ftp;

public class WindowsFileParser : FTPFileParser
{
    private const string DIR = "<DIR>";

    private const int MIN_EXPECTED_FIELD_COUNT = 4;

    private Logger log;

    private static readonly string format1 = "MM'-'dd'-'yy hh':'mmtt";

    private static readonly string format2 = "MM'-'dd'-'yy HH':'mm";

    private string[] formats = new string[2] { format1, format2 };

    private char[] sep = new char[1] { ' ' };

    public override bool TimeIncludesSeconds => false;

    public WindowsFileParser()
    {
        log = Logger.GetLogger("WindowsFileParser");
    }

    public override string ToString()
    {
        return "Windows";
    }

    public override bool IsValidFormat(string[] listing)
    {
        int num = Math.Min(listing.Length, 10);
        bool flag = false;
        bool flag2 = false;
        bool flag3 = false;
        for (int i = 0; i < num; i++)
        {
            if (listing[i].Trim().Length == 0)
            {
                continue;
            }
            string[] array = StringSplitter.Split(listing[i]);
            if (array.Length >= 4)
            {
                if (char.IsDigit(array[0][0]) && char.IsDigit(array[0][array[0].Length - 1]))
                {
                    flag = true;
                }
                if (array[1].IndexOf(':') > 0)
                {
                    flag2 = true;
                }
                if (array[2].ToUpper() == "<DIR>" || char.IsDigit(array[2][0]))
                {
                    flag3 = true;
                }
            }
        }
        if (flag && flag2 && flag3)
        {
            return true;
        }
        log.Debug("Not in Windows format");
        return false;
    }

    public override FTPFile Parse(string raw)
    {
        string[] array = StringSplitter.Split(raw);
        if (array.Length < 4)
        {
            return null;
        }
        string text = array[0] + " " + array[1];
        DateTime lastModifiedTime = DateTime.MinValue;
        try
        {
            lastModifiedTime = DateTime.ParseExact(text, formats, base.ParsingCulture.DateTimeFormat, DateTimeStyles.None);
        }
        catch (FormatException)
        {
            log.Warn("Failed to parse date string '" + text + "'");
        }
        bool isDir = false;
        long size = 0L;
        if (array[2].ToUpper().Equals("<DIR>".ToUpper()))
        {
            isDir = true;
        }
        else
        {
            try
            {
                size = long.Parse(array[2]);
            }
            catch (FormatException)
            {
                log.Warn("Failed to parse size: " + array[2]);
            }
        }
        int num = 0;
        bool flag = true;
        for (int i = 0; i < 3; i++)
        {
            num = raw.IndexOf(array[i], num);
            if (num < 0)
            {
                flag = false;
                break;
            }
            num += array[i].Length;
        }
        string name = null;
        if (flag)
        {
            name = raw.Substring(num).Trim();
        }
        else
        {
            log.Warn("Failed to retrieve name: " + raw);
        }
        return new FTPFile(0, raw, name, size, isDir, ref lastModifiedTime);
    }
}
