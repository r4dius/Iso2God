namespace Chilano.Xbox360.Xex;

public class XexRatingsInfo : XexInfoField
{
	public static byte[] Signature = new byte[4] { 0, 4, 3, 16 };

	public XexRatingsInfo(uint Address)
		: base(Address)
	{
	}
}
