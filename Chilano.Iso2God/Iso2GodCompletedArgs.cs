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
        Error = e.Error;
        Cancelled = e.Cancelled;

        if (Error != null)
            Message = "Error!";
        else if (Cancelled)
            Message = "Cancelled";
        else
            Message = (e.Result != null) ? e.Result.ToString() : "Error!";
    }

    public Iso2GodCompletedArgs(RunWorkerCompletedEventArgs e, string ContainerId)
    {
        Cancelled = e.Cancelled;
        Error = e.Error;
        Message = (Error != null || Cancelled || e.Result == null) ? "Error!" : e.Result.ToString();
        this.ContainerId = ContainerId;
    }
}
