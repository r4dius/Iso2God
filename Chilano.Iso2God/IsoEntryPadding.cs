namespace Chilano.Iso2God;

public class IsoEntryPadding
{
    public IsoEntryPaddingRemoval Type;

    public string TempPath;

    public string IsoPath;

    public bool DeleteSource = false;

    public bool DeleteRebuilt = false;

    public IsoEntryPadding()
    {
    }

    public IsoEntryPadding(IsoEntryPaddingRemoval Type, string TempPath, string IsoPath)
    {
        this.Type = Type;
        this.TempPath = TempPath;
        this.IsoPath = IsoPath;
    }
}
