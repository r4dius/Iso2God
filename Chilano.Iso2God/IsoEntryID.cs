namespace Chilano.Iso2God;

public class IsoEntryID
{
	public string TitleID;

	public string MediaID;

	public byte DiscNumber;

	public byte DiscCount;

	public byte Platform;

	public byte ExType;

	public string ContainerID;

	public IsoEntryID()
	{
	}

	public IsoEntryID(string TitleID, string MediaID, byte DiscNumber, byte DiscCount, byte Platform, byte ExType)
	{
		this.TitleID = TitleID;
		this.MediaID = MediaID;
		this.DiscNumber = DiscNumber;
		this.DiscCount = DiscCount;
		this.Platform = Platform;
		this.ExType = ExType;
	}
}
