namespace Chilano.Iso2God;

public class IsoEntryOptions
{
    public IsoEntryPaddingRemoval Padding;

    public IsoEntryFormat Format;

    public string TempPath;

    public string IsoPath;

    public IsoEntryGameDirectoryLayout Layout;

    public bool DeleteGod = false;

    public bool DeleteSource = false;

    public bool FtpUpload;

    public IsoEntryOptions()
    {
    }

    public IsoEntryOptions(IsoEntryPaddingRemoval Padding, IsoEntryFormat Format, string TempPath, string IsoPath, IsoEntryGameDirectoryLayout Layout, bool DeleteGod, bool DeleteSource, bool FtpUpload)
    {
        this.Padding = Padding;
        this.Format = Format;
        this.TempPath = TempPath;
        this.IsoPath = IsoPath;
        this.Layout = Layout;
        this.DeleteGod = DeleteGod;
        this.DeleteSource = DeleteSource;
        this.FtpUpload = FtpUpload;
    }
}
