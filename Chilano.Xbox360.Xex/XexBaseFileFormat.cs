namespace Chilano.Xbox360.Xex;

public class XexBaseFileFormat : XexInfoField
{
	public static byte[] Signature = new byte[4] { 0, 0, 3, 255 };

	public XexBaseFileFormat(uint Address)
		: base(Address)
	{
	}
}
