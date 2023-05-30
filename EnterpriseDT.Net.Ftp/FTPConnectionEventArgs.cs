namespace EnterpriseDT.Net.Ftp;

public class FTPConnectionEventArgs : FTPEventArgs
{
    private string serverAddress;

    private int serverPort;

    private bool connected;

    public string ServerAddress => serverAddress;

    public int ServerPort => serverPort;

    public bool IsConnected => connected;

    internal FTPConnectionEventArgs(string serverAddress, int serverPort, bool connected)
    {
        this.serverAddress = serverAddress;
        this.serverPort = serverPort;
        this.connected = connected;
    }
}
