using System;

namespace EnterpriseDT.Net.Ftp;

public class FTPEventArgs : EventArgs
{
    private int taskID = -1;

    private int connectionInstance = -1;

    private bool guiThread = false;

    public int TaskID
    {
        get
        {
            return taskID;
        }
        set
        {
            taskID = value;
        }
    }

    public int ConnectionInstanceNumber
    {
        get
        {
            return connectionInstance;
        }
        set
        {
            connectionInstance = value;
        }
    }

    public bool IsGuiThread
    {
        get
        {
            return guiThread;
        }
        set
        {
            guiThread = value;
        }
    }
}
