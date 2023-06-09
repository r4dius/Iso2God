namespace EnterpriseDT.Net.Ftp;

public enum MethodIdentifier
{
    ChangeWorkingDirectory,
    ChangeWorkingDirectoryUp,
    Close,
    Connect,
    CreateDirectory,
    DeleteDirectory,
    DeleteDirectoryTree,
    DeleteFile,
    DeleteMultipleFiles,
    DownloadByteArray,
    DownloadFile,
    DownloadDirectory,
    DownloadMultiple,
    DownloadStream,
    Exists,
    GetCommandHelp,
    GetFeatures,
    GetFileInfos,
    GetFiles,
    GetLastWriteTime,
    GetSize,
    GetSystemType,
    GetWorkingDirectory,
    InvokeFTPCommand,
    InvokeSiteCommand,
    Login,
    RenameFile,
    ResumeTransfer,
    SendPassword,
    SendUserName,
    Synchronize,
    TransferFileFXP,
    UploadByteArray,
    UploadDirectory,
    UploadMultiple,
    UploadFile,
    UploadStream
}
