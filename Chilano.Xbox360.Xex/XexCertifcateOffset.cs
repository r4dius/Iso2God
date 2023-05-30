namespace Chilano.Xbox360.Xex;

public class XexCertifcateOffset : XexInfoField
{
	public static byte[] Signature = new byte[4] { 0, 0, 3, 255 };

	public XexCertifcateOffset(uint Address)
		: base(Address)
	{
	}
}
