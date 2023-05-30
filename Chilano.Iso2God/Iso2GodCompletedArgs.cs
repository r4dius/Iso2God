using System;
using System.ComponentModel;

namespace Chilano.Iso2God;

public class Iso2GodCompletedArgs : EventArgs
{
	public string Message;

	public bool Cancelled;

	public Exception Error;

	public string ContainerId;

	public Iso2GodCompletedArgs(RunWorkerCompletedEventArgs e)
	{
		Message = ((e.Result == null) ? "Error!" : e.Result.ToString());
		Cancelled = e.Cancelled;
		Error = e.Error;
	}

	public Iso2GodCompletedArgs(RunWorkerCompletedEventArgs e, string ContainerId)
	{
		Message = ((e.Result == null) ? "Error!" : e.Result.ToString());
		Cancelled = e.Cancelled;
		Error = e.Error;
		this.ContainerId = ContainerId;
	}
}
