namespace Chilano.Iso2God;

public struct IsoEntry
{
    public IsoEntryPlatform Platform;

    public string Path;

    public string Destination;

    public string TitleName;

    public string Message;

    public long Size;

    public uint Parts;

    public byte[] Thumb;

    public bool TitleDirectory;

    public IsoEntryID ID;

    public IsoEntryStatus Status;

    public IsoEntryPadding Padding;

    public IsoEntry(IsoEntryPlatform Platform, string Path, string Destination, long Size, string TitleName, IsoEntryID ID, byte[] Thumb, IsoEntryPadding Padding, bool titleDirectory, string Message)
    {
        this.Platform = Platform;
        this.Path = Path;
        this.Destination = Destination;
        this.Size = Size;
        this.TitleName = TitleName;
        Parts = 0u;
        this.ID = ID;
        Status = IsoEntryStatus.Idle;
        this.Thumb = Thumb;
        this.Padding = Padding;
        this.Message = Message;
        this.TitleDirectory = titleDirectory;
    }
}
