using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Graphics;

public class XPRHeader
{
    public uint MagicBytes;

    public uint FileSize;

    public uint HeaderSize;

    public uint TextureCommon;

    public uint TextureData;

    public uint TextureLock;

    public byte TextureMisc1;

    public byte TextureFormat;

    public byte TextureRes1;

    public byte TextureRes2;

    public bool IsValid;

    public XPRHeader()
    {
    }

    public XPRHeader(CBinaryReader br)
    {
        br.Endian = EndianType.LittleEndian;
        MagicBytes = br.ReadUInt32();
        if (MagicBytes == 810700888)
        {
            FileSize = br.ReadUInt32();
            HeaderSize = br.ReadUInt32();
            TextureCommon = br.ReadUInt32();
            TextureData = br.ReadUInt32();
            TextureLock = br.ReadUInt32();
            TextureMisc1 = br.ReadByte();
            TextureFormat = br.ReadByte();
            TextureRes1 = br.ReadByte();
            TextureRes2 = br.ReadByte();
            IsValid = true;
        }
    }
}
