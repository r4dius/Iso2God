using System;
using EnterpriseDT.Util;

namespace EnterpriseDT.Net.Ftp;

public class FTPDirectoryEventArgs : FTPCancelableEventArgs
{
	private string oldDirectory;

	private string newDirectory;

	[Obsolete("Use OldDirectoryName or OldDirectoryPath")]
	public string OldDirectory => oldDirectory;

	public string OldDirectoryName => PathUtil.GetFileName(oldDirectory);

	public string OldDirectoryPath => oldDirectory;

	[Obsolete("Use NewDirectoryName or NewDirectoryPath")]
	public string NewDirectory => newDirectory;

	public string NewDirectoryName => PathUtil.GetFileName(newDirectory);

	public string NewDirectoryPath => newDirectory;

	internal FTPDirectoryEventArgs(string oldDirectory, string newDirectory, bool cancel, Exception ex)
		: base(canBeCancelled: true, cancel, ex)
	{
		this.oldDirectory = oldDirectory;
		this.newDirectory = newDirectory;
	}

	internal FTPDirectoryEventArgs(string oldDirectory, string newDirectory, Exception ex)
		: base(canBeCancelled: false, defaultCancelValue: false, ex)
	{
		this.oldDirectory = oldDirectory;
		this.newDirectory = newDirectory;
	}
}
