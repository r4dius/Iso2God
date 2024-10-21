using Chilano.Iso2God.ConHeader;
using Chilano.Iso2God.ConStructures;
using Chilano.Xbox360.IO;
using Chilano.Xbox360.Iso;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Chilano.Iso2God;

public class Iso2God : BackgroundWorker
{
    private uint blockSize = 4096u;

    private uint shtPerMHT = 203u;

    private uint blockPerSHT = 204u;

    private uint blockPerPart = 41412u;

    private DateTime Start;

    private DateTime Finish;

    private float progress;

    private string uniqueName = "";

    private SHA1Managed sha1 = new SHA1Managed();

    private GDFDirTable rootDir;

    private uint freeSector = 36u;

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

    public event Iso2GodProgressEventHandler Progress;

    public event Iso2GodCompletedEventHandler Completed;

    public Iso2God()
    {
        base.WorkerReportsProgress = true;
        base.WorkerSupportsCancellation = false;
        base.ProgressChanged += Iso2God_ProgressChanged;
        base.RunWorkerCompleted += Iso2God_RunWorkerCompleted;
        base.DoWork += Iso2God_Run;
    }

    protected virtual void OnProgressChange(Iso2GodProgressArgs e)
    {
        if (this.Progress != null)
        {
            this.Progress(this, e);
        }
    }

