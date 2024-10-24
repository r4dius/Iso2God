namespace Chilano.Iso2God.Ftp;

public class FtpTesterArgs
{
    public string Ip;

    public string User;

    public string Pass;

    public string Port;

    public FtpTesterArgs(string Ip, string User, string Pass, string Port)
    {
        this.Ip = Ip;
        this.User = User;
        this.Pass = Pass;
        this.Port = Port;
    }
}
