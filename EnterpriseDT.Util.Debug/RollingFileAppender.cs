using System;
using System.IO;
using System.Text;

namespace EnterpriseDT.Util.Debug;

public class RollingFileAppender : FileAppender
{
    private const long DEFAULT_MAXSIZE = 10485760L;

    private const int CHECK_COUNT_FREQUENCY = 100;

    private long maxFileSize = 10485760L;

    private int sizeCheckCount = 0;

    private int maxSizeRollBackups = 1;

    public int MaxSizeRollBackups
    {
        get
        {
            return maxSizeRollBackups;
        }
        set
        {
            maxSizeRollBackups = ((value >= 0) ? value : 0);
        }
    }

    public long MaxFileSize
    {
        get
        {
            return maxFileSize;
        }
        set
        {
            maxFileSize = value;
        }
    }

    public RollingFileAppender(string fileName, long maxFileSize)
        : base(fileName)
    {
        this.maxFileSize = maxFileSize;
    }

    public RollingFileAppender(string fileName)
        : base(fileName)
    {
    }

    private void CheckForRollover()
    {
        try
        {
            long num = fileStream.Position;
            if (sizeCheckCount >= 100)
            {
                num = fileStream.Length;
                sizeCheckCount = 0;
            }
            else
            {
                sizeCheckCount++;
            }
            if (num > maxFileSize)
            {
                Rollover();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to rollover log files (" + ex.Message + ")");
        }
    }

    private void Rollover()
    {
        Close();
        FileInfo fileInfo = new FileInfo(base.FileName);
        if (maxSizeRollBackups == 0)
        {
            fileInfo.Delete();
        }
        else
        {
            FileInfo fileInfo2 = new FileInfo(base.FileName + "." + maxSizeRollBackups);
            if (fileInfo2.Exists)
            {
                fileInfo2.Delete();
            }
            for (int num = maxSizeRollBackups - 1; num > 0; num--)
            {
                fileInfo2 = new FileInfo(base.FileName + "." + num);
                if (fileInfo2.Exists)
                {
                    fileInfo2.MoveTo(base.FileName + "." + (num + 1));
                }
            }
            fileInfo.MoveTo(base.FileName + ".1");
        }
        sizeCheckCount = 0;
        Open();
    }

    public override void Log(string msg)
    {
        if (!closed)
        {
            CheckForRollover();
            logger.WriteLine(msg);
            logger.Flush();
        }
        else
        {
            Console.WriteLine(msg);
        }
    }

    public override void Log(Exception t)
    {
        StringBuilder stringBuilder = new StringBuilder(((object)t).GetType().FullName);
        stringBuilder.Append(": ").Append(t.Message);
        if (!closed)
        {
            CheckForRollover();
            logger.WriteLine(stringBuilder.ToString());
            logger.WriteLine(t.StackTrace.ToString());
            logger.Flush();
        }
        else
        {
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
