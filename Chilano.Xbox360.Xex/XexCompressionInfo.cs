namespace Chilano.Xbox360.Xex;

public class XexCompressionInfo : XexInfoField
{
	public static byte[] Signature = new byte[4] { 0, 0, 3, 255 };

	public XexCompressionInfo(uint Address)
		: base(Address)
	{
	}
}
