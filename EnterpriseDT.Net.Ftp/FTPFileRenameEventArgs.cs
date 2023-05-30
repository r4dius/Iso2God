using EnterpriseDT.Util;
using System;
using System.ComponentModel;

namespace EnterpriseDT.Net.Ftp;

public class FTPFileRenameEventArgs : FTPCancelableEventArgs
{
    private string oldFilePath;

    private string newFilePath;

    public string OldFileName => PathUtil.GetFileName(oldFilePath);

    public string OldFilePath => oldFilePath;

    public string OldDirectory => PathUtil.GetFolderPath(oldFilePath);

    public string NewFileName => PathUtil.GetFileName(newFilePath);

    public string NewFilePath => newFilePath;

    public string NewDirectory => PathUtil.GetFolderPath(newFilePath);

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Cancel")]
    public bool RenameCompleted => !base.Cancel;

    internal FTPFileRenameEventArgs(bool canBeCancelled, string oldFilePath, string newFilePath, bool cancel, Exception ex)
        : base(canBeCancelled, cancel, ex)
    {
        this.oldFilePath = oldFilePath;
        this.newFilePath = newFilePath;
    }
}
