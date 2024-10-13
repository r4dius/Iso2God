using Chilano.Xbox360.Graphics;
using Chilano.Xbox360.IO;
using Chilano.Xbox360.Iso;
using Chilano.Xbox360.Xbe;
using Chilano.Xbox360.Xdbf;
using Chilano.Xbox360.Xex;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Chilano.Iso2God;

internal class IsoDetails : BackgroundWorker
{
    private IsoDetailsArgs args;

    private FileStream f;

    private GDF iso;

    public IsoDetails()
    {
        base.WorkerReportsProgress = true;
        base.WorkerSupportsCancellation = false;
        base.DoWork += IsoDetails_DoWork;
    }

    private void IsoDetails_DoWork(object sender, DoWorkEventArgs e)
    {
        if (e.Argument == null)
        {
            throw new ArgumentNullException("A populated instance of IsoDetailsArgs must be passed.");
        }
        args = (IsoDetailsArgs)e.Argument;
        if (!openIso())
        {
            return;
        }
        IsoDetailsPlatform isoDetailsPlatform;
        if (iso.Exists("default.xex"))
        {
            isoDetailsPlatform = IsoDetailsPlatform.Xbox360;
        }
        else
        {
            if (!iso.Exists("default.xbe"))
            {
                ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Could not locate default.xex or default.xbe."));
                return;
            }
            isoDetailsPlatform = IsoDetailsPlatform.Xbox;
        }
        switch (isoDetailsPlatform)
        {
            case IsoDetailsPlatform.Xbox:
                readXbe(e);
                break;
            case IsoDetailsPlatform.Xbox360:
                readXex(e);
                break;
        }
    }

