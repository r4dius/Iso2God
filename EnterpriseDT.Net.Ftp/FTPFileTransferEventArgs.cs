using EnterpriseDT.Util;
using System;
using System.IO;

namespace EnterpriseDT.Net.Ftp;

public class FTPFileTransferEventArgs : FTPCancelableEventArgs
{
    public enum DataType
    {
        File,
        Stream,
        ByteArray
    }

    private DataType localDataType;

    private string localFilePath;

    private string remoteDirectory;

    private string remoteFile;

    private Stream dataStream;

    private byte[] byteArray;

    private bool append;

    private long fileSize;

    public DataType LocalDataType => localDataType;

    public string LocalPath
    {
        get
        {
            return localFilePath;
        }
        set
        {
            localFilePath = value;
        }
    }

    public string LocalFile => PathUtil.GetFileName(localFilePath);

    public string LocalDirectory => PathUtil.GetFolderPath(localFilePath);

    public Stream Stream => dataStream;

    public byte[] Bytes => byteArray;

    public bool Appended => append;

    public string RemoteFile
    {
        get
        {
            return remoteFile;
        }
        set
        {
            remoteFile = value;
        }
    }

    public string RemoteFileName => PathUtil.GetFileName(RemotePath);

    public string RemotePath
    {
        get
        {
            if (!PathUtil.IsAbsolute(remoteFile))
            {
                return PathUtil.Combine(remoteDirectory, remoteFile);
            }
            return remoteFile;
        }
    }

    public string RemoteDirectory => PathUtil.GetFolderPath(RemotePath);

    public long FileSize => fileSize;

    public override bool Cancel
    {
        get
        {
            return base.Cancel;
        }
        set
        {
            base.Cancel = value;
        }
    }

    internal FTPFileTransferEventArgs(bool canBeCancelled, string localFilePath, string remoteFile, string remoteDirectory, long fileSize, bool append, bool cancelled, Exception ex)
        : this(canBeCancelled, remoteFile, remoteDirectory, fileSize, append, cancelled, ex)
    {
        localDataType = DataType.File;
        this.localFilePath = localFilePath;
    }

    internal FTPFileTransferEventArgs(bool canBeCancelled, Stream dataStream, string remoteFile, string remoteDirectory, long fileSize, bool append, bool cancelled, Exception ex)
        : this(canBeCancelled, remoteFile, remoteDirectory, fileSize, append, cancelled, ex)
    {
        localDataType = DataType.Stream;
        this.dataStream = dataStream;
    }

    internal FTPFileTransferEventArgs(bool canBeCancelled, byte[] bytes, string remoteFile, string remoteDirectory, long fileSize, bool append, bool cancelled, Exception ex)
        : this(canBeCancelled, remoteFile, remoteDirectory, fileSize, append, cancelled, ex)
    {
        localDataType = DataType.ByteArray;
        byteArray = bytes;
    }

    internal FTPFileTransferEventArgs(bool canBeCancelled, string remoteFile, string remoteDirectory, long fileSize, bool append, bool cancelled, Exception ex)
        : base(canBeCancelled, cancelled, ex)
    {
        localDataType = DataType.ByteArray;
        byteArray = null;
        this.remoteFile = remoteFile;
        this.remoteDirectory = remoteDirectory;
        this.fileSize = fileSize;
        this.append = append;
    }
}
