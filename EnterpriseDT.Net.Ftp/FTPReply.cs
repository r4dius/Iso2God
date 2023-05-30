namespace EnterpriseDT.Net.Ftp;

public class FTPReply
{
	private string replyCode;

	private string replyText;

	private string[] data;

	public virtual string ReplyCode => replyCode;

	public virtual string ReplyText => replyText;

	public virtual string[] ReplyData => data;

	public FTPReply(string replyCode, string replyText)
		: this(replyCode, replyText, null)
	{
	}

	public FTPReply(string replyCode, string replyText, string[] data)
	{
		foreach (char c in replyCode)
		{
			if (!char.IsDigit(c))
			{
				throw new MalformedReplyException("Malformed FTP reply: " + replyCode);
			}
		}
		this.replyCode = replyCode;
		this.replyText = replyText;
		this.data = data;
	}
}
