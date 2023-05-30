namespace Chilano.Xbox360.Iso;

public enum GDFDirEntryAttrib : byte
{
	ReadOnly = 1,
	Hidden = 2,
	System = 4,
	Directory = 0x10,
	Archive = 0x20,
	Normal = 0x80
}
