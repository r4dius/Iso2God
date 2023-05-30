namespace Chilano.Iso2God;

public class IsoEntryPadding
{
	public IsoEntryPaddingRemoval Type;

	public string TempPath;

	public string IsoPath;

	public bool KeepIso = true;

	public IsoEntryPadding()
	{
	}

	public IsoEntryPadding(IsoEntryPaddingRemoval Type, string TempPath, string IsoPath, bool KeepIso)
	{
		this.Type = Type;
		this.TempPath = TempPath;
		this.IsoPath = IsoPath;
		this.KeepIso = KeepIso;
	}
}
