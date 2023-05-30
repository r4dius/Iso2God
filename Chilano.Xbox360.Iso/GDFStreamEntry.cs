namespace Chilano.Xbox360.Iso;

public class GDFStreamEntry
{
	public GDFDirEntry Entry;

	public string Path;

	public uint Size => Entry.Size;

	public uint Sector
	{
		get
		{
			return Entry.Sector;
		}
		set
		{
			Entry.Sector = value;
		}
	}

	public GDFStreamEntry(GDFDirEntry Entry, string Path)
	{
		this.Entry = Entry;
		this.Path = Path;
	}
}
