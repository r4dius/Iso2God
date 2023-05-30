namespace Chilano.Iso2God;

internal class IsoDetailsArgs
{
	public string PathISO;

	public string PathTemp;

	public string PathXexTool;

	public IsoDetailsArgs(string ISO, string Temp, string XT)
	{
		PathISO = ISO;
		PathTemp = Temp;
		PathXexTool = XT;
	}
}
