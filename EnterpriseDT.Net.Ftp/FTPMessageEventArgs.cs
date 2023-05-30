namespace EnterpriseDT.Net.Ftp;

public class FTPMessageEventArgs : FTPEventArgs
{
	private string message;

	public string Message => message;

	public FTPMessageEventArgs(string message)
	{
		this.message = message;
	}
}
