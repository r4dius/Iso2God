using System.Collections.Generic;
using System.IO;

namespace Chilano.Iso2God.ConStructures;

public class MasterHashtable : List<byte[]>
{
    public MasterHashtable()
        : base(204)
    {
    }

    public void Write(FileStream f)
    {
        BinaryWriter binaryWriter = new BinaryWriter(f);
        using Enumerator enumerator = GetEnumerator();
        while (enumerator.MoveNext())
        {
            byte[] current = enumerator.Current;
            binaryWriter.Write(current, 0, current.Length);
        }
    }

    public void WriteBlank(FileStream f)
    {
        BinaryWriter binaryWriter = new BinaryWriter(f);
        for (int i = 0; i < 4096; i++)
        {
            binaryWriter.Write((byte)0);
        }
    }

    public byte[] ToByteArray()
    {
        byte[] array = new byte[4096];
        uint num = 0u;
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = 0;
        }
        using Enumerator enumerator = GetEnumerator();
        while (enumerator.MoveNext())
        {
            byte[] current = enumerator.Current;
            current.CopyTo(array, num);
            num += 20;
        }
        return array;
    }

    public void ReadMHT(FileStream f)
    {
        byte[] array = new byte[4096];
        f.Read(array, 0, array.Length);
        for (int i = 0; i < 204; i++)
        {
            uint num = 0u;
            byte[] array2 = new byte[20];
            for (int j = 0; j < 20; j++)
            {
                array2[j] = array[i * 20 + j];
                num += array2[j];
            }
            if (num != 0)
            {
                Add(array2);
                continue;
            }
            break;
        }
    }
}
