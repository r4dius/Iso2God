namespace Chilano.Xbox360.Xex;

public class XexOriginalName : XexInfoField
{
	public static byte[] Signature = new byte[4] { 0, 1, 131, 255 };

	public XexOriginalName(uint Address)
		: base(Address)
	{
	}
}
