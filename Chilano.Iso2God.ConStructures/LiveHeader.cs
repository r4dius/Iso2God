using System.IO;

namespace Chilano.Iso2God.ConStructures;

public class LiveHeader
{
    private byte[] buffer = new byte[45056];

    private MemoryStream ms;

    private BinaryWriter bw;

    public LiveHeader()
    {
        ms = new MemoryStream(buffer);
        pad(buffer.Length);
        ms.Seek(0L, SeekOrigin.Begin);
        bw.Write((byte)76);
        bw.Write((byte)73);
        bw.Write((byte)86);
        bw.Write((byte)69);
        ms.Seek(556L, SeekOrigin.Begin);
        pad(8, byte.MaxValue);
        ms.Seek(834L, SeekOrigin.Begin);
        bw.Write(173);
        bw.Write(14);
        ms.Seek(838L, SeekOrigin.Begin);
        bw.Write(112);
        ms.Seek(843L, SeekOrigin.Begin);
        bw.Write(2);
        ms.Seek(859L, SeekOrigin.Begin);
        bw.Write(10);
        ms.Seek(863L, SeekOrigin.Begin);
        bw.Write(10);
    }

    public void WriteTitleID(string TitleID)
    {
        ms.Seek(864L, SeekOrigin.Begin);
        byte[] array = hexToBytes(TitleID);
        ms.Write(array, 0, array.Length);
    }

    public void WriteMediaID(string MediaID)
    {
        ms.Seek(852L, SeekOrigin.Begin);
        byte[] array = hexToBytes(MediaID);
        ms.Write(array, 0, array.Length);
    }

    public void WriteTitleName(string Name)
    {
    }

    private byte[] hexToBytes(string hex)
    {
        byte[] array = new byte[hex.Length];
        for (int i = 0; i < array.Length; i++)
        {
            char c = char.ToUpper(hex[i]);
            if (char.IsDigit(c))
            {
                array[i] = (byte)((byte)c - 48);
            }
            if (char.IsLetter(c))
            {
                array[i] = (byte)((byte)c - 55);
            }
        }
        return array;
    }

    private void pad(int count)
    {
        pad(count, 0);
    }

    private void pad(int count, byte value)
    {
        for (int i = 0; i < count; i++)
        {
            bw.Write(value);
        }
    }
}
