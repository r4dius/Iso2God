namespace Chilano.Iso2God;

public class IsoEntryGameDirectoryLayout
{
    public IsoEntryGameDirectoryLayoutID ID;

    public string Path;

    public IsoEntryGameDirectoryLayout()
    {
    }

    public IsoEntryGameDirectoryLayout(IsoEntryGameDirectoryLayoutID ID, string Path)
    {
        this.ID = ID;
        this.Path = Path;
    }
}
