using EnterpriseDT.Util;
using EnterpriseDT.Util.Debug;
using System;
using System.Globalization;
using System.Text;

namespace EnterpriseDT.Net.Ftp;

public class UnixFileParser2 : FTPFileParser
{
    private const string SYMLINK_ARROW = "->";

    private const char SYMLINK_CHAR = 'l';

    private const char ORDINARY_FILE_CHAR = '-';

    private const char DIRECTORY_CHAR = 'd';

    private const string format1a = "MMM'-'d'-'yyyy";

    private const string format1b = "MMM'-'dd'-'yyyy";

    private const string format2a = "MMM'-'d'-'yyyy'-'HH':'mm:ss";

    private const string format2b = "MMM'-'dd'-'yyyy'-'HH':'mm:ss";

    private const string format2c = "MMM'-'d'-'yyyy'-'H':'mm:ss";

    private const string format2d = "MMM'-'dd'-'yyyy'-'H':'mm:ss";

    private const int MIN_FIELD_COUNT = 8;

    private string[] format1 = new string[2] { "MMM'-'d'-'yyyy", "MMM'-'dd'-'yyyy" };

    private string[] format2 = new string[4] { "MMM'-'d'-'yyyy'-'HH':'mm:ss", "MMM'-'dd'-'yyyy'-'HH':'mm:ss", "MMM'-'d'-'yyyy'-'H':'mm:ss", "MMM'-'dd'-'yyyy'-'H':'mm:ss" };

    private Logger log;

    public override bool TimeIncludesSeconds => true;

    public UnixFileParser2()
    {
        log = Logger.GetLogger("UnixFileParser2");
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
            throw new FormatException(stringBuilder.ToString());
        }
        int num = 0;
        string text = array[num++];
        c = text[0];
        bool isDir = false;
        bool link = false;
        switch (c)
        {
            case 'd':
                isDir = true;
                break;
            case 'l':
                link = true;
                break;
        }
        string group = array[num++];
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
        string owner = array[num++];
        long size = 0L;
        string text3 = array[num++];
        try
        {
            size = long.Parse(text3);
        }
        catch (FormatException)
        {
            log.Warn("Failed to parse size: " + text3);
        }
        int num2 = num;
        DateTime lastModifiedTime = DateTime.MinValue;
        StringBuilder stringBuilder2 = new StringBuilder(array[num++]);
        stringBuilder2.Append('-').Append(array[num++]).Append('-');
        string text4 = array[num++];
        if (text4.IndexOf(':') < 0)
        {
            stringBuilder2.Append(text4);
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
            stringBuilder2.Append(year).Append('-').Append(text4);
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
        int num3 = 0;
        bool flag = true;
        for (int i = num2; i < num2 + 3; i++)
        {
            num3 = raw.IndexOf(array[i], num3);
            if (num3 < 0)
            {
                flag = false;
                break;
            }
            num3 += array[i].Length;
        }
        if (flag)
        {
            name = raw.Substring(num3).Trim();
        }
        else
        {
            log.Warn("Failed to retrieve name: " + raw);
        }
        FTPFile fTPFile = new FTPFile(1, raw, name, size, isDir, ref lastModifiedTime);
        fTPFile.Group = group;
        fTPFile.Owner = owner;
        fTPFile.Link = link;
        fTPFile.LinkCount = linkCount;
        fTPFile.Permissions = text;
        return fTPFile;
    }
}
