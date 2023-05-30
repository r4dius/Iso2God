namespace EnterpriseDT.Net.Ftp;

public class FTPTransferCancelledException : FTPException
{
	public FTPTransferCancelledException(string message)
		: base(message)
	{
	}
}
