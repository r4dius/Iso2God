using System;

namespace EnterpriseDT.Net.Ftp;

public class FTPCancelableEventArgs : FTPEventArgs
{
	private bool cancel;

	private bool canBeCancelled;

	private Exception ex;

	public virtual bool Cancel
	{
		get
		{
			return cancel;
		}
		set
		{
			cancel = value;
		}
	}

	internal bool CanBeCancelled => canBeCancelled;

	public bool Succeeded => ex == null;

	public Exception Exception => ex;

	protected FTPCancelableEventArgs(bool canBeCancelled, bool defaultCancelValue, Exception ex)
	{
		cancel = defaultCancelValue;
		this.canBeCancelled = canBeCancelled;
		this.ex = ex;
	}
}
