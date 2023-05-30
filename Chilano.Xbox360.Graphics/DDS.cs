using System;
using System.Drawing;
using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Graphics;

public class DDS
{
	public uint magicBytes = 542327876u;

	public DDSSurfaceDesc2 Header;

	public byte[] Data;

	public DDS(DDSType Type)
	{
		Header = new DDSSurfaceDesc2();
		switch (Type)
		{
		case DDSType.DXT1:
			Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_FOURCC;
			Header.PixelFormat.FourCC = DDSPixelFormatFourCC.DXT1;
			Header.Pitch /= 2u;
			break;
		case DDSType.DXT1a:
			Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_FOURCC;
			Header.PixelFormat.FourCC = DDSPixelFormatFourCC.DXT1;
			Header.Pitch /= 2u;
			break;
		case DDSType.DXT3:
			Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_FOURCC;
			Header.PixelFormat.FourCC = DDSPixelFormatFourCC.DXT3;
			Header.Pitch /= 2u;
			break;
		case DDSType.DXT5:
			Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_FOURCC;
			Header.PixelFormat.FourCC = DDSPixelFormatFourCC.DXT5;
			Header.Pitch /= 2u;
			break;
		case DDSType.ARGB:
			Header.PixelFormat.Flags |= (DDSPixelFormatFlags)65u;
			Header.PixelFormat.RGBBitCount = 32u;
			Header.PixelFormat.BitMaskRed = 16711680u;
			Header.PixelFormat.BitMaskGreen = 65280u;
			Header.PixelFormat.BitMaskBlue = 255u;
			Header.PixelFormat.BitMaskRGBAlpha = 4278190080u;
			Header.Pitch *= 4u;
			break;
		case DDSType.b1555:
			Header.PixelFormat.Flags |= (DDSPixelFormatFlags)65u;
			Header.PixelFormat.RGBBitCount = 16u;
			Header.PixelFormat.BitMaskRed = 31744u;
			Header.PixelFormat.BitMaskGreen = 992u;
			Header.PixelFormat.BitMaskBlue = 31u;
			Header.PixelFormat.BitMaskRGBAlpha = 32768u;
			break;
		case DDSType.b4444:
		case DDSType.b565:
			break;
		}
	}

	public void SetDetails(int Height, int Width, uint MipMapCount)
	{
		Header.Height = Height;
		Header.Width = Width;
		Header.Pitch = (uint)(Height * Width);
		Header.MipMapCount = MipMapCount;
		Header.Caps.Caps1 |= (DDSCaps1Flags)((MipMapCount > 1) ? 4194304 : 4096);
	}

	public void Write(CBinaryWriter bw)
	{
		bw.Endian = EndianType.LittleEndian;
		bw.WriteUint32(magicBytes);
		Header.Write(bw);
		if (Data != null)
		{
			bw.Write(Data);
		}
	}

	public Image GetImage(DDSType Type)
	{
		Image image = new Bitmap(Header.Width, Header.Height);
		switch (Type)
		{
		case DDSType.DXT1:
			blockDecompressImageDXT1((ulong)Header.Width, (ulong)Header.Height, Data, image);
			break;
		case DDSType.ARGB:
			rgbaDecompressImage(image);
			break;
		}
		return image;
	}

	private void rgbaDecompressImage(Image img)
	{
		int width = Header.Width;
		int height = Header.Height;
		int num = 0;
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				Color color = Color.FromArgb(Data[num + 3], Data[num + 2], Data[num + 1], Data[num]);
				((Bitmap)img).SetPixel(j, i, color);
				num += 4;
			}
		}
	}

	private void blockDecompressImageDXT1(ulong width, ulong height, byte[] data, Image img)
	{
		ulong num = (width + 3) / 4uL;
		ulong num2 = (height + 3) / 4uL;
		long num3 = 0L;
		for (ulong num4 = 0uL; num4 < num2; num4++)
		{
			for (ulong num5 = 0uL; num5 < num; num5++)
			{
				byte[] array = new byte[8];
				Array.Copy(data, num3, array, 0L, 8L);
				num3 += 8;
				decompressBlockDXT1(num5 * 4, num4 * 4, width, array, img);
			}
		}
	}

	private void decompressBlockDXT1(ulong x, ulong y, ulong width, byte[] blockStorage, Image img)
	{
		ushort num = BitConverter.ToUInt16(blockStorage, 0);
		ushort num2 = BitConverter.ToUInt16(blockStorage, 2);
		ulong num3 = (ulong)((num >> 11) * 255 + 16);
		byte b = (byte)((num3 / 32uL + num3) / 32uL);
		num3 = (ulong)(((num & 0x7E0) >> 5) * 255 + 32);
		byte b2 = (byte)((num3 / 64uL + num3) / 64uL);
		num3 = (ulong)((num & 0x1F) * 255 + 16);
		byte b3 = (byte)((num3 / 32uL + num3) / 32uL);
		num3 = (ulong)((num2 >> 11) * 255 + 16);
		byte b4 = (byte)((num3 / 32uL + num3) / 32uL);
		num3 = (ulong)(((num2 & 0x7E0) >> 5) * 255 + 32);
		byte b5 = (byte)((num3 / 64uL + num3) / 64uL);
		num3 = (ulong)((num2 & 0x1F) * 255 + 16);
		byte b6 = (byte)((num3 / 32uL + num3) / 32uL);
		ulong num4 = BitConverter.ToUInt32(blockStorage, 4);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				Color color = default(Color);
				byte b7 = (byte)((num4 >> 2 * (4 * i + j)) & 3);
				if (num > num2)
				{
					switch (b7)
					{
					case 0:
						color = PackRGBA(b, b2, b3, byte.MaxValue);
						break;
					case 1:
						color = PackRGBA(b4, b5, b6, byte.MaxValue);
						break;
					case 2:
						color = PackRGBA((byte)((2 * b + b4) / 3), (byte)((2 * b2 + b5) / 3), (byte)((2 * b3 + b6) / 3), byte.MaxValue);
						break;
					case 3:
						color = PackRGBA((byte)((b + 2 * b4) / 3), (byte)((b2 + 2 * b5) / 3), (byte)((b3 + 2 * b6) / 3), byte.MaxValue);
						break;
					}
				}
				else
				{
					switch (b7)
					{
					case 0:
						color = PackRGBA(b, b2, b3, byte.MaxValue);
						break;
					case 1:
						color = PackRGBA(b4, b5, b6, byte.MaxValue);
						break;
					case 2:
						color = PackRGBA((byte)((b + b4) / 2), (byte)((b2 + b5) / 2), (byte)((b3 + b6) / 2), byte.MaxValue);
						break;
					case 3:
						color = PackRGBA(0, 0, 0, byte.MaxValue);
						break;
					}
				}
				if ((ulong)((long)x + (long)j) < width)
				{
					((Bitmap)img).SetPixel((int)x + j, (int)y + i, color);
				}
			}
		}
	}

	private Color PackRGBA(byte r, byte g, byte b, byte a)
	{
		return Color.FromArgb(a, r, g, b);
	}
}
