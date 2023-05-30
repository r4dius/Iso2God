using EnterpriseDT.Util;

namespace EnterpriseDT.Net.Ftp;

public class BytesTransferredEventArgs : FTPEventArgs
{
	private long byteCount;

	private long resumeOffset;

	private string remoteFilePath;

	public long ByteCount => byteCount;

	public long ResumeOffset => resumeOffset;

	public string RemoteFile => PathUtil.GetFileName(remoteFilePath);

	public string RemoteDirectory => PathUtil.GetFolderPath(remoteFilePath);

	public string RemotePath => remoteFilePath;

	public BytesTransferredEventArgs(string remoteFile, long byteCount, long resumeOffset)
	{
		remoteFilePath = remoteFile;
		this.byteCount = byteCount;
		this.resumeOffset = resumeOffset;
	}

	public BytesTransferredEventArgs(string remoteDirectory, string remoteFile, long byteCount, long resumeOffset)
	{
		this.byteCount = byteCount;
		this.resumeOffset = resumeOffset;
		remoteFilePath = (PathUtil.IsAbsolute(remoteFile) ? remoteFile : PathUtil.Combine(remoteDirectory, remoteFile));
	}
}
