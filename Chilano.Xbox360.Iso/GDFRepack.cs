using Chilano.Xbox360.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chilano.Xbox360.Iso;

public class GDFRepack : IDisposable
{
    private GDF src;

    private GDFDirTable root;

    private uint freeSector = 40u;

    private string path;

    private List<GDFDirTable> tables;

    private static byte[] gdf_sector = new byte[2055]
    {
        1, 67, 68, 48, 48, 49, 1, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        23, 75, 0, 0, 0, 0, 75, 23, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 1, 1, 0, 0, 1, 0, 8,
        8, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 48, 48, 48, 48, 48, 48, 48,
        48, 48, 48, 48, 48, 48, 48, 48, 48, 0,
        48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
        48, 48, 48, 48, 48, 48, 0, 48, 48, 48,
        48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
        48, 48, 48, 0, 48, 48, 48, 48, 48, 48,
        48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
        0, 1, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 255, 67,
        68, 48, 48, 49, 1
    };

    public GDFRepack(GDF Source, string TempPath)
    {
        src = Source;
        path = TempPath;
    }

    public void Dispose()
    {
        src = null;
        root = null;
        tables.Clear();
        GC.Collect();
    }

    public void ExtractGDF()
    {
        Queue<GDFStreamEntry> fileSystem = src.GetFileSystem(src.RootDir);
        while (fileSystem.Count > 0)
        {
            GDFStreamEntry gDFStreamEntry = fileSystem.Dequeue();
            string text = gDFStreamEntry.Path.Substring(2, gDFStreamEntry.Path.Length - 2);
            if (gDFStreamEntry.Entry.IsDirectory)
            {
                if (!Directory.Exists(path + text))
                {
                    Directory.CreateDirectory(path + text);
                }
            }
            else
            {
                File.WriteAllText(path + text, gDFStreamEntry.Entry.Size.ToString());
            }
        }
    }

    public void GenerateGDF()
    {
        tables = new List<GDFDirTable>();
        root = new GDFDirTable();
        buildTables(ref root, path);
        updateTableSectors();
    }

    private void buildTables(ref GDFDirTable t, string dir)
    {
        string[] directories = Directory.GetDirectories(dir);
        string[] files = Directory.GetFiles(dir);
        t.Size = (uint)((directories.Length + files.Length) * 144);
        if (t.Parent != null)
        {
            t.Parent.Size = t.Size;
        }
        string[] array = directories;
        foreach (string text in array)
        {
            GDFDirEntry gDFDirEntry = new GDFDirEntry();
            gDFDirEntry.Name = text.Substring(text.LastIndexOf("\\") + 1);
            gDFDirEntry.NameLength = (byte)gDFDirEntry.Name.Length;
            gDFDirEntry.Attributes = GDFDirEntryAttrib.Directory;
            gDFDirEntry.SubDir = new GDFDirTable();
            gDFDirEntry.SubDir.Parent = gDFDirEntry;
            gDFDirEntry.Parent = t;
            t.Add(gDFDirEntry);
            buildTables(ref gDFDirEntry.SubDir, text);
        }
        string[] array2 = files;
        foreach (string text2 in array2)
        {
            if (!text2.EndsWith("i2g.iso"))
            {
                GDFDirEntry gDFDirEntry2 = new GDFDirEntry();
                gDFDirEntry2.Name = text2.Substring(text2.LastIndexOf("\\") + 1);
                gDFDirEntry2.NameLength = (byte)gDFDirEntry2.Name.Length;
                gDFDirEntry2.Attributes = GDFDirEntryAttrib.Normal;
                gDFDirEntry2.SubDir = null;
                gDFDirEntry2.Parent = t;
                try
                {
                    uint.TryParse(File.ReadAllText(text2, Encoding.ASCII), out gDFDirEntry2.Size);
                }
                catch (Exception)
                {
                    gDFDirEntry2.Size = 0u;
                    Console.WriteLine("Exception occured when reading file size from disk for {0}", gDFDirEntry2.Name);
                }
                t.Add(gDFDirEntry2);
            }
        }
        tables.Add(t);
    }

