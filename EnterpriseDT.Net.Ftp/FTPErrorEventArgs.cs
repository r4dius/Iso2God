using System;
using System.Windows.Forms;

namespace EnterpriseDT.Net.Ftp;

public class FTPErrorEventArgs : FTPEventArgs
{
    private Exception exception;

    private string methodName;

    private object[] methodArguments;

    public Exception Exception => exception;

    public string SyncMethodName => methodName;

    public object[] Arguments => methodArguments;

    internal FTPErrorEventArgs(Exception exception)
    {
        this.exception = exception;
    }

    internal FTPErrorEventArgs(Exception exception, string methodName, object[] methodArguments)
    {
        this.exception = exception;
        this.methodName = methodName;
        this.methodArguments = methodArguments;
    }

    public void ShowMessageBox()
    {
        ShowMessageBox(null, showDetail: false);
    }

    public void ShowMessageBox(IWin32Window owner)
    {
        ShowMessageBox(owner, showDetail: false);
    }

    public void ShowMessageBox(bool showDetail)
    {
        ShowMessageBox(null, showDetail);
    }

    public void ShowMessageBox(IWin32Window owner, bool showDetail)
    {
        Exception innerException = exception;
        while (innerException.InnerException != null)
        {
            innerException = innerException.InnerException;
        }
        string text = ((!showDetail) ? innerException.Message : $"{((object)innerException).GetType()}: {innerException.Message}\n{innerException.StackTrace}");
        MessageBox.Show(owner, text, "FTP Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
}
