using System;

namespace EnterpriseDT.Net.Ftp;

public class FTPException : FileTransferException
{
	public FTPException(string msg)
		: base(msg)
	{
	}

	public FTPException(string msg, Exception innerException)
		: base(msg, innerException)
	{
	}

	public FTPException(string msg, string replyCode)
		: base(msg, replyCode)
	{
	}

	public FTPException(string msg, int replyCode)
		: base(msg, replyCode)
	{
	}

	public FTPException(FTPReply reply)
		: base(reply.ReplyText, reply.ReplyCode)
	{
	}
}
