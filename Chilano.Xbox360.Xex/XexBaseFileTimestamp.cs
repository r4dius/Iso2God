namespace Chilano.Xbox360.Xex;

public class XexBaseFileTimestamp : XexInfoField
{
	public static byte[] Signature = new byte[4] { 0, 1, 128, 2 };

	public XexBaseFileTimestamp(uint Address)
		: base(Address)
	{
	}
}
