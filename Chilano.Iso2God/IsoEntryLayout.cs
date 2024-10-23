namespace Chilano.Iso2God;

public class IsoEntryLayout
{
    public int ID;

    public string Path;

    public IsoEntryLayout()
    {
    }

    public IsoEntryLayout(int ID, string Path)
    {
        this.ID = ID;
        this.Path = Path;
    }
}
