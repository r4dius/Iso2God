namespace Chilano.Iso2God;

public class IsoEntryPadding
{
    public IsoEntryPaddingRemoval Type;

    public string TempPath;

    public string IsoPath;

    public bool KeepIso = true;

    public bool KeepGod = true;

    public bool SkipGod = false;

    public IsoEntryPadding()
    {
    }

    public IsoEntryPadding(IsoEntryPaddingRemoval Type, string TempPath, string IsoPath, bool KeepIso, bool KeepGod, bool SkipGod)
    {
        this.Type = Type;
        this.TempPath = TempPath;
        this.IsoPath = IsoPath;
        this.KeepIso = KeepIso;
        this.KeepGod = KeepGod;
        this.SkipGod = SkipGod;
    }
}
