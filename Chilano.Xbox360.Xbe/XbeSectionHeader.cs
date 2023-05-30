using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Xbe;

public class XbeSectionHeader
{
    public XbeSectionFlags Flags;

    public uint VirtualAddress;

    public uint VirtualSize;

    public uint RawAddress;

    public uint RawSize;

    public uint SectionNameAddress;

    public uint SectionNameRefCount;

    public uint HeadSharedPageRefCountAddress;

    public uint TailSharedPageRefCountAddress;

    public byte[] SectionDigest = new byte[20];

    public XbeSectionHeader()
    {
    }

    public XbeSectionHeader(CBinaryReader bw)
    {
        bw.Endian = EndianType.LittleEndian;
        Flags = (XbeSectionFlags)bw.ReadUInt32();
        VirtualAddress = bw.ReadUInt32();
        VirtualSize = bw.ReadUInt32();
        RawAddress = bw.ReadUInt32();
        RawSize = bw.ReadUInt32();
        SectionNameAddress = bw.ReadUInt32();
        SectionNameRefCount = bw.ReadUInt32();
        HeadSharedPageRefCountAddress = bw.ReadUInt32();
        TailSharedPageRefCountAddress = bw.ReadUInt32();
        SectionDigest = bw.ReadBytes(20);
    }
}
