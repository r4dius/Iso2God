using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Xdbf;

public class XdbfTableEntry
{
	public uint Identifier;

	public uint Offset;

	public uint Size;

	public ushort Type;

	public uint Padding;

	public XdbfTableEntry(CBinaryReader b)
	{
		Identifier = b.ReadUInt32();
		Offset = b.ReadUInt32();
		Size = b.ReadUInt32();
		Type = b.ReadUInt16();
		Padding = b.ReadUInt32();
	}

	public override string ToString()
	{
		string text = "XdbfTableEntry: { ";
		text = text + "Identifier = " + Identifier;
		text = text + ", Offset = " + Offset;
		text = text + ", Size = " + Size;
		text = text + ", Type = " + Type;
		return text + " }";
	}
}
