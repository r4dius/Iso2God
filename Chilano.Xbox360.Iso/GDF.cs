using Chilano.Xbox360.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chilano.Xbox360.Iso;

public class GDF : IDisposable
{
    private FileStream file;

    private CBinaryReader fr;

    private GDFVolumeDescriptor volDesc;

    private GDFDirTable rootDir;

    private IsoType type;

    private uint files;

    private uint dirs;

    public List<Exception> Exceptions = new List<Exception>();

    public IsoType Type => type;

    public uint FileCount => files;

    public uint DirCount => dirs;

    public ulong RootOffset => (ulong)Type;

    public uint LastSector
    {
        get
        {
            uint lastSector = 0u;
            ParseDirectory(rootDir, recursive: true, ref lastSector);
            return lastSector;
        }
    }

    public ulong LastOffset => (ulong)LastSector * (ulong)volDesc.SectorSize;

    public GDFDirTable RootDir => rootDir;

    public GDFVolumeDescriptor VolDesc => volDesc;

    public GDF(FileStream File)
    {
        file = File;
        fr = new CBinaryReader(EndianType.LittleEndian, file);
        readVolume();
        try
        {
            rootDir = new GDFDirTable(fr, volDesc, volDesc.RootDirSector, volDesc.RootDirSize);
        }
        catch (Exception item)
        {
            Exceptions.Add(item);
        }
    }

    public void Close()
    {
        volDesc = default(GDFVolumeDescriptor);
        Exceptions.Clear();
        type = IsoType.Gdf;
        rootDir.Clear();
        fr.Close();
        file.Close();
    }

    public void Dispose()
    {
        Close();
        file.Dispose();
    }

    private void readVolume()
    {
        volDesc = default(GDFVolumeDescriptor);
        volDesc.SectorSize = 2048u;
        fr.Seek(32 * volDesc.SectorSize, SeekOrigin.Begin);
        if (Encoding.ASCII.GetString(fr.ReadBytes(20)) == "MICROSOFT*XBOX*MEDIA")
        {
            type = IsoType.Xsf;
            volDesc.RootOffset = (uint)type;
        }
        else
        {
            file.Seek(32 * volDesc.SectorSize + 265879552, SeekOrigin.Begin);
            if (Encoding.ASCII.GetString(fr.ReadBytes(20)) == "MICROSOFT*XBOX*MEDIA")
            {
                type = IsoType.Gdf;
                volDesc.RootOffset = (uint)type;
            }
            else
            {
                type = IsoType.XGD3;
                volDesc.RootOffset = (uint)type;
            }
        }
        file.Seek(32 * volDesc.SectorSize + volDesc.RootOffset, SeekOrigin.Begin);
        volDesc.Identifier = fr.ReadBytes(20);
        volDesc.RootDirSector = fr.ReadUInt32();
        volDesc.RootDirSize = fr.ReadUInt32();
        volDesc.ImageCreationTime = fr.ReadBytes(8);
        volDesc.VolumeSize = (ulong)(fr.BaseStream.Length - volDesc.RootOffset);
        volDesc.VolumeSectors = (uint)(volDesc.VolumeSize / volDesc.SectorSize);
    }

    public void ParseDirectory(GDFDirTable table, bool recursive, ref uint lastSector)
    {
        try
        {
            foreach (GDFDirEntry item2 in table)
            {
                if (item2.Sector >= lastSector)
                {
                    lastSector = item2.Sector + (uint)Math.Ceiling((double)item2.Size / (double)volDesc.SectorSize);
                }
                if (item2.IsDirectory)
                {
                    dirs++;
                    item2.SubDir = new GDFDirTable(fr, volDesc, item2.Sector, item2.Size);
                    item2.SubDir.Parent = item2;
                    if (recursive)
                    {
                        ParseDirectory(item2.SubDir, recursive: true, ref lastSector);
                    }
                }
                else
                {
                    files++;
                }
            }
        }
        catch (Exception item)
        {
            Exceptions.Add(item);
        }
    }

    public List<GDFDirEntry> GetDirContents(string Path)
    {
        List<GDFDirEntry> list = new List<GDFDirEntry>();
        try
        {
            GDFDirTable folder = GetFolder(rootDir, Path);
            if (folder != null)
            {
                foreach (GDFDirEntry item2 in folder)
                {
                    list.Add(item2);
                }
            }
        }
        catch (Exception item)
        {
            Exceptions.Add(item);
        }
        return list;
    }

    public bool Exists(string Path)
    {
        try
        {
            GDFDirTable folder = GetFolder(rootDir, Path);
            string text = (Path.Contains("\\") ? Path.Substring(Path.LastIndexOf("\\") + 1) : Path);
            foreach (GDFDirEntry item2 in folder)
            {
                if (item2.Name.ToLower() == text.ToLower())
                {
                    return true;
                }
            }
        }
        catch (Exception item)
        {
            Exceptions.Add(item);
        }
        return false;
    }

    public byte[] GetFile(string Path)
    {
        try
        {
            GDFDirTable folder = GetFolder(rootDir, Path);
            string text = (Path.Contains("\\") ? Path.Substring(Path.LastIndexOf("\\") + 1) : Path);
            foreach (GDFDirEntry item2 in folder)
            {
                if (item2.Name.ToLower() == text.ToLower())
                {
                    fr.Seek(volDesc.RootOffset + (long)item2.Sector * (long)volDesc.SectorSize, SeekOrigin.Begin);
                    return fr.ReadBytes((int)item2.Size);
                }
            }
        }
        catch (Exception item)
        {
            Exceptions.Add(item);
        }
        return new byte[0];
    }