    private void updateTableSectors()
    {
        freeSector = 36u;
        for (int num = tables.Count - 1; num >= 0; num--)
        {
            tables[num].CalcSize();
            tables[num].Sector = freeSector;
            if (tables[num].Parent != null)
            {
                tables[num].Parent.Sector = freeSector;
            }
            freeSector += sizeToSectors(tables[num].Size);
        }
        for (int num2 = tables.Count - 1; num2 >= 0; num2--)
        {
            foreach (GDFDirEntry item in tables[num2])
            {
                if (!item.IsDirectory)
                {
                    item.Sector = freeSector;
                    freeSector += sizeToSectors(item.Size);
                }
            }
            tables[num2].CreateBST();
        }
    }

    public void WriteGDF(CBinaryWriter bw)
    {
        MemoryStream memoryStream = new MemoryStream();
        memoryStream.Seek(73728L, SeekOrigin.Begin);
        for (int num = tables.Count - 1; num >= 0; num--)
        {
            if (tables[num].Size != 0)
            {
                byte[] array = tables[num].ToByteArray();
                memoryStream.Write(array, 0, array.Length);
            }
        }
        writeGDFheader(memoryStream);
        bw.Seek(0L, SeekOrigin.Begin);
        bw.Write(memoryStream.ToArray());
        memoryStream.Close();
    }

    private void writeGDFheader(MemoryStream ms)
    {
        CBinaryWriter cBinaryWriter = new CBinaryWriter(EndianType.LittleEndian, ms);
        cBinaryWriter.Seek(0L, SeekOrigin.Begin);
        cBinaryWriter.Write(440816472u);
        cBinaryWriter.Write(1024u);
        cBinaryWriter.Seek(32768L, SeekOrigin.Begin);
        cBinaryWriter.Write(gdf_sector);
        cBinaryWriter.Seek(65536L, SeekOrigin.Begin);
        cBinaryWriter.Write(src.VolDesc.Identifier);
        cBinaryWriter.Write(tables[tables.Count - 1].Sector);
        cBinaryWriter.Write(sizeToSectors(tables[tables.Count - 1].Size) * src.VolDesc.SectorSize);
        cBinaryWriter.Write(src.VolDesc.ImageCreationTime);
        cBinaryWriter.Write((byte)1);
        cBinaryWriter.Seek(67564L, SeekOrigin.Begin);
        cBinaryWriter.Write(src.VolDesc.Identifier);
        cBinaryWriter.Close();
    }

    private void writeGDFsizes(CBinaryWriter bw)
    {
        long length = bw.BaseStream.Length;
        bw.Seek(8L, SeekOrigin.Begin);
        bw.Write(length - 1024);
        uint value = (uint)((double)length / 2048.0);
        bw.Seek(32848L, SeekOrigin.Begin);
        bw.Write(value);
        bw.Endian = EndianType.BigEndian;
        bw.Seek(32852L, SeekOrigin.Begin);
        bw.Write(value);
        bw.Endian = EndianType.LittleEndian;
    }

    public void WriteData(CBinaryWriter bw)
    {
        for (int num = tables.Count - 1; num >= 0; num--)
        {
            foreach (GDFDirEntry item in tables[num])
            {
                if (!item.IsDirectory)
                {
                    string text = "";
                    calcPath(tables[num], item, ref text);
                    if (text.StartsWith("\\"))
                    {
                        text = text.Remove(0, 1);
                    }
                    bw.Seek((long)item.Sector * (long)src.VolDesc.SectorSize, SeekOrigin.Begin);
                    src.WriteFileToStream(text, bw);
                }
            }
        }
        writeGDFsizes(bw);
    }

    private void calcPath(GDFDirTable t, GDFDirEntry e, ref string path)
    {
        if (e != null)
        {
            path = path.Insert(0, "\\" + e.Name);
            if (e.Parent != null)
            {
                calcPath(e.Parent, null, ref path);
            }
        }
        else if (t.Parent != null)
        {
            path = path.Insert(0, "\\" + t.Parent.Name);
            if (t.Parent.Parent != null)
            {
                calcPath(t.Parent.Parent, null, ref path);
            }
        }
    }

    private uint sizeToSectors(uint size)
    {
        return (uint)Math.Ceiling((double)size / (double)src.VolDesc.SectorSize);
    }
}
