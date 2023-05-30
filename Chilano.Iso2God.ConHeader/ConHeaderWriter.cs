using Chilano.Iso2God.ConStructures;
using Chilano.Iso2God.Properties;
using Chilano.Xbox360.IO;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Chilano.Iso2God.ConHeader;

public class ConHeaderWriter
{
    private byte[] buffer;

    private MemoryStream header;

    private CBinaryWriter bw;

    public ConHeaderWriter()
    {
        buffer = new byte[Resources.emptyLIVE.Length];
        Resources.emptyLIVE.CopyTo(buffer, 0);
        header = new MemoryStream(buffer);
        bw = new CBinaryWriter(EndianType.BigEndian, header);
    }

    public void Close()
    {
        bw.Close();
    }

    public void Write(string FilePath)
    {
        bw.Seek(859L, SeekOrigin.Begin);
        bw.Write((byte)0);
        bw.Seek(863L, SeekOrigin.Begin);
        bw.Write((byte)0);
        bw.Seek(913L, SeekOrigin.Begin);
        bw.Write((byte)0);
        WriteHash();
        FileStream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        fileStream.Write(buffer, 0, buffer.Length);
        fileStream.Close();
    }

    public void WriteIDs(string TitleID, string MediaID, string GameTitle)
    {
        header.Seek(864L, SeekOrigin.Begin);
        bw.Write(hexStringToBytes(TitleID));
        header.Seek(852L, SeekOrigin.Begin);
        bw.Write(hexStringToBytes(MediaID));
        byte[] bytes = Encoding.Unicode.GetBytes(GameTitle);
        for (int i = 0; i < bytes.Length; i += 2)
        {
            byte b = bytes[i];
            bytes[i] = bytes[i + 1];
            bytes[i + 1] = b;
        }
        header.Seek(1041L, SeekOrigin.Begin);
        header.Write(bytes, 0, bytes.Length);
        header.Seek(5777L, SeekOrigin.Begin);
        header.Write(bytes, 0, bytes.Length);
    }

    public void WriteExecutionDetails(byte DiscNumber, byte DiscCount, byte Platform, byte ExType)
    {
        header.Seek(868L, SeekOrigin.Begin);
        bw.Write(Platform);
        header.Seek(869L, SeekOrigin.Begin);
        bw.Write(ExType);
        header.Seek(870L, SeekOrigin.Begin);
        bw.Write(DiscNumber);
        header.Seek(871L, SeekOrigin.Begin);
        bw.Write(DiscCount);
    }

    public void WriteMhtHash(byte[] hash)
    {
        header.Seek(893L, SeekOrigin.Begin);
        header.Write(hash, 0, hash.Length);
    }

    public void WriteBlockCounts(uint TotalNumberToBeStored, ushort TotalNumberNotAllocated)
    {
        header.Seek(914L, SeekOrigin.Begin);
        bw.WriteUint24(TotalNumberToBeStored);
        header.Seek(917L, SeekOrigin.Begin);
        bw.WriteUint16(TotalNumberNotAllocated);
    }

    public void WriteDataPartsInfo(uint NumberParts, ulong SizeOfDataParts)
    {
        bw.Seek(928L, SeekOrigin.Begin);
        bw.Endian = EndianType.LittleEndian;
        bw.WriteUint32(NumberParts);
        bw.Seek(932L, SeekOrigin.Begin);
        bw.Endian = EndianType.BigEndian;
        bw.WriteUint32((uint)(SizeOfDataParts / 256uL));
    }

    public void WriteGameIcon(byte[] PngData)
    {
        if (PngData == null)
        {
            PngData = new byte[20];
        }
        uint data = (uint)PngData.Length;
        bw.Seek(5906L, SeekOrigin.Begin);
        bw.WriteUint32(data);
        bw.Seek(5910L, SeekOrigin.Begin);
        bw.WriteUint32(data);
        bw.Seek(5914L, SeekOrigin.Begin);
        bw.Write(PngData);
        bw.Seek(22298L, SeekOrigin.Begin);
        bw.Write(PngData);
    }

    public void WriteContentType(ContentType Type)
    {
        bw.Seek(836L, SeekOrigin.Begin);
        bw.Endian = EndianType.BigEndian;
        bw.WriteUint32((uint)Type);
    }

    public void WriteHash()
    {
        SHA1Managed sHA1Managed = new SHA1Managed();
        byte[] array = sHA1Managed.ComputeHash(buffer, 836, 44220);
        bw.Seek(812L, SeekOrigin.Begin);
        bw.Write(array);
    }

    private byte[] hexStringToBytes(string value)
    {
        int num = value.Length / 2;
        byte[] array = new byte[num];
        for (int i = 0; i < num; i++)
        {
            array[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
        }
        return array;
    }

    private string bytesToHexString(byte[] value)
    {
        StringBuilder stringBuilder = new StringBuilder(value.Length * 2);
        foreach (byte b in value)
        {
            stringBuilder.Append(b.ToString("X02"));
        }
        return stringBuilder.ToString();
    }
}
