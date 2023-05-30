using Chilano.Xbox360.IO;
using System;
using System.IO;

namespace Chilano.Xbox360.Iso;

public class GDFDirEntry : ICloneable, IComparable<GDFDirEntry>
{
    public ushort SubTreeL;

    public ushort SubTreeR;

    public uint Sector;

    public uint Size;

    public GDFDirEntryAttrib Attributes;

    public byte NameLength;

    public string Name;

    public byte[] Padding;

    public GDFDirTable SubDir;

    public GDFDirTable Parent;

    public bool IsDirectory => (Attributes & GDFDirEntryAttrib.Directory) == GDFDirEntryAttrib.Directory;

    public uint EntrySize
    {
        get
        {
            if (Padding == null)
            {
                calcPadding();
            }
            return (uint)(14 + Name.Length + Padding.Length);
        }
    }

    private void calcPadding()
    {
        int num = 4 - (14 + NameLength) % 4;
        num = ((num != 4) ? num : 0);
        Padding = new byte[num];
        for (int i = 0; i < Padding.Length; i++)
        {
            Padding[i] = byte.MaxValue;
        }
    }

    public byte[] ToByteArray()
    {
        if (Padding == null)
        {
            calcPadding();
        }
        byte[] array = new byte[EntrySize];
        CBinaryWriter cBinaryWriter = new CBinaryWriter(EndianType.LittleEndian, new MemoryStream(array));
        cBinaryWriter.Write(SubTreeL);
        cBinaryWriter.Write(SubTreeR);
        cBinaryWriter.Write(Sector);
        cBinaryWriter.Write(Size);
        cBinaryWriter.Write((byte)Attributes);
        cBinaryWriter.Write(NameLength);
        cBinaryWriter.Write(Name.ToCharArray(), 0, Name.Length);
        cBinaryWriter.Write(Padding);
        cBinaryWriter.Close();
        return array;
    }

    public object Clone()
    {
        GDFDirEntry gDFDirEntry = new GDFDirEntry();
        gDFDirEntry.SubTreeL = SubTreeL;
        gDFDirEntry.SubTreeR = SubTreeR;
        gDFDirEntry.Sector = Sector;
        gDFDirEntry.Size = Size;
        gDFDirEntry.Attributes = Attributes;
        gDFDirEntry.NameLength = NameLength;
        gDFDirEntry.Name = (string)Name.Clone();
        gDFDirEntry.Padding = ((Padding == null) ? null : ((byte[])Padding.Clone()));
        gDFDirEntry.SubDir = ((SubDir == null) ? null : ((GDFDirTable)SubDir.Clone()));
        return gDFDirEntry;
    }

    public int CompareTo(GDFDirEntry Entry)
    {
        return Name.CompareTo(Entry.Name);
    }

    public override string ToString()
    {
        string text = "";
        object obj = text;
        text = string.Concat(obj, "XDFDirEntry '", Name, "' at Sector: ", Sector, ", Size: {2}\n");
        text += "---------------------------------------------------\n";
        text = text + "STL = " + SubTreeL;
        text = text + "\nSTR = " + SubTreeR;
        return text + "\nAtt = " + Attributes.ToString() + "\n\n";
    }
}
