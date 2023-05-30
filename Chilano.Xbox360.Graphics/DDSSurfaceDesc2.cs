using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Graphics;

public class DDSSurfaceDesc2
{
	public uint Size = 124u;

	public DDSSurfaceDescriptionFlags Flags;

	public int Height;

	public int Width;

	public uint Pitch;

	public uint Depth;

	public uint MipMapCount;

	public byte[] Reserved = new byte[44];

	public DDSPixelFormat PixelFormat;

	public DDSCaps2 Caps;

	public uint Reserved2;

	public DDSSurfaceDesc2()
	{
		Flags = (DDSSurfaceDescriptionFlags)659463u;
		PixelFormat = new DDSPixelFormat();
		Caps = new DDSCaps2();
	}

	public DDSSurfaceDesc2(DDSSurfaceDescriptionFlags Flags, int Height, int Width, uint Pitch, uint Depth, uint MipMapCount)
	{
		this.Flags = Flags;
		this.Height = Height;
		this.Width = Width;
		this.Pitch = Pitch;
		this.Depth = Depth;
		this.MipMapCount = MipMapCount;
		PixelFormat = new DDSPixelFormat();
		Caps = new DDSCaps2();
	}

	public DDSSurfaceDesc2(DDSSurfaceDescriptionFlags Flags, int Height, int Width, uint Pitch, uint Depth, uint MipMapCount, DDSPixelFormat PixelFormat, DDSCaps2 Caps)
	{
		this.Flags = Flags;
		this.Height = Height;
		this.Width = Width;
		this.Pitch = Pitch;
		this.Depth = Depth;
		this.MipMapCount = MipMapCount;
		this.PixelFormat = PixelFormat;
		this.Caps = Caps;
	}

	public void Write(CBinaryWriter bw)
	{
		bw.Write(Size);
		bw.Write((uint)Flags);
		bw.Write(Height);
		bw.Write(Width);
		bw.Write(Depth);
		bw.Write(Pitch);
		bw.Write(MipMapCount);
		bw.Write(Reserved);
		PixelFormat.Write(bw);
		Caps.Write(bw);
		bw.Write(Reserved2);
	}
}
