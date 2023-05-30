using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Graphics;

public class DDSPixelFormat
{
	public uint Size = 32u;

	public DDSPixelFormatFlags Flags;

	public DDSPixelFormatFourCC FourCC;

	public uint RGBBitCount;

	public uint BitMaskRed;

	public uint BitMaskGreen;

	public uint BitMaskBlue;

	public uint BitMaskRGBAlpha;

	public DDSPixelFormat()
	{
	}

	public DDSPixelFormat(DDSPixelFormatFlags Flags, DDSPixelFormatFourCC FourCC, uint RGBBitCount, uint BitMaskRed, uint BitMaskGreen, uint BitMaskBlue, uint BitMaskRGBAlpha)
	{
		this.Flags = Flags;
		this.FourCC = FourCC;
		this.RGBBitCount = RGBBitCount;
		this.BitMaskRed = BitMaskRed;
		this.BitMaskGreen = BitMaskGreen;
		this.BitMaskBlue = BitMaskBlue;
		this.BitMaskRGBAlpha = BitMaskRGBAlpha;
	}

	public void Write(CBinaryWriter bw)
	{
		bw.Write(Size);
		bw.Write((uint)Flags);
		bw.Write((uint)FourCC);
		bw.Write(RGBBitCount);
		bw.Write(BitMaskRed);
		bw.Write(BitMaskGreen);
		bw.Write(BitMaskBlue);
		bw.Write(BitMaskRGBAlpha);
	}
}
