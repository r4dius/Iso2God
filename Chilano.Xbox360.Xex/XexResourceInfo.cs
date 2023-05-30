namespace Chilano.Xbox360.Xex;

public class XexResourceInfo : XexInfoField
{
    public static byte[] Signature = new byte[4] { 0, 0, 2, 255 };

    public XexResourceInfo(uint Address)
        : base(Address)
    {
    }
}
