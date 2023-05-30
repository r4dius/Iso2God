using Chilano.Xbox360.IO;
using System;
using System.IO;

namespace Chilano.Xbox360.Xex;

public class XexInfo : IDisposable
{
    private byte[] data;

    private MemoryStream ms;

    private CBinaryReader br;

    public XexHeader Header;

    public bool IsValid => Header.Count > 0;

    public XexInfo(byte[] Xex)
    {
        data = Xex;
        ms = new MemoryStream(data);
        br = new CBinaryReader(EndianType.BigEndian, ms);
        Header = new XexHeader(br);
        foreach (XexInfoField value in Header.Values)
        {
            if (!value.Flags)
            {
                value.Parse(br);
            }
        }
    }

    public void Dispose()
    {
        br.Close();
        ms.Close();
    }
}
