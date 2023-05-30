using System.IO;
using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Xex;

public class XexExecutionInfo : XexInfoField
{
	public static byte[] Signature = new byte[4] { 0, 4, 0, 6 };

	public byte[] TitleID = new byte[0];

	public byte[] MediaID = new byte[0];

	public uint BaseVersion;

	public uint Version;

	public byte Platform;

	public byte ExecutableType;

	public byte DiscNumber;

	public byte DiscCount;

	public XexExecutionInfo(uint Address)
		: base(Address)
	{
	}

	public override void Parse(CBinaryReader br)
	{
		br.Seek(Address, SeekOrigin.Begin);
		br.Endian = EndianType.BigEndian;
		MediaID = br.ReadBytes(4);
		Version = br.ReadUInt32();
		BaseVersion = br.ReadUInt32();
		TitleID = br.ReadBytes(4);
		Platform = br.ReadByte();
		ExecutableType = br.ReadByte();
		DiscNumber = br.ReadByte();
		DiscCount = br.ReadByte();
	}
}
