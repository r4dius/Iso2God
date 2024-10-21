using Chilano.Iso2God.Ftp;

namespace Chilano.Iso2God;

public class IsoEntryOptions
{
    public IsoEntryPaddingRemoval Padding;

    public IsoEntryFormat Format;

    public string TempPath;

    public string IsoPath;

    public bool DeleteGod = false;

    public bool DeleteSource = false;

    public int FolderLayout;

    public bool FtpUpload;

    public IsoEntryOptions()
    {
    }

    public IsoEntryOptions(IsoEntryPaddingRemoval Padding, IsoEntryFormat Format, string TempPath, string IsoPath, bool DeleteGod, bool DeleteSource, int FolderLayout, bool FtpUpload)
    {
        this.Padding = Padding;
        this.Format = Format;
        this.TempPath = TempPath;
        this.IsoPath = IsoPath;
        this.DeleteGod = DeleteGod;
        this.DeleteSource = DeleteSource;
        this.FolderLayout = FolderLayout;
        this.FtpUpload = FtpUpload;
    }
}
