using EnterpriseDT.Util;
using EnterpriseDT.Util.Debug;
using System;
using System.Globalization;

namespace EnterpriseDT.Net.Ftp;

public class TandemFileParser : FTPFileParser
{
    private const int MIN_EXPECTED_FIELD_COUNT = 7;

    private Logger log;

    private static readonly string format1 = "d'-'MMM'-'yy HH':'mm':'ss";

    private string[] formats = new string[1] { format1 };

    private char[] trimChars = new char[1] { '"' };

    public override bool TimeIncludesSeconds => true;

    public TandemFileParser()
    {
        log = Logger.GetLogger("TandemFileParser");
    }

    public override string ToString()
    {
        return "Tandem";
    }

    public override bool IsValidFormat(string[] listing)
    {
        return IsHeader(listing[0]);
    }

    private bool IsHeader(string line)
    {
        if (line.IndexOf("Code") > 0 && line.IndexOf("EOF") > 0 && line.IndexOf("RWEP") > 0)
        {
            return true;
        }
        return false;
    }

    public override FTPFile Parse(string raw)
    {
        if (IsHeader(raw))
        {
            return null;
        }
        string[] array = StringSplitter.Split(raw);
        if (array.Length < 7)
        {
            return null;
        }
        string name = array[0];
        string text = array[3] + " " + array[4];
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
        try
        {
            size = long.Parse(array[2]);
        }
        catch (FormatException)
        {
            log.Warn("Failed to parse size: " + array[2]);
        }
        string owner = array[5] + array[6];
        string permissions = array[7].Trim(trimChars);
        FTPFile fTPFile = new FTPFile(-1, raw, name, size, isDir, ref lastModifiedTime);
        fTPFile.Owner = owner;
        fTPFile.Permissions = permissions;
        return fTPFile;
    }
}
