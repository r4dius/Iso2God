using System.IO;
using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Xbe;

public class XbeSection
{
	public XbeSectionHeader Header;

	public string Name;

	public byte[] Data;

	public XbeSection()
	{
	}

	public XbeSection(CBinaryReader br)
	{
		Header = new XbeSectionHeader(br);
		Name = "";
	}

	public void Read(CBinaryReader br, uint BaseAddress)
	{
		br.Seek(Header.SectionNameAddress - BaseAddress, SeekOrigin.Begin);
		while (br.PeekChar() != 0)
		{
			Name += br.ReadChar();
		}
		br.Seek(Header.RawAddress, SeekOrigin.Begin);
		Data = br.ReadBytes((int)Header.RawSize);
	}
}
