using System;
using System.Globalization;
using System.Text;

namespace EnterpriseDT.Net.Ftp;

public class FTPFile
{
    public const int UNKNOWN = -1;

    public const int WINDOWS = 0;

    public const int UNIX = 1;

    public const int VMS = 2;

    public const int OS400 = 3;

    private static readonly string format = "dd-MM-yyyy HH:mm";

    private int type;

    protected internal bool isLink = false;

    protected internal int linkNum = 1;

    protected internal string filePermissions;

    protected internal bool isDir = false;

    protected internal long fileSize = 0L;

    protected internal string fileName;

    protected internal string linkedFileName;

    protected internal string fileOwner;

    protected internal string userGroup;

    protected internal DateTime lastModifiedTime;

    protected internal string rawRep;

    protected string filePath;

    protected FTPFile[] kids;

    public int Type => type;

    public string Name
    {
        get
        {
            return fileName;
        }
        set
        {
            fileName = value;
        }
    }

    public string Raw => rawRep;

    public int LinkCount
    {
        get
        {
            return linkNum;
        }
        set
        {
            linkNum = value;
        }
    }

    public bool Link
    {
        get
        {
            return isLink;
        }
        set
        {
            isLink = value;
        }
    }

    public string LinkedName
    {
        get
        {
            return linkedFileName;
        }
        set
        {
            linkedFileName = value;
        }
    }

    public string Group
    {
        get
        {
            return userGroup;
        }
        set
        {
            userGroup = value;
        }
    }

    public string Owner
    {
        get
        {
            return fileOwner;
        }
        set
        {
            fileOwner = value;
        }
    }

    public bool Dir
    {
        get
        {
            return isDir;
        }
        set
        {
            isDir = value;
        }
    }

    public string Path
    {
        get
        {
            return filePath;
        }
        set
        {
            filePath = value;
        }
    }

    public FTPFile[] Children
    {
        get
        {
            return kids;
        }
        set
        {
            kids = value;
        }
    }

    public string Permissions
    {
        get
        {
            return filePermissions;
        }
        set
        {
            filePermissions = value;
        }
    }

    public DateTime LastModified
    {
        get
        {
            return lastModifiedTime;
        }
        set
        {
            lastModifiedTime = value;
        }
    }

    public long Size
    {
        get
        {
            return fileSize;
        }
        set
        {
            fileSize = value;
        }
    }

    public FTPFile(int type, string raw, string name, long size, bool isDir, ref DateTime lastModifiedTime)
    {
        this.type = type;
        rawRep = raw;
        fileName = name;
        fileSize = size;
        this.isDir = isDir;
        this.lastModifiedTime = lastModifiedTime;
    }

    internal FTPFile(string name, long size, bool isDir, DateTime lastModifiedTime)
    {
        type = -1;
        rawRep = "";
        fileName = name;
        fileSize = size;
        this.isDir = isDir;
        this.lastModifiedTime = lastModifiedTime;
    }

    internal FTPFile(string name, long size, bool isDir, DateTime lastModifiedTime, string path)
    {
        type = -1;
        rawRep = "";
        fileName = name;
        fileSize = size;
        this.isDir = isDir;
        this.lastModifiedTime = lastModifiedTime;
        filePath = path;
    }

    internal void SetLastModified(DateTime time, TimeSpan timeDifference)
    {
        lastModifiedTime = time;
        ApplyTimeDifference(timeDifference);
    }

    internal void ApplyTimeDifference(TimeSpan difference)
    {
        lastModifiedTime -= difference;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder(rawRep);
        stringBuilder.Append("[").Append("Name=").Append(fileName)
            .Append(",")
            .Append("Size=")
            .Append(fileSize)
            .Append(",")
            .Append("Permissions=")
            .Append(filePermissions)
            .Append(",")
            .Append("Owner=")
            .Append(fileOwner)
            .Append(",")
            .Append("Group=")
            .Append(userGroup)
            .Append(",")
            .Append("Is link=")
            .Append(isLink)
            .Append(",")
            .Append("Link count=")
            .Append(linkNum)
            .Append(",")
            .Append("Is dir=")
            .Append(isDir)
            .Append(",")
            .Append("Linked name=")
            .Append(linkedFileName)
            .Append(",")
            .Append("Permissions=")
            .Append(filePermissions)
            .Append(",")
            .Append("Last modified=")
            .Append(lastModifiedTime.ToString(format, CultureInfo.CurrentCulture.DateTimeFormat))
            .Append("]");
        return stringBuilder.ToString();
    }
}