    private bool openIso()
    {
        try
        {
            f = new FileStream(args.PathISO, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            iso = new GDF(f);
        }
        catch (IOException ex)
        {
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Failed to open ISO image. Reason:\n\n" + ex.Message));
            return false;
        }
        catch (Exception ex2)
        {
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Unhandled exception occured when opening ISO image. Reason:\n\n" + ex2.Message));
            return false;
        }
        return true;
    }

    public static string getMD5(byte[] input)
    {
        // Create an instance of the MD5CryptoServiceProvider class
        using (MD5 md5 = MD5.Create())
        {
            // Convert the input string to a byte array and compute the hash
            byte[] hashBytes = md5.ComputeHash(input);

            // Convert the byte array to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }

    private void readXbe(DoWorkEventArgs e)
    {
        IsoDetailsResults isoDetailsResults = null;
        string md5 = "";
        byte[] array = null;
        ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Progress, "Locating default.xbe..."));
        try
        {
            array = iso.GetFile("default.xbe");
        }
        catch (Exception ex)
        {
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Unable to extract default.xbe. Reason:\n\n" + ex.Message));
            return;
        }
        ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Progress, "Found! Reading default.xbe..."));
        using (XbeInfo xbeInfo = new XbeInfo(array))
        {
            if (!xbeInfo.IsValid)
            {
                ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Default.xbe was not valid."));
                return;
            }
            isoDetailsResults = new IsoDetailsResults(xbeInfo.Certifcate.TitleName, xbeInfo.Certifcate.TitleID, (xbeInfo.Certifcate.DiskNumber != 0) ? xbeInfo.Certifcate.DiskNumber.ToString() : "1", getMD5(array));
            isoDetailsResults.DiscCount = "1";
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Progress, "Extracting thumbnail..."));
            foreach (XbeSection section in xbeInfo.Sections)
            {
                if (!(section.Name == "$$XSIMAGE"))
                {
                    continue;
                }
                try
                {
                    XPR xPR = new XPR(section.Data);
                    DDS dDS = xPR.ConvertToDDS(64, 64);
                    Bitmap bitmap = new Bitmap(64, 64);
                    switch (xPR.Format)
                    {
                        case XPRFormat.ARGB:
                            bitmap = (Bitmap)dDS.GetImage(DDSType.ARGB);
                            break;
                        case XPRFormat.DXT1:
                            bitmap = (Bitmap)dDS.GetImage(DDSType.DXT1);
                            break;
                    }
                    MemoryStream memoryStream = new MemoryStream();
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    isoDetailsResults.Thumbnail = (Image)bitmap.Clone();
                    isoDetailsResults.RawThumbnail = (byte[])memoryStream.ToArray().Clone();
                    bitmap.Dispose();
                    memoryStream.Dispose();
                    if (xPR.Format == XPRFormat.ARGB)
                    {
                        ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "XBE thumbnail type is not supported or is corrupt."));
                    }
                }
                catch (Exception ex2)
                {
                    ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Failed to convert thumbnail DDS to PNG.\n\n" + ex2.Message));
                }
            }
            if (isoDetailsResults.Thumbnail == null)
            {
                foreach (XbeSection section2 in xbeInfo.Sections)
                {
                    if (!(section2.Name == "$$XTIMAGE"))
                    {
                        continue;
                    }
                    try
                    {
                        XPR xPR2 = new XPR(section2.Data);
                        DDS dDS2 = xPR2.ConvertToDDS(128, 128);
                        Bitmap bitmap2 = new Bitmap(128, 128);
                        switch (xPR2.Format)
                        {
                            case XPRFormat.ARGB:
                                bitmap2 = (Bitmap)dDS2.GetImage(DDSType.ARGB);
                                break;
                            case XPRFormat.DXT1:
                                bitmap2 = (Bitmap)dDS2.GetImage(DDSType.DXT1);
                                break;
                        }
                        Image image = new Bitmap(64, 64);
                        Graphics graphics = Graphics.FromImage(image);
                        graphics.DrawImage(bitmap2, 0, 0, 64, 64);
                        MemoryStream memoryStream2 = new MemoryStream();
                        image.Save(memoryStream2, ImageFormat.Png);
                        isoDetailsResults.Thumbnail = (Image)image.Clone();
                        isoDetailsResults.RawThumbnail = (byte[])memoryStream2.ToArray().Clone();
                        memoryStream2.Dispose();
                        bitmap2.Dispose();
                        graphics.Dispose();
                        if (xPR2.Format == XPRFormat.ARGB)
                        {
                            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "XBE Thumbnail type is not supported or is corrupt."));
                        }
                    }
                    catch (Exception ex3)
                    {
                        ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Failed to convert thumbnail DDS to PNG.\n\n" + ex3.Message));
                    }
                }
            }
        }
        e.Result = isoDetailsResults;
    }

    private void readXex(DoWorkEventArgs e)
    {
        IsoDetailsResults isoDetailsResults = null;
        byte[] array = null;
        string text = null;
        string text2 = null;
        ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Progress, "Locating default.xex..."));
        try
        {
            array = iso.GetFile("default.xex");
            text2 = args.PathTemp;
            text = text2 + "default.xex";
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Progress, "Extracting default.xex..."));
            if (array == null || array.Length == 0)
            {
                ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Couldn't locate default.xex. Please check this ISO is valid."));
                return;
            }
            File.WriteAllBytes(text, array);
        }
        catch (Exception ex)
        {
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "A problem occured when reading the contents of the ISO image.\n\nPlease ensure this is a valid Xbox 360 ISO by running it through ABGX360.\n\n" + ex.Message));
            return;
        }
        ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Progress, "Found! Reading default.xex..."));
        using (XexInfo xexInfo = new XexInfo(array))
        {
            if (!xexInfo.IsValid)
            {
                ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Default.xex is not valid."));
                return;
            }
            if (xexInfo.Header.ContainsKey(XexInfoFields.ExecutionInfo))
            {
                XexExecutionInfo xexExecutionInfo = (XexExecutionInfo)xexInfo.Header[XexInfoFields.ExecutionInfo];
                isoDetailsResults = new IsoDetailsResults("", DataConversion.BytesToHexString(xexExecutionInfo.TitleID), DataConversion.BytesToHexString(xexExecutionInfo.MediaID), xexExecutionInfo.Platform.ToString(), xexExecutionInfo.ExecutableType.ToString(), xexExecutionInfo.DiscNumber.ToString(), xexExecutionInfo.DiscCount.ToString(), null);
            }
        }
        ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Progress, "Extracting resources..."));
        Process process = new Process();
        process.EnableRaisingEvents = false;
        process.StartInfo.FileName = args.PathXexTool;
        if (!File.Exists(process.StartInfo.FileName))
        {
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Couldn't locate XexTool. Expected location was:\n" + process.StartInfo.FileName + "\n\nTry disabling User Access Control if it's enabled."));
            return;
        }
        process.StartInfo.WorkingDirectory = text2;
        process.StartInfo.Arguments = "-d . default.xex";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = false;
        process.StartInfo.CreateNoWindow = true;
        try
        {
            process.Start();
            process.WaitForExit();
            process.Close();
        }
        catch (Win32Exception)
        {
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Could not launch XexTool!"));
            return;
        }
        if (File.Exists(text2 + isoDetailsResults.TitleID))
        {
            Xdbf xdbf = new Xdbf(File.ReadAllBytes(text2 + isoDetailsResults.TitleID));
            ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Progress, "Extracting thumbnail..."));
            try
            {
                byte[] resource = xdbf.GetResource(XdbfResource.Thumb, XdbfResourceType.TitleInfo);
                MemoryStream stream = new MemoryStream(resource);
                Image image = Image.FromStream(stream);
                isoDetailsResults.Thumbnail = (Image)image.Clone();
                isoDetailsResults.RawThumbnail = (byte[])resource.Clone();
                image.Dispose();
            }
            catch (Exception)
            {
                try
                {
                    byte[] resource2 = xdbf.GetResource(XdbfResource.Thumb, XdbfResourceType.Achievement);
                    MemoryStream stream2 = new MemoryStream(resource2);
                    Image image2 = Image.FromStream(stream2);
                    isoDetailsResults.Thumbnail = (Image)image2.Clone();
                    isoDetailsResults.RawThumbnail = (byte[])resource2.Clone();
                    image2.Dispose();
                }
                catch (Exception)
                {
                    ReportProgress(0, new IsoDetailsResults(IsoDetailsResultsType.Error, "Couldn't find thumbnail in XDBF. Possibly corrupt XDBF?"));
                }
            }
            try
            {
                MemoryStream memoryStream = new MemoryStream(xdbf.GetResource(1u, 3));
                memoryStream.Seek(17L, SeekOrigin.Begin);
                int count = memoryStream.ReadByte();
                isoDetailsResults.Name = Encoding.UTF8.GetString(memoryStream.ToArray(), 18, count);
                memoryStream.Close();
            }
            catch (Exception)
            {
                try
                {
                    MemoryStream memoryStream2 = new MemoryStream(xdbf.GetResource(1u, 0));
                    memoryStream2.Seek(17L, SeekOrigin.Begin);
                    int count2 = memoryStream2.ReadByte();
                    isoDetailsResults.Name = Encoding.UTF8.GetString(memoryStream2.ToArray(), 18, count2);
                    memoryStream2.Close();
                }
                catch (Exception)
                {
                    isoDetailsResults.Name = "Unable to read name.";
                }
            }
        }
        e.Result = isoDetailsResults;
    }
}
