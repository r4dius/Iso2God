using EnterpriseDT.Util;
using EnterpriseDT.Util.Debug;
using System;
using System.Globalization;

namespace EnterpriseDT.Net.Ftp;

public class OS400FileParser : FTPFileParser
{
    private static readonly string DIR = "*DIR";

    private static readonly string DDIR = "*DDIR";

    private static readonly string MEM = "*MEM";

    private static readonly int MIN_EXPECTED_FIELD_COUNT = 6;

    private static readonly string DATE_FORMAT_1 = "dd'/'MM'/'yy' 'HH':'mm':'ss";

    private static readonly string DATE_FORMAT_2 = "dd'/'MM'/'yyyy' 'HH':'mm':'ss";

    private static readonly string DATE_FORMAT_3 = "dd'.'MM'.'yy' 'HH':'mm':'ss";

    private static readonly string DATE_FORMAT_11 = "yy'/'MM'/'dd' 'HH':'mm':'ss";

    private static readonly string DATE_FORMAT_12 = "yyyy'/'MM'/'dd' 'HH':'mm':'ss";

    private static readonly string DATE_FORMAT_13 = "yy'.'MM'.'dd' 'HH':'mm':'ss";

    private static readonly string DATE_FORMAT_21 = "MM'/'dd'/'yy' 'HH':'mm':'ss";

    private static readonly string DATE_FORMAT_22 = "MM'/'dd'/'yyyy' 'HH':'mm':'ss";

    private static readonly string DATE_FORMAT_23 = "MM'.'dd'.'yy' 'HH':'mm':'ss";

    private static string[] formats1 = new string[3] { DATE_FORMAT_1, DATE_FORMAT_2, DATE_FORMAT_3 };

    private static string[] formats2 = new string[3] { DATE_FORMAT_11, DATE_FORMAT_12, DATE_FORMAT_13 };

    private static string[] formats3 = new string[3] { DATE_FORMAT_21, DATE_FORMAT_22, DATE_FORMAT_23 };

    private string[][] formats = new string[3][] { formats1, formats2, formats3 };

    private Logger log = Logger.GetLogger("OS400FileParser");

    private int formatIndex = 0;

    public override bool TimeIncludesSeconds => true;

    public override string ToString()
    {
        return "OS400";
    }

    public override bool IsValidFormat(string[] listing)
    {
        int num = Math.Min(listing.Length, 10);
        bool flag = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        bool flag6 = false;
        for (int i = 0; i < num; i++)
        {
            if (listing[i].IndexOf("*DIR") > 0)
            {
                flag = true;
            }
            else if (listing[i].IndexOf("*FILE") > 0)
            {
                flag6 = true;
            }
            else if (listing[i].IndexOf("*FLR") > 0)
            {
                flag5 = true;
            }
            else if (listing[i].IndexOf("*DDIR") > 0)
            {
                flag2 = true;
            }
            else if (listing[i].IndexOf("*STMF") > 0)
            {
                flag4 = true;
            }
            else if (listing[i].IndexOf("*LIB") > 0)
            {
                flag3 = true;
            }
        }
        if (flag || flag6 || flag2 || flag3 || flag4 || flag5)
        {
            return true;
        }
        log.Debug("Not in OS/400 format");
        return false;
    }

    public override FTPFile Parse(string raw)
    {
        string[] array = StringSplitter.Split(raw);
        if (array.Length <= 0)
        {
            return null;
        }
        if (array.Length >= 2 && array[1].Equals(MEM))
        {
            DateTime lastModifiedTime = DateTime.MinValue;
            string owner = array[0];
            string name = array[2];
            FTPFile fTPFile = new FTPFile(3, raw, name, 0L, isDir: false, ref lastModifiedTime);
            fTPFile.Owner = owner;
            return fTPFile;
        }
        if (array.Length < MIN_EXPECTED_FIELD_COUNT)
        {
            return null;
        }
        string owner2 = array[0];
        long size = long.Parse(array[1]);
        string lastModifiedStr = array[2] + " " + array[3];
        DateTime lastModifiedTime2 = GetLastModified(lastModifiedStr);
        bool isDir = false;
        if (array[4] == DIR || array[4] == DDIR)
        {
            isDir = true;
        }
        string text = array[5];
        if (text.EndsWith("/"))
        {
            isDir = true;
            text = text.Substring(0, text.Length - 1);
        }
        FTPFile fTPFile2 = new FTPFile(3, raw, text, size, isDir, ref lastModifiedTime2);
        fTPFile2.Owner = owner2;
        return fTPFile2;
    }

    private DateTime GetLastModified(string lastModifiedStr)
    {
        DateTime dateTime = DateTime.MinValue;
        if (formatIndex >= formats.Length)
        {
            log.Warn("Exhausted formats - failed to parse date");
            return DateTime.MinValue;
        }
        int num = formatIndex;
        for (int i = formatIndex; i < formats.Length; i++, formatIndex++)
        {
            try
            {
                dateTime = DateTime.ParseExact(lastModifiedStr, formats[formatIndex], base.ParsingCulture.DateTimeFormat, DateTimeStyles.None);
                if (dateTime > DateTime.Now.AddDays(2.0))
                {
                    log.Debug("Swapping to alternate format (found date in future)");
                    continue;
                }
            }
            catch (FormatException)
            {
                continue;
            }
            break;
        }
        if (formatIndex >= formats.Length)
        {
            log.Warn("Exhausted formats - failed to parse date");
            return DateTime.MinValue;
        }
        if (formatIndex > num)
        {
            throw new RestartParsingException();
        }
        return dateTime;
    }
}
