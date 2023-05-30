using System;
using System.IO;
using System.Text;

namespace Chilano.Xbox360.IO;

public class CBinaryWriter : BinaryWriter
{
	public EndianType Endian;

	public CBinaryWriter(EndianType e, Stream s)
		: base(s)
	{
		Endian = e;
	}

	public long Seek(long Offset, SeekOrigin Origin)
	{
		return base.BaseStream.Seek(Offset, Origin);
	}

	private void writeBigEndian(object data, DataType dt)
	{
		byte[] array = null;
		switch (dt)
		{
		case DataType.Double:
			array = BitConverter.GetBytes((double)data);
			break;
		case DataType.Int16:
			array = BitConverter.GetBytes((short)data);
			break;
		case DataType.Int32:
			array = BitConverter.GetBytes((int)data);
			break;
		case DataType.Int64:
			array = BitConverter.GetBytes((long)data);
			break;
		case DataType.Single:
			array = BitConverter.GetBytes((float)data);
			break;
		case DataType.UInt16:
			array = BitConverter.GetBytes((ushort)data);
			break;
		case DataType.UInt32:
			array = BitConverter.GetBytes((uint)data);
			break;
		case DataType.UInt64:
			array = BitConverter.GetBytes((ulong)data);
			break;
		}
		Array.Reverse(array);
		base.Write(array);
	}

	public void WriteInt16(short data)
	{
		if (Endian == EndianType.BigEndian)
		{
			writeBigEndian(data, DataType.Int16);
		}
		else
		{
			base.Write(data);
		}
	}

	public void WriteInt32(short data)
	{
		if (Endian == EndianType.BigEndian)
		{
			writeBigEndian(data, DataType.Int32);
		}
		else
		{
			base.Write((int)data);
		}
	}

	public void WriteInt64(long data)
	{
		if (Endian == EndianType.BigEndian)
		{
			writeBigEndian(data, DataType.Int64);
		}
		else
		{
			base.Write(data);
		}
	}

	public void WriteUint16(ushort data)
	{
		if (Endian == EndianType.BigEndian)
		{
			writeBigEndian(data, DataType.UInt16);
		}
		else
		{
			base.Write(data);
		}
	}

	public void WriteUint24(uint data)
	{
		byte[] array = new byte[4];
		MemoryStream output = new MemoryStream(array);
		BinaryWriter binaryWriter = new BinaryWriter(output);
		binaryWriter.Write(data);
		if (Endian == EndianType.BigEndian)
		{
			Array.Reverse(array);
			for (int i = 1; i < array.Length; i++)
			{
				base.Write(array[i]);
			}
		}
		else
		{
			for (int j = 0; j < array.Length - 1; j++)
			{
				base.Write(array[j]);
			}
		}
	}

	public void WriteUint32(uint data)
	{
		if (Endian == EndianType.BigEndian)
		{
			writeBigEndian(data, DataType.UInt32);
		}
		else
		{
			base.Write(data);
		}
	}

	public void WriteUint64(ulong data)
	{
		if (Endian == EndianType.BigEndian)
		{
			writeBigEndian(data, DataType.UInt64);
		}
		else
		{
			base.Write(data);
		}
	}

	public void WriteDouble(double data)
	{
		if (Endian == EndianType.BigEndian)
		{
			writeBigEndian(data, DataType.Double);
		}
		else
		{
			base.Write(data);
		}
	}

	public void WriteSingle(double data)
	{
		if (Endian == EndianType.BigEndian)
		{
			writeBigEndian(data, DataType.Single);
		}
		else
		{
			base.Write((float)data);
		}
	}

	public void WriteDecimal(decimal data)
	{
		base.Write(data);
	}

	public void WriteStringUTF16(string data)
	{
		byte[] bytes = Encoding.Unicode.GetBytes(data);
		if (Endian == EndianType.BigEndian)
		{
			for (int i = 0; i < bytes.Length; i += 2)
			{
				Array.Reverse(bytes, i, 2);
			}
			base.Write(bytes);
		}
		else
		{
			base.Write(bytes);
		}
	}
}
