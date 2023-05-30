using EnterpriseDT.Util;
using System;

namespace EnterpriseDT.Net.Ftp;

public class FTPDirectoryListEventArgs : FTPEventArgs
{
    private string dirPath = null;

    private FTPFile[] files = null;

    public FTPFile[] FileInfos => files;

    [Obsolete("Use DirectoryName or DirectoryPath")]
    public string Directory => dirPath;

    public string DirectoryName => PathUtil.GetFileName(dirPath);

    public string DirectoryPath => dirPath;

    internal FTPDirectoryListEventArgs(string dirPath)
    {
        this.dirPath = dirPath;
    }

    internal FTPDirectoryListEventArgs(string dirPath, FTPFile[] files)
    {
        this.dirPath = dirPath;
        this.files = files;
    }
}
