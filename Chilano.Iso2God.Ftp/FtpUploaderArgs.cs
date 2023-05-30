namespace Chilano.Iso2God.Ftp;

public class FtpUploaderArgs
{
	public IsoEntryPlatform Platform;

	public string Ip;

	public string User;

	public string Pass;

	public string Port;

	public string TitleID;

	public string ContainerID;

	public string SourcePath;

	public FtpUploaderArgs(string Ip, string User, string Pass, string Port, string TitleID, string ContainerID, string SourcePath, IsoEntryPlatform Platform)
	{
		this.Ip = Ip;
		this.User = User;
		this.Pass = Pass;
		this.Port = Port;
		this.TitleID = TitleID;
		this.ContainerID = ContainerID;
		this.SourcePath = SourcePath;
		this.Platform = Platform;
	}
}
