namespace Chilano.Xbox360.Xex;

public class XexCodeOffset : XexInfoField
{
    public static byte[] Signature = new byte[4] { 0, 0, 3, 255 };

    public XexCodeOffset(uint Address)
        : base(Address)
    {
    }
}
