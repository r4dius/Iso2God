namespace Chilano.Xbox360.Iso;

public struct GDFVolumeDescriptor
{
    public byte[] Identifier;

    public uint RootDirSector;

    public uint RootDirSize;

    public byte[] ImageCreationTime;

    public uint SectorSize;

    public uint RootOffset;

    public ulong VolumeSize;

    public uint VolumeSectors;
}
