using System.Text;
using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Xbe;

public class XbeCertifcate
{
	private byte[] titleName = new byte[80];

	private uint titleID;

	public uint Size;

	public byte[] TimeData = new byte[4];

	public byte[] AltTitleIDs = new byte[64];

	public XbeAllowedMedia AllowedMedia;

	public XbeGameRegion GameRegion;

	public uint GameRatings;

	public uint DiskNumber;

	public uint Version;

	public byte[] LanKey = new byte[16];

	public byte[] SignatureKey = new byte[16];

	public byte[] AltSignatureKeys = new byte[256];

	public string TitleID => titleID.ToString("X02");

	public string TitleName
	{
		get
		{
			if (titleName == null)
			{
				return "";
			}
			return Encoding.Unicode.GetString(titleName);
		}
	}

	public XbeCertifcate(CBinaryReader br)
	{
		Size = br.ReadUInt32();
		TimeData = br.ReadBytes(4);
		titleID = br.ReadUInt32();
		titleName = br.ReadBytes(80);
		AltTitleIDs = br.ReadBytes(64);
		AllowedMedia = (XbeAllowedMedia)br.ReadUInt32();
		GameRegion = (XbeGameRegion)br.ReadUInt32();
		GameRatings = br.ReadUInt32();
		DiskNumber = br.ReadUInt32();
		Version = br.ReadUInt32();
		LanKey = br.ReadBytes(16);
		SignatureKey = br.ReadBytes(16);
		AltSignatureKeys = br.ReadBytes(256);
	}
}
