using System;
using System.IO;

namespace EnterpriseDT.Net.Ftp;

public interface IFileTransferClient
{
    bool CloseStreamsAfterTransfer { get; set; }

    int ControlPort { get; set; }

    bool DeleteOnFailure { get; set; }

    bool IsConnected { get; }

    string RemoteHost { get; set; }

    int Timeout { get; set; }

    int TransferBufferSize { get; set; }

    long TransferNotifyInterval { get; set; }

    bool TransferNotifyListings { get; set; }

    FTPTransferType TransferType { get; set; }

    bool ShowHiddenFiles { get; set; }

    event BytesTransferredHandler BytesTransferred;

    event TransferHandler TransferCompleteEx;

    event TransferHandler TransferStartedEx;

    void Connect();

    void Quit();

    void QuitImmediately();

    void Get(Stream destStream, string remoteFile);

    void Get(string localPath, string remoteFile);

    byte[] Get(string remoteFile);

    void Put(byte[] bytes, string remoteFile);

    void Put(byte[] bytes, string remoteFile, bool append);

    void Put(string localPath, string remoteFile);

    void Put(string localPath, string remoteFile, bool append);

    long Put(Stream srcStream, string remoteFile);

    long Put(Stream srcStream, string remoteFile, bool append);

    void CdUp();

    void ChDir(string dir);

    string[] Dir();

    string[] Dir(string dirname, bool full);

    string[] Dir(string dirname);

    FTPFile[] DirDetails(string dirname);

    FTPFile[] DirDetails();

    void MkDir(string dir);

    string Pwd();

    void RmDir(string dir);

    bool Exists(string remoteFile);

    void Delete(string remoteFile);

    DateTime ModTime(string remoteFile);

    void SetModTime(string remoteFile, DateTime modTime);

    void Rename(string from, string to);

    long Size(string remoteFile);

    void CancelResume();

    void CancelTransfer();

    void Resume();
}
