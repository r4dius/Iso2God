using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Iso;

public class GDFDirTable : List<GDFDirEntry>, ICloneable
{
	public uint Sector;

	public uint Size;

	public GDFDirEntry Parent;

	public AVLTree Tree;

	public GDFDirTable()
	{
		Size = 2048u;
		Tree = new AVLTree();
	}

	public GDFDirTable(CBinaryReader File, GDFVolumeDescriptor Vol, uint Sector, uint Size)
	{
		this.Sector = Sector;
		this.Size = Size;
		File.Seek((long)Sector * (long)Vol.SectorSize + Vol.RootOffset, SeekOrigin.Begin);
		byte[] array = File.ReadBytes((int)Size);
		MemoryStream memoryStream = new MemoryStream(array);
		CBinaryReader cBinaryReader = new CBinaryReader(EndianType.LittleEndian, memoryStream);
		try
		{
			while (memoryStream.Position < Size)
			{
				GDFDirEntry gDFDirEntry = new GDFDirEntry
				{
					SubTreeL = cBinaryReader.ReadUInt16(),
					SubTreeR = cBinaryReader.ReadUInt16()
				};
				if (gDFDirEntry.SubTreeL != ushort.MaxValue && gDFDirEntry.SubTreeR != ushort.MaxValue)
				{
					gDFDirEntry.Sector = cBinaryReader.ReadUInt32();
					gDFDirEntry.Size = cBinaryReader.ReadUInt32();
					gDFDirEntry.Attributes = (GDFDirEntryAttrib)cBinaryReader.ReadByte();
					gDFDirEntry.NameLength = cBinaryReader.ReadByte();
					gDFDirEntry.Name = Encoding.ASCII.GetString(array, (int)memoryStream.Position, gDFDirEntry.NameLength);
					memoryStream.Seek(gDFDirEntry.NameLength, SeekOrigin.Current);
					_ = memoryStream.Position % 4;
					if (memoryStream.Position % 4 != 0)
					{
						memoryStream.Seek(4 - memoryStream.Position % 4, SeekOrigin.Current);
					}
					Add(gDFDirEntry);
				}
			}
		}
		catch (EndOfStreamException)
		{
			Console.WriteLine("EndOfStreamException while trying to read directory at sector {0} ({1} bytes)", Sector.ToString(), Size.ToString());
		}
		catch (Exception ex2)
		{
			Console.WriteLine("Unhandled Exception {0} for directory at sector {1} -> {2}", ex2.InnerException, Sector.ToString(), ex2.Message);
		}
	}

	public byte[] ToByteArray()
	{
		byte[] array = new byte[(int)(Math.Ceiling((double)Size / 2048.0) * 2048.0)];
		MemoryStream memoryStream = new MemoryStream(array);
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = byte.MaxValue;
		}
		using (Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GDFDirEntry current = enumerator.Current;
				byte[] array2 = current.ToByteArray();
				int num = (int)(2048 - memoryStream.Position % 2048);
				if (array2.Length > num)
				{
					for (int j = 0; j < num; j++)
					{
						memoryStream.WriteByte(byte.MaxValue);
					}
				}
				memoryStream.Write(array2, 0, array2.Length);
			}
		}
		memoryStream.Close();
		return array;
	}

	private void updateInOrder(GDFEntryNode Node, ref uint offset)
	{
		offset += Node.Key.EntrySize / 4u;
		if (Node.Left != null)
		{
			Node.Key.SubTreeL = (ushort)offset;
			updateInOrder(Node.Left, ref offset);
		}
		if (Node.Right != null)
		{
			Node.Key.SubTreeR = (ushort)offset;
			updateInOrder(Node.Right, ref offset);
		}
	}

	private void writeInOrder(GDFEntryNode Node, MemoryStream ms)
	{
		byte[] array = Node.Key.ToByteArray();
		ms.Write(array, 0, array.Length);
		if (Node.Left != null)
		{
			writeInOrder(Node.Left, ms);
		}
		if (Node.Right != null)
		{
			writeInOrder(Node.Right, ms);
		}
	}

	public void CalcSize()
	{
		uint num = 0u;
		using (Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GDFDirEntry current = enumerator.Current;
				num += current.EntrySize;
			}
		}
		Size = num;
		if (Parent != null)
		{
			Parent.Size = (uint)(Math.Ceiling((double)Size / 2048.0) * 2048.0);
		}
	}

	public void CreateBST()
	{
		if (base.Count == 0)
		{
			Size = 0u;
			Sector = 0u;
			return;
		}
		Sort();
		GDFDirEntry gDFDirEntry = base[(int)Math.Floor((double)base.Count / 2.0)];
		Tree.Insert(gDFDirEntry);
		List<GDFDirEntry> list = new List<GDFDirEntry>();
		using (Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GDFDirEntry current = enumerator.Current;
				list.Add(current);
			}
		}
		list.Remove(gDFDirEntry);
		Random random = new Random();
		while (list.Count > 0)
		{
			int index = random.Next(0, list.Count - 1);
			Tree.Insert(list[index]);
			list.RemoveAt(index);
		}
	}

	public object Clone()
	{
		GDFDirTable gDFDirTable = new GDFDirTable();
		gDFDirTable.Sector = Sector;
		gDFDirTable.Size = Size;
		using Enumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			GDFDirEntry current = enumerator.Current;
			GDFDirEntry gDFDirEntry = (GDFDirEntry)current.Clone();
			gDFDirEntry.Parent = gDFDirTable;
			gDFDirTable.Add(gDFDirEntry);
		}
		return gDFDirTable;
	}
}
