using EnterpriseDT.Net.Ftp;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Chilano.Iso2God.Ftp;

public class FtpTester : BackgroundWorker
{
    private FTPConnection ftp;

    private FtpTesterArgs args;

    public List<Exception> Errors = new List<Exception>();

    public bool Debug;

    public FtpTester()
    {
        base.WorkerReportsProgress = true;
        base.WorkerSupportsCancellation = false;
        base.DoWork += FtpTester_DoWork;
    }

    private void FtpTester_DoWork(object sender, DoWorkEventArgs e)
    {
        BackgroundWorker worker = sender as BackgroundWorker;

        ftp = new FTPConnection();
        try
        {
            args = (FtpTesterArgs)e.Argument;
        }
        catch
        {
            Errors.Add(new ArgumentNullException("FtpUploader must be passed an instance of FtpUploaderArgs."));
            e.Cancel = worker.CancellationPending;
            return;
        }

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
            e.Cancel = worker.CancellationPending;
            return;
        }
    }
}