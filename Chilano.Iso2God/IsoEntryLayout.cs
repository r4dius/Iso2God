namespace Chilano.Iso2God;

public class IsoEntryLayout
{
    public IsoEntryLayoutID ID;

    public string Path;

    public IsoEntryLayout()
    {
    }

    public IsoEntryLayout(IsoEntryLayoutID ID, string Path)
    {
        this.ID = ID;
        this.Path = Path;
    }
}