    public long WriteFileToStream(string Path, CBinaryWriter Writer)
    {
        try
        {
            GDFDirTable folder = GetFolder(rootDir, Path);
            string text = (Path.Contains("\\") ? Path.Substring(Path.LastIndexOf("\\") + 1) : Path);
            foreach (GDFDirEntry item2 in folder)
            {
                if (!(item2.Name.ToLower() == text.ToLower()))
                {
                    continue;
                }
                fr.Seek(volDesc.RootOffset + (long)item2.Sector * (long)volDesc.SectorSize, SeekOrigin.Begin);
                uint num = (uint)Math.Ceiling((double)item2.Size / (double)volDesc.SectorSize);
                long num2 = 0L;
                for (uint num3 = 0u; num3 < num; num3++)
                {
                    if (num2 + volDesc.SectorSize > item2.Size)
                    {
                        byte[] array = fr.ReadBytes((int)(item2.Size - num2));
                        Writer.Write(array);
                        int num4 = (int)(volDesc.SectorSize - array.Length);
                        for (int i = 0; i < num4; i++)
                        {
                            Writer.Write((byte)0);
                        }
                    }
                    else
                    {
                        byte[] array = fr.ReadBytes((int)volDesc.SectorSize);
                        Writer.Write(array);
                    }
                    num2 += volDesc.SectorSize;
                }
                return item2.Size;
            }
        }
        catch (Exception item)
        {
            Exceptions.Add(item);
        }
        return -1L;
    }

    public GDFDirTable GetFolder(GDFDirTable Table, string Path)
    {
        try
        {
            if (Path.Length == 0)
            {
                return Table;
            }
            string[] array = new string[1];
            if (Path.Contains("\\"))
            {
                array = Path.Split('\\');
            }
            else
            {
                array[0] = Path;
            }
            foreach (GDFDirEntry item2 in Table)
            {
                if (!(item2.Name.ToLower() == array[0].ToLower()))
                {
                    continue;
                }
                if (item2.IsDirectory)
                {
                    if (item2.SubDir == null)
                    {
                        item2.SubDir = new GDFDirTable(fr, volDesc, item2.Sector, item2.Size);
                    }
                    if (array.Length == 1)
                    {
                        return item2.SubDir;
                    }
                    return GetFolder(item2.SubDir, Path.Substring(array[0].Length + 1));
                }
                return Table;
            }
        }
        catch (Exception item)
        {
            Exceptions.Add(item);
        }
        return null;
    }

    public void SaveFileSystem(FileStream File)
    {
        StreamWriter streamWriter = new StreamWriter(File);
        streamWriter.WriteLine("GDF File System Structure");
        streamWriter.WriteLine("-------------------------");
        streamWriter.WriteLine("Source ISO: " + file.Name);
        streamWriter.Write("\nSector\t\tSize (s)\t\tSize (b)\t\tName\n");
        streamWriter.WriteLine("---------------------------------------------------------");
        uint lastSector = 0u;
        ParseDirectory(rootDir, recursive: true, ref lastSector);
        saveFileSystemTable(streamWriter, rootDir);
        streamWriter.Flush();
    }

    private void saveFileSystemTable(StreamWriter file, GDFDirTable table)
    {
        foreach (GDFDirEntry item in table)
        {
            if (item.IsDirectory)
            {
                saveFileSystemTable(file, item.SubDir);
                continue;
            }
            file.WriteLine(item.Sector + "\t\t" + item.Size + "\t\t" + Math.Ceiling((double)item.Size / (double)volDesc.SectorSize) + "\t\t" + item.Name);
        }
    }

    public GDFStats ExamineSectors()
    {
        GDFStats gDFStats = new GDFStats(volDesc);
        for (int i = 0; i < 32; i++)
        {
            gDFStats.SetPixel((uint)i, GDFStats.GDFSectorStatus.Gdf);
        }
        gDFStats.SetPixel(volDesc.RootDirSector, GDFStats.GDFSectorStatus.DirTable);
        uint lastSector = 0u;
        ParseDirectory(rootDir, recursive: true, ref lastSector);
        calcSectors(rootDir, gDFStats);
        return gDFStats;
    }

    private void calcSectors(GDFDirTable table, GDFStats stats)
    {
        stats.UsedDirSectors += (uint)Math.Ceiling((double)table.Size / (double)stats.SectorSize);
        stats.TotalDirs++;
        foreach (GDFDirEntry item in table)
        {
            if (item.IsDirectory)
            {
                if (item.SubDir == null)
                {
                    item.SubDir = new GDFDirTable(fr, volDesc, item.Sector, item.Size);
                    item.SubDir.Parent = item;
                }
                calcSectors(item.SubDir, stats);
            }
            else
            {
                uint num = (uint)Math.Ceiling((double)item.Size / (double)stats.SectorSize);
                stats.TotalFiles++;
                stats.UsedDataSectors += num;
                stats.DataBytes += item.Size;
            }
        }
    }

    public Queue<GDFStreamEntry> GetFileSystem(GDFDirTable Root)
    {
        Queue<GDFStreamEntry> fs = new Queue<GDFStreamEntry>();
        getFileSystem(ref fs, Root, "*");
        return fs;
    }

    private void getFileSystem(ref Queue<GDFStreamEntry> fs, GDFDirTable root, string path)
    {
        foreach (GDFDirEntry item in root)
        {
            fs.Enqueue(new GDFStreamEntry(item, path + "\\" + item.Name));
            if (item.IsDirectory)
            {
                getFileSystem(ref fs, item.SubDir, path + "\\" + item.Name);
            }
        }
    }
}
