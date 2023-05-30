namespace Chilano.Xbox360.Xex;

public class XexSystemFlags : XexInfoField
{
	public static byte[] Signature = new byte[4] { 0, 3, 0, 0 };

	public XexSystemFlags(uint Address)
		: base(Address)
	{
		base.Flags = true;
	}
}
