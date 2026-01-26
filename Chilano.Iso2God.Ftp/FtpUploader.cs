using EnterpriseDT.Net.Ftp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Chilano.Iso2God.Ftp;

public class FtpUploader : BackgroundWorker
{
    private FTPConnection ftp;

    private FtpUploaderArgs args;

    public List<Exception> Errors = new List<Exception>();

    public bool Debug;

    public FtpUploader()
    {
        base.WorkerReportsProgress = true;
        base.WorkerSupportsCancellation = false;
        base.DoWork += FtpUploader_DoWork;
    }

    private void FtpUploader_DoWork(object sender, DoWorkEventArgs e)
    {
        ftp = new FTPConnection();
        try
        {
            args = (FtpUploaderArgs)e.Argument;
        }
        catch
        {
            Errors.Add(new ArgumentNullException("FtpUploader must be passed an instance of FtpUploaderArgs."));
            return;
        }

        string text = ((args.Platform == IsoEntryPlatform.Xbox360) ? "00007000" : "00005000");
        string text2 = args.SourcePath + args.GameDirectory + Path.DirectorySeparatorChar + text + Path.DirectorySeparatorChar;
        ftp.ServerAddress = args.Ip;
        ftp.UserName = args.User;
        ftp.Password = args.Pass;
        ftp.AutoLogin = true;
        try
        {
            ftp.Connect();
        }
        catch (Exception item)
        {
            Errors.Add(item);
            return;
        }
        string ftpPath = "Hdd1/Content/0000000000000000";
        if (Properties.Settings.Default.FtpPathType == 0)
        {
            switch (Properties.Settings.Default.FtpPathDefaults)
            {
                case 1:
                    ftpPath = "Usb0";
                    break;
                case 2:
                    ftpPath = "Usb1";
                    break;
            }
            ftp.ChangeWorkingDirectory(ftpPath);
        }
        else
        {
            ftpPath = Properties.Settings.Default["FtpPathCustom"].ToString();

            // in case user custom path has subfolders, mkdir and chdir recursively
            string[] CustomDirectories = ftpPath.Split('/');
            foreach (string subDirectory in CustomDirectories)
            {
                if (!dirExists(subDirectory))
                {
                    ftp.CreateDirectory(subDirectory);
                }
                ftp.ChangeWorkingDirectory(subDirectory);
            }
        }
        // in case TitleDirectory has subfolders, mkdir and chdir recursively
        string[] TitleDirectories = args.GameDirectory.Split(Path.DirectorySeparatorChar);
        foreach (string subDirectory in TitleDirectories)
        {
            if (!dirExists(subDirectory))
            {
                ftp.CreateDirectory(subDirectory);
            }
            ftp.ChangeWorkingDirectory(subDirectory);
        }
        if (!dirExists(text))
        {
            ftp.CreateDirectory(text);
        }
        ftp.ChangeWorkingDirectory(text);
        if (!dirExists(args.ContainerID + ".data"))
        {
            ftp.CreateDirectory(args.ContainerID + ".data");
        }
        else
        {
            clearDir(args.ContainerID + ".data");
        }
        if (fileExists(args.ContainerID))
        {
            ftp.DeleteFile(args.ContainerID);
        }
        ReportProgress(1, "Uploading GOD header...");
        FileStream srcStream = new FileStream(text2 + args.ContainerID, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        ftp.UploadStream(srcStream, args.ContainerID);
        ftp.ChangeWorkingDirectory(args.ContainerID + ".data");
        int num = 0;
        string[] files = Directory.GetFiles(text2 + args.ContainerID + ".data");
        string[] array = files;
        foreach (string text3 in array)
        {
            string text4 = text3.Substring(text3.LastIndexOf('\\') + 1);
            ReportProgress(num, "Uploading '" + text4 + "'...");
            FileStream srcStream2 = new FileStream(text3, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ftp.UploadStream(srcStream2, text4);
            num += (int)Math.Floor(1f / (float)files.Length * 100f);
        }
        ftp.Close();
        Errors.Clear();
        ReportProgress(100, "Uploaded");
    }

    private bool fileExists(string file)
    {
        try
        {
            FTPFile[] fileInfos = ftp.GetFileInfos();
            FTPFile[] array = fileInfos;
            foreach (FTPFile fTPFile in array)
            {
                if (!fTPFile.Dir && fTPFile.Name.StartsWith(file))
                {
                    return true;
                }
            }
        }
        catch (FTPException item)
        {
            Errors.Add(item);
        }
        return false;
    }

    private void clearDir(string dir)
    {
        try
        {
            ftp.ChangeWorkingDirectory(dir);
            FTPFile[] fileInfos = ftp.GetFileInfos();
            FTPFile[] array = fileInfos;
            foreach (FTPFile fTPFile in array)
            {
                ftp.DeleteFile(fTPFile.Name);
            }
            ftp.ChangeWorkingDirectoryUp();
        }
        catch (FTPException item)
        {
            Errors.Add(item);
        }
    }

    private bool dirExists(string dir)
    {
        try
        {
            FTPFile[] fileInfos = ftp.GetFileInfos();
            FTPFile[] array = fileInfos;
            foreach (FTPFile fTPFile in array)
            {
                //if (fTPFile.Dir && fTPFile.Name.StartsWith(dir))
                if (fTPFile.Dir && fTPFile.Name == dir)
                {
                    return true;
                }
            }
            return false;
        }
        catch (FTPException item)
        {
            Errors.Add(item);
            return false;
        }
    }
}
