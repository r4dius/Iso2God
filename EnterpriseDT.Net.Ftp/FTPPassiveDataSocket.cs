using System.IO;

namespace EnterpriseDT.Net.Ftp;

public class FTPPassiveDataSocket : FTPDataSocket
{
	internal override int Timeout
	{
		set
		{
			SetSocketTimeout(sock, value);
		}
	}

	internal override Stream DataStream => sock.GetStream();

	internal FTPPassiveDataSocket(BaseSocket sock)
	{
		base.sock = sock;
	}

	internal override void Close()
	{
		sock.Close();
	}
}
