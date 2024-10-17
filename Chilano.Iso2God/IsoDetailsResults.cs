using System.Drawing;

namespace Chilano.Iso2God;

internal class IsoDetailsResults
{
    public IsoDetailsResultsType Results;

    public IsoDetailsPlatform ConsolePlatform;

    public string ErrorMessage;

    public string ProgressMessage;

    public string Name;

    public string TitleID;

    public string MediaID;

    public string Platform;

    public string ExType;

    public string DiscNumber;

    public string DiscCount;

    public Image Thumbnail;

    public byte[] RawThumbnail;

    public IsoDetailsResults()
    {
    }

    public IsoDetailsResults(string Name, string TitleID, string MediaID, string Platform, string ExType, string DiscNumber, string DiscCount, Image Thumbnail)
    {
        Results = IsoDetailsResultsType.Completed;
        ConsolePlatform = IsoDetailsPlatform.Xbox360;
        this.Name = Name;
        this.TitleID = TitleID;
        this.MediaID = MediaID;
        this.Platform = Platform;
        this.ExType = ExType;
        this.DiscNumber = DiscNumber;
        this.DiscCount = DiscCount;
        this.Thumbnail = Thumbnail;
    }

    public IsoDetailsResults(string Name, string TitleID, string DiscNumber)
    {
        Results = IsoDetailsResultsType.Completed;
        ConsolePlatform = IsoDetailsPlatform.Xbox;
        this.Name = Name;
        this.TitleID = TitleID;
        MediaID = "00000000";
        Platform = "0";
        ExType = "0";
        this.DiscNumber = DiscNumber;
    }

    public IsoDetailsResults(IsoDetailsResultsType Type, string Message)
    {
        Results = Type;
        if (Type == IsoDetailsResultsType.Error)
        {
            ErrorMessage = Message;
        }
        if (Type == IsoDetailsResultsType.Progress)
        {
            ProgressMessage = Message;
        }
    }
}
