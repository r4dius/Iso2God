using System.IO;
using System.Net;
using System.Net.Sockets;
using EnterpriseDT.Util.Debug;

namespace EnterpriseDT.Net.Ftp;

public abstract class FTPDataSocket
{
	private Logger log = Logger.GetLogger("FTPDataSocket");

	internal BaseSocket sock = null;

	internal int timeout = 0;

	internal virtual int Timeout
	{
		get
		{
			return timeout;
		}
		set
		{
			timeout = value;
			SetSocketTimeout(sock, value);
		}
	}

	internal int LocalPort => ((IPEndPoint)sock.LocalEndPoint).Port;

	internal abstract Stream DataStream { get; }

	internal virtual int Available => sock.Available;

	internal virtual bool Poll(int microseconds, SelectMode mode)
	{
		return sock.Poll(microseconds, mode);
	}

	internal void SetSocketTimeout(BaseSocket sock, int timeout)
	{
		if (timeout > 0)
		{
			try
			{
				sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
				sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);
			}
			catch (SocketException ex)
			{
				log.Warn("Failed to set socket timeout: " + ex.Message);
			}
		}
	}

	internal abstract void Close();
}
