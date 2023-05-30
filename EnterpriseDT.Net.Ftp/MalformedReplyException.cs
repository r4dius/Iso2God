namespace EnterpriseDT.Net.Ftp;

public class MalformedReplyException : FTPException
{
    public MalformedReplyException(string message)
        : base(message)
    {
    }
}
