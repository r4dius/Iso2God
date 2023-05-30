using System.IO;

namespace EnterpriseDT.Net.Ftp;

public class TransferEventArgs : FTPEventArgs
{
	private Stream localStream;

	private byte[] localByteArray;

	private string localFilePath;

	private string remoteFilename;

	private TransferDirection direction;

	private FTPTransferType transferType;

	public string LocalFilePath
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

	public Stream LocalStream => localStream;

	public byte[] LocalByteArray => localByteArray;

	public string RemoteFilename => remoteFilename;

	public TransferDirection Direction => direction;

	public FTPTransferType TransferType => transferType;

	public TransferEventArgs(Stream localStream, string remoteFilename, TransferDirection direction, FTPTransferType transferType)
	{
		this.localStream = localStream;
		this.remoteFilename = remoteFilename;
		this.direction = direction;
		this.transferType = transferType;
	}

	public TransferEventArgs(byte[] localByteArray, string remoteFilename, TransferDirection direction, FTPTransferType transferType)
	{
		this.localByteArray = localByteArray;
		this.remoteFilename = remoteFilename;
		this.direction = direction;
		this.transferType = transferType;
	}

	public TransferEventArgs(string localFilePath, string remoteFilename, TransferDirection direction, FTPTransferType transferType)
	{
		this.localFilePath = localFilePath;
		this.remoteFilename = remoteFilename;
		this.direction = direction;
		this.transferType = transferType;
	}
}
