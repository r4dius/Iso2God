using System.Text;

namespace Chilano.Xbox360.IO;

public static class DataConversion
{
    public static string BytesToHexString(byte[] value)
    {
        StringBuilder stringBuilder = new StringBuilder(value.Length * 2);
        foreach (byte b in value)
        {
            stringBuilder.Append(b.ToString("X02"));
        }
        return stringBuilder.ToString();
    }
}
