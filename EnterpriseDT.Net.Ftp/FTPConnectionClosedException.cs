namespace EnterpriseDT.Net.Ftp;

public class FTPConnectionClosedException : FTPException
{
    public FTPConnectionClosedException(string message)
        : base(message)
    {
    }
}
