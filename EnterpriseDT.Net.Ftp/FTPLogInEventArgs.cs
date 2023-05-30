namespace EnterpriseDT.Net.Ftp;

public class FTPLogInEventArgs : FTPEventArgs
{
	private string userName;

	private string password;

	private bool hasLoggedIn;

	public string UserName => userName;

	public string Password => password;

	public bool HasLoggedIn => hasLoggedIn;

	internal FTPLogInEventArgs(string userName, string password, bool hasLoggedIn)
	{
		this.userName = userName;
		this.password = password;
		this.hasLoggedIn = hasLoggedIn;
	}
}
