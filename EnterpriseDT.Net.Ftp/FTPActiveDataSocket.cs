using System.IO;
using System.Net.Sockets;
using EnterpriseDT.Util.Debug;

namespace EnterpriseDT.Net.Ftp;

public class FTPActiveDataSocket : FTPDataSocket
{
	private Logger log;

	internal BaseSocket acceptedSock = null;

	internal override int Timeout
	{
		set
		{
			timeout = value;
			SetSocketTimeout(sock, value);
			if (acceptedSock != null)
			{
				SetSocketTimeout(acceptedSock, value);
			}
		}
	}

	internal override Stream DataStream
	{
		get
		{
			AcceptConnection();
			return acceptedSock.GetStream();
		}
	}

	internal override int Available
	{
		get
		{
			if (acceptedSock != null)
			{
				return acceptedSock.Available;
			}
			throw new IOException("Not accepted yet");
		}
	}

	internal FTPActiveDataSocket(BaseSocket sock)
	{
		base.sock = sock;
		log = Logger.GetLogger("FTPActiveDataSocket");
	}

	internal virtual void AcceptConnection()
	{
		if (acceptedSock == null)
		{
			acceptedSock = sock.Accept(timeout);
			SetSocketTimeout(acceptedSock, timeout);
			log.Debug("AcceptConnection() succeeded");
		}
	}

	internal override bool Poll(int microseconds, SelectMode mode)
	{
		if (acceptedSock != null)
		{
			return acceptedSock.Poll(microseconds, mode);
		}
		throw new IOException("Not accepted yet");
	}

	internal override void Close()
	{
		try
		{
			if (acceptedSock != null)
			{
				acceptedSock.Close();
				acceptedSock = null;
			}
		}
		finally
		{
			sock.Close();
		}
	}
}