    private void Iso2God_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        OnProgressChange(new Iso2GodProgressArgs(e));
    }

    protected virtual void OnCompleted(Iso2GodCompletedArgs e)
    {
        if (this.Completed != null)
        {
            this.Completed(this, e);
        }
    }

    private void Iso2God_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        OnCompleted(new Iso2GodCompletedArgs(e, uniqueName));
    }

    private void Iso2God_Run(object sender, DoWorkEventArgs e)
    {
        Start = DateTime.Now;
        IsoEntry iso = (IsoEntry)e.Argument;
        uniqueName = createUniqueName(iso);
        switch (iso.Options.Padding)
        {
            case IsoEntryPaddingRemoval.None:
            case IsoEntryPaddingRemoval.Partial:
                Iso2God_Partial(sender, e, iso);
                break;
            case IsoEntryPaddingRemoval.Full:
                Iso2God_Full(sender, e);
                break;
        }
    }

    private string createUniqueName(IsoEntry Iso)
    {
        MemoryStream memoryStream = new MemoryStream();
        CBinaryWriter cBinaryWriter = new CBinaryWriter(EndianType.LittleEndian, memoryStream);
        cBinaryWriter.Write(Iso.ID.TitleID);
        cBinaryWriter.Write(Iso.ID.MediaID);
        cBinaryWriter.Write(Iso.ID.DiscNumber);
        cBinaryWriter.Write(Iso.ID.DiscCount);
        byte[] array = sha1.ComputeHash(memoryStream.ToArray());
        string text = "";
        for (int i = 0; i < array.Length / 2; i++)
        {
            text += array[i].ToString("X02");
        }
        return text;
    }

    private void Iso2God_Full(object sender, DoWorkEventArgs e)
    {
        ReportProgress((int)progress, "Preparing to rebuild ISO image...");
        IsoEntry iso = (IsoEntry)e.Argument;
        FileStream fileStream;
        try
        {
            fileStream = new FileStream(iso.Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
        catch (Exception ex)
        {
            ReportProgress(0, "Error! " + ex.Message);
            return;
        }
        FileStream fileStream2;
        using (GDF gDF = new GDF(fileStream))
        {
            uint lastSector = 0u;
            gDF.ParseDirectory(gDF.RootDir, recursive: true, ref lastSector);
            ReportProgress((int)progress, "Generating new GDFS structures...");
            try
            {
                fileStream2 = File.OpenWrite(iso.Options.IsoPath + iso.Path.Substring(iso.Path.LastIndexOf(Path.DirectorySeparatorChar) + 1) + "_rebuilt.iso");
            }
            catch (Exception ex2)
            {
                ReportProgress(0, "Error rebuilding GDF! " + ex2.Message);
                return;
            }
            rootDir = (GDFDirTable)gDF.RootDir.Clone();
            RemapSectors(gDF);
            WriteGDF(gDF, fileStream2);
            WriteFiles(gDF, fileStream2);
        }
        if (fileStream2.Length <= 0)
        {
            ReportProgress(100, "Failed to rebuild ISO. Aborting.");
            return;
        }
        ReportProgress((int)progress, "ISO image rebuilt.");
        iso.Path = fileStream2.Name;
        iso.Size = fileStream2.Length;
        fileStream2.Close();
        fileStream.Close();
        if (iso.Options.Format != IsoEntryFormat.Iso)
        {
            Iso2God_Partial(sender, e, iso);
        }
        else
        {
            Finish = DateTime.Now;
            TimeSpan timeSpan = Finish - Start;
            ReportProgress(100, "Done!");
            e.Result = "Finished in " + timeSpan.Minutes + "m" + timeSpan.Seconds + "s. ISO image rebuilt";
            GC.Collect();
        }
    }

    private void Iso2God_Partial(object sender, DoWorkEventArgs e, IsoEntry iso)
    {
        ReportProgress((int)progress, "Examining ISO image...");
        FileStream fileStream;
        try
        {
            fileStream = new FileStream(iso.Path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        catch (Exception)
        {
            ReportProgress(0, "Cannot access the ISO image because it is being accessed by another application.");
            return;
        }
        GDF gDF;
        try
        {
            gDF = new GDF(fileStream);
        }
        catch (Exception ex2)
        {
            ReportProgress(0, "Error while parsing GDF: " + ex2.Message);
            return;
        }
        ulong num = 0uL;
        num = ((iso.Options.Padding != IsoEntryPaddingRemoval.Partial) ? ((ulong)iso.Size - gDF.RootOffset) : ((ulong)(iso.Size - (long)gDF.RootOffset - (iso.Size - (long)(gDF.LastOffset + gDF.RootOffset)))));
        uint num2 = (uint)Math.Ceiling((double)num / (double)blockSize);
        uint num3 = (uint)Math.Ceiling((double)num2 / (double)blockPerPart);
        ContentType contentType = ((iso.Platform == IsoEntryPlatform.Xbox360) ? ContentType.GamesOnDemand : ContentType.XboxOriginal);
        string gameDirectory = iso.ID.TitleID;
        if (iso.FolderLayout > 0 && Utils.sanitizePath(iso.TitleName).Length != 0)
        {
            switch (iso.FolderLayout)
            {
                case 1:
                    gameDirectory = Utils.sanitizePath(iso.TitleName) + Path.DirectorySeparatorChar + iso.ID.TitleID;
                    break;
                case 2:
                    gameDirectory = Utils.sanitizePath(iso.TitleName) + " " + iso.ID.TitleID;
                    break;
            }
        }
        object[] array = new object[6]
        {
            iso.Destination,
            gameDirectory,
            Path.DirectorySeparatorChar,
            "0000",
            null,
            null
        };
        uint num4 = (uint)contentType;
        array[4] = num4.ToString("X02");
        array[5] = Path.DirectorySeparatorChar;
        string text = string.Concat(array);
        text += ((uniqueName != null) ? uniqueName : iso.ID.TitleID);
        text += ".data";
        if (Directory.Exists(text))
        {
            Directory.Delete(text, recursive: true);
        }
        Directory.CreateDirectory(text);
        ReportProgress((int)progress, "Beginning ISO conversion...");
        fileStream.Seek((long)gDF.RootOffset, SeekOrigin.Begin);
        writeParts(fileStream, text, iso, num3, num2);
        ReportProgress((int)progress, "Calculating Master Hash Table chain...");
        byte[] lastMhtHash = new byte[20];
        uint lastPartSize = 0u;
        calcMhtHashChain(text, num3, out lastPartSize, out lastMhtHash);
        ulong num5 = 41616uL;
        ulong num6 = blockSize * num5;
        ulong sizeParts = lastPartSize + (num3 - 1) * num6;
        ReportProgress(95, "Creating LIVE header...");
        createConHeader(text.Substring(0, text.Length - 5), iso, num2, 0, num3, sizeParts, lastMhtHash);
        fileStream.Close();
        fileStream.Dispose();
        gDF.Dispose();
        if (iso.Options.Padding == IsoEntryPaddingRemoval.Full && iso.Options.Format == IsoEntryFormat.God)
        {
            try
            {
                File.Delete(iso.Path);
            }
            catch (Exception)
            {
                ReportProgress(95, "Unable to delete ISO temporary image.");
            }
        }
        Finish = DateTime.Now;
        TimeSpan timeSpan = Finish - Start;
        ReportProgress(100, "Done!");
        e.Result = "Finished in " + timeSpan.Minutes + "m" + timeSpan.Seconds + "s. GOD package written to: " + text;
        GC.Collect();
    }

    private void writeParts(FileStream src, string destPath, IsoEntry iso, uint partsReq, uint blocksReq)
    {
        uint num = 0u;
        for (uint num2 = 0u; num2 < partsReq; num2++)
        {
            progress += 1f / (float)partsReq * ((iso.Options.Padding == IsoEntryPaddingRemoval.Full) ? 0.45f : 0.9f) * 100f;
            ReportProgress((int)progress, "Writing Part " + num2 + " / " + partsReq + "...");
            string text = destPath + Path.DirectorySeparatorChar + "Data";
            if (num2 < 10)
            {
                text = text + "000" + num2;
            }
            else if (num2 < 100)
            {
                text = text + "00" + num2;
            }
            else if (num2 < 1000)
            {
                text = text + "0" + num2;
            }
            else if (num2 < 10000)
            {
                text += num2;
            }
            FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            MasterHashtable masterHashtable = new MasterHashtable();
            masterHashtable.WriteBlank(fileStream);
            for (int i = 0; i < shtPerMHT; i++)
            {
                SubHashTable subHashTable = new SubHashTable();
                subHashTable.WriteBlank(fileStream);
                uint num3 = 0u;
                while (num < blocksReq && num3 < blockPerSHT)
                {
                    byte[] array = new byte[blockSize];
                    src.Read(array, 0, array.Length);
                    byte[] array2 = new byte[20];
                    array2 = sha1.ComputeHash(array, 0, (int)blockSize);
                    subHashTable.Add(array2);
                    fileStream.Write(array, 0, array.Length);
                    num++;
                    num3++;
                }
                long position = fileStream.Position;
                fileStream.Seek(0L - (long)((num3 + 1) * blockSize), SeekOrigin.Current);
                subHashTable.Write(fileStream);
                fileStream.Seek(position, SeekOrigin.Begin);
                byte[] array3 = new byte[20];
                array3 = sha1.ComputeHash(subHashTable.ToByteArray(), 0, (int)blockSize);
                masterHashtable.Add(array3);
                if (num >= blocksReq)
                {
                    break;
                }
            }
            fileStream.Seek(0L, SeekOrigin.Begin);
            masterHashtable.Write(fileStream);
            fileStream.Close();
            if (num >= blocksReq)
            {
                break;
            }
        }
    }

    private void calcMhtHashChain(string Destination, uint TotalPartsReq, out uint lastPartSize, out byte[] lastMhtHash)
    {
        lastPartSize = 0u;
        lastMhtHash = new byte[20];
        for (uint num = TotalPartsReq - 1; num != 0; num--)
        {
            string text = Destination + Path.DirectorySeparatorChar + "Data";
            if (num < 10)
            {
                text = text + "000" + num;
            }
            else if (num < 100)
            {
                text = text + "00" + num;
            }
            else if (num < 1000)
            {
                text = text + "0" + num;
            }
            else if (num < 10000)
            {
                text += num;
            }
            string text2 = Destination + Path.DirectorySeparatorChar + "Data";
            if (num - 1 < 10)
            {
                text2 = text2 + "000" + (num - 1);
            }
            else if (num - 1 < 100)
            {
                text2 = text2 + "00" + (num - 1);
            }
            else if (num - 1 < 1000)
            {
                text2 = text2 + "0" + (num - 1);
            }
            else if (num - 1 < 10000)
            {
                text2 += num - 1;
            }
            FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.None);
            FileStream fileStream2 = new FileStream(text2, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            if (num == TotalPartsReq - 1)
            {
                lastPartSize = (uint)fileStream.Length;
            }
            MasterHashtable masterHashtable = new MasterHashtable();
            masterHashtable.ReadMHT(fileStream);
            byte[] array = new byte[blockSize];
            masterHashtable.ToByteArray().CopyTo(array, 0);
            byte[] array2 = new byte[20];
            array2 = sha1.ComputeHash(array);
            MasterHashtable masterHashtable2 = new MasterHashtable();
            masterHashtable2.ReadMHT(fileStream2);
            masterHashtable2.Add(array2);
            fileStream2.Seek(0L, SeekOrigin.Begin);
            masterHashtable2.Write(fileStream2);
            if (num - 1 == 0)
            {
                lastMhtHash = sha1.ComputeHash(masterHashtable2.ToByteArray());
            }
            fileStream.Close();
            fileStream2.Close();
        }
    }

    private void createConHeader(string path, IsoEntry iso, uint blocksAllocated, ushort blocksNotAllocated, uint totalParts, ulong sizeParts, byte[] mhtHash)
    {
        ConHeaderWriter conHeaderWriter = new ConHeaderWriter();
        conHeaderWriter.WriteIDs(iso.ID.TitleID, iso.ID.MediaID, iso.TitleName);
        conHeaderWriter.WriteExecutionDetails(iso.ID.DiscNumber, iso.ID.DiscCount, iso.ID.Platform, iso.ID.ExType);
        conHeaderWriter.WriteBlockCounts(blocksAllocated, blocksNotAllocated);
        conHeaderWriter.WriteDataPartsInfo(totalParts, sizeParts);
        conHeaderWriter.WriteGameIcon(iso.Thumb);
        switch (iso.Platform)
        {
            case IsoEntryPlatform.Xbox:
                conHeaderWriter.WriteContentType(ContentType.XboxOriginal);
                break;
            case IsoEntryPlatform.Xbox360:
                conHeaderWriter.WriteContentType(ContentType.GamesOnDemand);
                break;
        }
        conHeaderWriter.WriteMhtHash(mhtHash);
        conHeaderWriter.WriteHash();
        conHeaderWriter.Write(path);
    }

    public void RemapSectors(GDF src)
    {
        if (rootDir != null)
        {
            rootDir.Sector = freeSector;
            rootDir.Size = (uint)sectorsToSize(src, sizeToSectors(src, rootDir.Size));
            freeSector += sizeToSectors(src, rootDir.Size);
            remapDirs(src, rootDir);
            remapFiles(src, rootDir);
        }
    }

    private void remapDirs(GDF src, GDFDirTable table)
    {
        foreach (GDFDirEntry item in table)
        {
            if (item.IsDirectory)
            {
                if (item.SubDir == null)
                {
                    item.Sector = 0u;
                    item.Size = 0u;
                    continue;
                }
                item.Sector = freeSector;
                item.SubDir.Sector = freeSector;
                item.SubDir.Parent = item;
                freeSector += sizeToSectors(src, item.Size);
                ReportProgress(0, "Remapped '" + item.Name + "' (" + sizeToSectors(src, item.Size) + " sectors) to Sector 0x" + item.Sector.ToString("X02"));
                remapDirs(src, item.SubDir);
            }
        }
    }

    private void remapFiles(GDF src, GDFDirTable table)
    {
        foreach (GDFDirEntry item in table)
        {
            if (!item.IsDirectory)
            {
                item.Sector = freeSector;
                freeSector += sizeToSectors(src, item.Size);
            }
        }
        foreach (GDFDirEntry item2 in table)
        {
            if (item2.IsDirectory && item2.SubDir != null)
            {
                remapFiles(src, item2.SubDir);
            }
        }
    }

    private uint sizeToSectors(GDF src, uint size)
    {
        return (uint)Math.Ceiling((double)size / (double)src.VolDesc.SectorSize);
    }

    private long sectorsToSize(GDF src, uint sectors)
    {
        return (long)sectors * (long)src.VolDesc.SectorSize;
    }

    public void WriteGDF(GDF src, FileStream iso)
    {
        CBinaryWriter bw = new CBinaryWriter(EndianType.LittleEndian, iso);
        ReportProgress(0, "Writing new GDF header...");
        writeGDFheader(src, bw);
        ReportProgress(0, "Writing new GDF directories...");
        writeGDFtable(src, bw, rootDir);
    }

    private void writeGDFtable(GDF src, CBinaryWriter bw, GDFDirTable table)
    {
        bw.Seek((long)table.Sector * (long)src.VolDesc.SectorSize, SeekOrigin.Begin);
        byte[] buffer = table.ToByteArray();
        bw.Write(buffer);
        foreach (GDFDirEntry item in table)
        {
            if (item.IsDirectory && item.SubDir != null)
            {
                writeGDFtable(src, bw, item.SubDir);
            }
        }
    }

    private void writeGDFheader(GDF src, CBinaryWriter bw)
    {
        bw.Seek(0L, SeekOrigin.Begin);
        bw.Write(440816472u);
        bw.Write(1024u);
        bw.Seek(32768L, SeekOrigin.Begin);
        bw.Write(gdf_sector);
        bw.Seek(65536L, SeekOrigin.Begin);
        bw.Write(src.VolDesc.Identifier);
        bw.Write(rootDir.Sector);
        bw.Write(sizeToSectors(src, rootDir.Size) * src.VolDesc.SectorSize);
        bw.Write(src.VolDesc.ImageCreationTime);
        bw.Write((byte)1);
        bw.Seek(67564L, SeekOrigin.Begin);
        bw.Write(src.VolDesc.Identifier);
    }

    public void WriteFiles(GDF src, FileStream Iso)
    {
        CBinaryWriter bw = new CBinaryWriter(EndianType.LittleEndian, Iso);
        ReportProgress(0, "Writing file data to new ISO image...");
        writeFiles(src, bw, rootDir);
        writeGDFsizes(bw);
    }

    private void writeFiles(GDF src, CBinaryWriter bw, GDFDirTable table)
    {
        uint fileCount = src.FileCount;
        foreach (GDFDirEntry item in table)
        {
            if (!item.IsDirectory)
            {
                bw.Seek((long)item.Sector * (long)src.VolDesc.SectorSize, SeekOrigin.Begin);
                string path = "";
                calcPath(table, item, ref path);
                if (path.StartsWith("\\"))
                {
                    path = path.Remove(0, 1);
                }
                progress += 1f / (float)fileCount * 0.45f * 100f;
                ReportProgress((int)progress, "Writing '" + path + "' at Sector 0x" + item.Sector.ToString("X02") + "...");
                src.WriteFileToStream(path, bw);
            }
        }
        foreach (GDFDirEntry item2 in table)
        {
            if (item2.IsDirectory && item2.SubDir != null)
            {
                writeFiles(src, bw, item2.SubDir);
            }
        }
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
}
