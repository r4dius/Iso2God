using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Xex;

public class XexInfoField
{
	public uint Address;

	private bool flags;

	public bool Found => Address != 0;

	public bool Flags
	{
		get
		{
			return flags;
		}
		set
		{
			flags = value;
		}
	}

	public XexInfoField(uint address)
	{
		Address = address;
	}

	public virtual void Parse(CBinaryReader br)
	{
	}
}
