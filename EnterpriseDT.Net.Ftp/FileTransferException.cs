using System;

namespace EnterpriseDT.Net.Ftp;

public class FileTransferException : ApplicationException
{
    private int replyCode = -1;

    public int ReplyCode => replyCode;

    public override string Message
    {
        get
        {
            if (replyCode > 0)
            {
                return base.Message + " (code=" + replyCode + ")";
            }
            return base.Message;
        }
    }

    public FileTransferException(string msg)
        : base(msg)
    {
    }

    public FileTransferException(string msg, Exception innerException)
        : base(msg, innerException)
    {
    }

    public FileTransferException(string msg, string replyCode)
        : base(msg)
    {
        try
        {
            this.replyCode = int.Parse(replyCode);
        }
        catch (FormatException)
        {
            this.replyCode = -1;
        }
    }

    public FileTransferException(string msg, int replyCode)
        : base(msg)
    {
        this.replyCode = replyCode;
    }
}
