using Chilano.Common;
using Chilano.Iso2God.Ftp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace Chilano.Iso2God;

public class Main : Form
{
    private IContainer components;

    private ToolStripEx toolStrip1;

    private ToolStripDropDownButton toolStripDropDownButton1;

    private ToolStripMenuItem allToolStripMenuItem;

    private ToolStripMenuItem selectedToolStripMenuItem;

    private ToolStripMenuItem completedToolStripMenuItem;

    private ToolStripButton toolStripButton2;

    private ToolStripSeparator toolStripSeparator1;

    private ToolStripButton toolStripButton3;

    private ToolStripButton toolStripButton4;

    private CListView listView1;

    private StatusStrip statusStrip1;

    private ColumnHeader columnHeader1;

    private ColumnHeader columnHeader2;

    private ColumnHeader columnHeader4;

    private Timer jobCheck;

    private ToolStripStatusLabel tsStatus;

    private ColumnHeader columnHeader3;

    private ColumnHeader columnHeader5;

    private ColumnHeader columnHeader6;

    private Timer ftpCheck;

    private ColumnHeader columnHeader7;

    private ContextMenuStrip cmQueue;

    private ToolStripMenuItem editToolStripMenuItem;

    private ToolStripMenuItem removeToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator2;

    private ToolStripMenuItem restartFTPUploadToolStripMenuItem;

    private Timer freeDiskCheck;

    private ToolStripButton toolStripButton1;

    private Iso2God i2g = new Iso2God();

    private FtpUploader ftp = new FtpUploader();

    public string pathXT = "";
    private ToolStripLabel toolStripLabel2;
    public string pathTemp = "";

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.cmQueue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.restartFTPUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.jobCheck = new System.Windows.Forms.Timer(this.components);
            this.ftpCheck = new System.Windows.Forms.Timer(this.components);
            this.freeDiskCheck = new System.Windows.Forms.Timer(this.components);
            this.listView1 = new Chilano.Common.CListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new Chilano.Common.ToolStripEx();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.cmQueue.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmQueue
            // 
            this.cmQueue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripSeparator2,
            this.restartFTPUploadToolStripMenuItem});
            this.cmQueue.Name = "cmQueue";
            this.cmQueue.Size = new System.Drawing.Size(174, 76);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // restartFTPUploadToolStripMenuItem
            // 
            this.restartFTPUploadToolStripMenuItem.Name = "restartFTPUploadToolStripMenuItem";
            this.restartFTPUploadToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.restartFTPUploadToolStripMenuItem.Text = "Restart FTP Upload";
            this.restartFTPUploadToolStripMenuItem.Click += new System.EventHandler(this.restartFTPUploadToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsStatus
            // 
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(26, 17);
            this.tsStatus.Text = "Idle";
            // 
            // freeDiskCheck
            // 
            this.freeDiskCheck.Enabled = true;
            this.freeDiskCheck.Interval = 30000;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader7,
            this.columnHeader4,
            this.columnHeader6});
            this.listView1.ContextMenuStrip = this.cmQueue;
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 70);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(784, 269);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 190;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Title ID";
            this.columnHeader5.Width = 65;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Disc";
            this.columnHeader1.Width = 35;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 50;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Padding";
            this.columnHeader7.Width = 55;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Progress";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Status Message";
            this.columnHeader6.Width = 270;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.ToolbarBg;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.ClickThrough = true;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripButton1,
            this.toolStripDropDownButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(784, 71);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStripLabel2.Image = global::Chilano_Iso2God_Properties_Resources.LogoToolbar;
            this.toolStripLabel2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(20, 0, 15, 0);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(194, 71);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::Chilano_Iso2God_Properties_Resources.icon_add;
            this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButton1.Size = new System.Drawing.Size(83, 68);
            this.toolStripButton1.Text = "Add ISO";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.selectedToolStripMenuItem,
            this.completedToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::Chilano_Iso2God_Properties_Resources.icon_delete;
            this.toolStripDropDownButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(92, 68);
            this.toolStripDropDownButton1.Text = "Remove";
            this.toolStripDropDownButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // selectedToolStripMenuItem
            // 
            this.selectedToolStripMenuItem.Name = "selectedToolStripMenuItem";
            this.selectedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.selectedToolStripMenuItem.Text = "Selected";
            this.selectedToolStripMenuItem.Click += new System.EventHandler(this.selectedToolStripMenuItem_Click);
            // 
            // completedToolStripMenuItem
            // 
            this.completedToolStripMenuItem.Name = "completedToolStripMenuItem";
            this.completedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.completedToolStripMenuItem.Text = "Completed";
            this.completedToolStripMenuItem.Click += new System.EventHandler(this.completedToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::Chilano_Iso2God_Properties_Resources.icon_start;
            this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButton2.Size = new System.Drawing.Size(82, 68);
            this.toolStripButton2.Text = "Convert";
            this.toolStripButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(10, 15, 5, 15);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::Chilano_Iso2God_Properties_Resources.icon_settings;
            this.toolStripButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButton3.Size = new System.Drawing.Size(82, 68);
            this.toolStripButton3.Text = "Settings";
            this.toolStripButton3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::Chilano_Iso2God_Properties_Resources.icon_info;
            this.toolStripButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButton4.Size = new System.Drawing.Size(73, 68);
            this.toolStripButton4.Text = "About";
            this.toolStripButton4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iso2God";
            this.cmQueue.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    [DllImport("user32.dll")]
    private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

    public Main()
    {
        InitializeComponent();
        base.Load += Main_Load;
        base.FormClosing += Main_FormClosing;
        jobCheck.Tick += jobCheck_Tick;
        ftpCheck.Tick += ftpCheck_Tick;
        freeDiskCheck.Tick += freeDiskCheck_Tick;
        i2g.Progress += i2g_Progress;
        i2g.Completed += i2g_Completed;
        ftp.RunWorkerCompleted += ftp_RunWorkerCompleted;
        ftp.ProgressChanged += ftp_ProgressChanged;
    }

    private void Main_Load(object sender, EventArgs e)
    {
        listView1.DoubleClick += listView1_DoubleClick;
        Text = Text + " " + getVersion(build: true, revision: false);
        pathTemp = Path.GetTempPath() + "i2g" + Path.DirectorySeparatorChar;
        Directory.CreateDirectory(pathTemp);
        pathXT = Application.StartupPath + Path.DirectorySeparatorChar + "xextool.exe";
        Width = (int)Chilano.Iso2God.Properties.Settings.Default["Width"];
        Height = (int)Chilano.Iso2God.Properties.Settings.Default["Height"];
        CenterToScreen();
        string[] ColumnsWidth = Chilano.Iso2God.Properties.Settings.Default["ColumnsWidth"].ToString().Split(',');
        for (int i = 0; i < ColumnsWidth.Length; i++)
        {
            if (listView1.Columns.Count > i) listView1.Columns[i].Width = Convert.ToInt32(ColumnsWidth[i]);
            else break;
        }
        if (!File.Exists(pathXT))
        {
            tsStatus.Text = "Could not locate XexTool! Please ensure it is in the same directory as Iso2God or thumbnail extraction will not work.";
        }
        else
        {
            UpdateSpace();
        }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (i2g.IsBusy)
        {
            DialogResult dialogResult = MessageBox.Show("An ISO is currently being converted.\n\nAre you sure you wish to exit?", "ISO Conversion in Progress", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }
        if (ftp.IsBusy)
        {
            DialogResult dialogResult2 = MessageBox.Show("A GOD container is currently being uploaded.\n\nAre you sure you wish to exit?", "FTP Upload in Progress", MessageBoxButtons.YesNo);
            if (dialogResult2 == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }
        if (Directory.Exists(pathTemp))
        {
            string[] files = Directory.GetFiles(pathTemp);
            foreach (string path in files)
            {
                File.Delete(path);
            }
        }

        Chilano.Iso2God.Properties.Settings.Default["Width"] = Width;
        Chilano.Iso2God.Properties.Settings.Default["Height"] = Height;
        Chilano.Iso2God.Properties.Settings.Default.Save();

        List<string> ColumnsWidth = new List<string>();
        foreach (ColumnHeader column in listView1.Columns)
        {
            ColumnsWidth.Add(column.Width.ToString());
        }
        Chilano.Iso2God.Properties.Settings.Default["ColumnsWidth"] = String.Join(",", ColumnsWidth.ToArray());
        Chilano.Iso2God.Properties.Settings.Default.Save();
    }

    private void freeDiskCheck_Tick(object sender, EventArgs e)
    {
        UpdateSpace();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
        using AddISO addISO = new AddISO(IsoEntryPlatform.Xbox360);
        addISO.ShowDialog(this);
    }

    private void toolStripButton3_Click(object sender, EventArgs e)
    {
        using Settings settings = new Settings();
        settings.ShowDialog(this);
    }

    private void toolStripButton4_Click(object sender, EventArgs e)
    {
        using About about = new About();
        about.ShowDialog(this);
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
        jobCheck.Enabled = true;
    }

    private void ftpCheck_Tick(object sender, EventArgs e)
    {
        if (ftp.IsBusy)
        {
            return;
        }
        foreach (ListViewItem item in listView1.Items)
        {
            IsoEntry isoEntry = (IsoEntry)item.Tag;
            if (isoEntry.Status == IsoEntryStatus.UploadQueue)
            {
                isoEntry.Status = IsoEntryStatus.Uploading;
                item.Tag = isoEntry;
                ftp = new FtpUploader();
                ftp.RunWorkerCompleted += ftp_RunWorkerCompleted;
                ftp.ProgressChanged += ftp_ProgressChanged;
                string ip = Chilano.Iso2God.Properties.Settings.Default["FtpIP"].ToString();
                string user = Chilano.Iso2God.Properties.Settings.Default["FtpUser"].ToString();
                string pass = Chilano.Iso2God.Properties.Settings.Default["FtpPass"].ToString();
                string port = Chilano.Iso2God.Properties.Settings.Default["FtpPort"].ToString();
                _ = isoEntry.ID.ContainerID;
                ftp.RunWorkerAsync(new FtpUploaderArgs(ip, user, pass, port, isoEntry.ID.TitleID, isoEntry.ID.ContainerID, isoEntry.Destination, isoEntry.Platform));
                ftpCheck.Enabled = false;
                return;
            }
        }
        ftpCheck.Enabled = false;
    }

    private void ftp_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        foreach (ListViewItem item in listView1.Items)
        {
            IsoEntry isoEntry = (IsoEntry)item.Tag;
            if (isoEntry.Status == IsoEntryStatus.Uploading)
            {
                ProgressBar progressBar = (ProgressBar)listView1.GetEmbeddedControl(5, item.Index);
                progressBar.Value = ((e.ProgressPercentage > 100) ? 100 : e.ProgressPercentage);
                item.ForeColor = Color.Blue;
                item.SubItems[6].Text = e.UserState.ToString();
                item.Tag = isoEntry;
                break;
            }
        }
    }

    private void ftp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        foreach (ListViewItem item in listView1.Items)
        {
            IsoEntry isoEntry = (IsoEntry)item.Tag;
            if (isoEntry.Status != IsoEntryStatus.Uploading)
            {
                continue;
            }
            isoEntry.Status = IsoEntryStatus.Completed;
            ProgressBar progressBar = (ProgressBar)listView1.GetEmbeddedControl(5, item.Index);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = 100;
            FlashWindow(base.Handle, bInvert: false);
            if (ftp.Errors.Count == 0)
            {
                item.ForeColor = Color.Green;
                item.SubItems[6].Text = "Uploaded";
            }
            else
            {
                item.ForeColor = Color.Red;
                foreach (Exception error in ftp.Errors)
                {
                    MessageBox.Show("Error while attempting to upload GOD container for '" + isoEntry.TitleName + "':\n\n" + error.Message);
                }
                item.SubItems[6].Text = "Failed to upload.";
            }
            item.Tag = isoEntry;
            ftpCheck.Enabled = true;
            break;
        }
    }

    private void jobCheck_Tick(object sender, EventArgs e)
    {
        if (i2g.IsBusy)
        {
            return;
        }
        foreach (ListViewItem item in listView1.Items)
        {
            IsoEntry isoEntry = (IsoEntry)item.Tag;
            if (isoEntry.Status == IsoEntryStatus.Idle)
            {
                isoEntry.Status = IsoEntryStatus.InProgress;
                item.Tag = isoEntry;
                i2g = new Iso2God();
                i2g.Completed += i2g_Completed;
                i2g.Progress += i2g_Progress;
                i2g.RunWorkerAsync(isoEntry);
                jobCheck.Enabled = false;
                return;
            }
        }
        jobCheck.Enabled = false;
    }

    private void i2g_Completed(object sender, Iso2GodCompletedArgs e)
    {
        foreach (ListViewItem item in listView1.Items)
        {
            IsoEntry isoEntry = (IsoEntry)item.Tag;
            if (isoEntry.Status == IsoEntryStatus.InProgress)
            {
                ProgressBar progressBar = (ProgressBar)listView1.GetEmbeddedControl(5, item.Index);
                if ((bool)Chilano.Iso2God.Properties.Settings.Default["FtpUpload"])
                {
                    isoEntry.Status = IsoEntryStatus.UploadQueue;
                    isoEntry.ID.ContainerID = e.ContainerId;
                    progressBar.Value = 0;
                    item.SubItems[6].Text = "Queued for upload.";
                    ftpCheck.Enabled = true;
                }
                else
                {
                    isoEntry.Status = IsoEntryStatus.Completed;
                    progressBar.Value = 100;
                    item.SubItems[6].Text = e.Message + ((e.Error != null) ? (". Error: " + e.Error.Message) : "");
                    FlashWindow(base.Handle, bInvert: false);
                }
                jobCheck.Enabled = true;
                item.Tag = isoEntry;
                item.ForeColor = Color.Green;
                break;
            }
        }
    }

    private void i2g_Progress(object sender, Iso2GodProgressArgs e)
    {
        foreach (ListViewItem item in listView1.Items)
        {
            IsoEntry isoEntry = (IsoEntry)item.Tag;
            if (isoEntry.Status == IsoEntryStatus.InProgress)
            {
                ((ProgressBar)listView1.GetEmbeddedControl(5, item.Index)).Value = ((e.Percentage > 100) ? 100 : e.Percentage);
                item.SubItems[6].Text = e.Message;
                item.Tag = isoEntry;
                break;
            }
        }
    }

    public void AddISOEntry(IsoEntry Entry)
    {
        ListViewItem listViewItem = new ListViewItem();
        listViewItem.Text = Entry.TitleName;
        listViewItem.SubItems.Add(Entry.ID.TitleID);
        listViewItem.SubItems.Add(Entry.ID.DiscNumber.ToString());
        double num = Math.Round((double)Entry.Size / 1073741824.0, 2);
        listViewItem.SubItems.Add(num + " GB");
        listViewItem.SubItems.Add(Entry.Padding.Type.ToString());
        listViewItem.SubItems.Add("");
        listViewItem.SubItems.Add(Entry.Path);
        listViewItem.Tag = Entry;
        listView1.Items.Add(listViewItem);
        listView1.AddEmbeddedControl(new ProgressBar(), 5, listViewItem.Index);
        long FreeSpace = 0L;
        UpdateSpace(out FreeSpace);
        if (FreeSpace < Entry.Size)
        {
            tsStatus.Text += ". You do not have enough free disk space to convert this ISO.";
        }
    }

    public void UpdateISOEntry(int Index, IsoEntry Entry)
    {
        ListViewItem listViewItem = listView1.Items[Index];
        listViewItem.Tag = Entry;
        listViewItem.Text = Entry.TitleName;
        listViewItem.SubItems[1].Text = Entry.ID.TitleID;
        listViewItem.SubItems[2].Text = Entry.ID.DiscNumber.ToString();
        double num = Math.Round((double)Entry.Size / 1073741824.0, 2);
        listViewItem.SubItems[3].Text = num + " GB";
        listViewItem.SubItems[4].Text = Entry.Padding.Type.ToString();
        listViewItem.SubItems[6].Text = Entry.Path;
    }

    public string getVersion(bool build, bool revision)
    {
        Version version = Assembly.GetExecutingAssembly().GetName().Version;
        string text = "";
        object obj = text;
        text = string.Concat(obj, version.Major, ".", version.Minor);
        if (build)
        {
            text = text + "." + version.Build;
            if (revision)
            {
                text = text + "." + version.Revision;
            }
        }
        return text;
    }

    public long GetFreeSpace(string DriveLetter)
    {
        try
        {
            ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + DriveLetter.ToLower() + ":\"");
            managementObject.Get();
            return (long)(ulong)managementObject["FreeSpace"];
        }
        catch
        {
            return -1L;
        }
    }

    public void UpdateSpace()
    {
        UpdateSpace(out var _);
    }

    public void UpdateSpace(out long FreeSpace)
    {
        string text = Chilano.Iso2God.Properties.Settings.Default["OutputPath"].ToString();
        if (!IsRunningOnMono())
        {
            FreeSpace = GetFreeSpace((text.Length > 0) ? text[0].ToString() : "C");
            if (FreeSpace > -1)
            {
                tsStatus.Text = "Free Disk Space: " + Math.Round((float)FreeSpace / 1.0737418E+09f, 2) + " GB";
            }
            else
            {
                tsStatus.Text = "Free Disk Space: Unable to find this value.";
            }
        }
        else
        {
            tsStatus.Text = "Free Disk Space: [Not supported for Mono. Sorry!]";
            FreeSpace = 0L;
        }
    }

    public bool IsRunningOnMono()
    {
        return Type.GetType("Mono.Runtime") != null;
    }

    private void listView1_DoubleClick(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count > 1)
        {
            return;
        }
        ListViewItem listViewItem = listView1.SelectedItems[0];
        IsoEntry entry = (IsoEntry)listViewItem.Tag;
        if (entry.Status == IsoEntryStatus.Idle)
        {
            using (AddISO addISO = new AddISO(entry.Platform))
            {
                addISO.Edit(listViewItem.Index, entry);
                addISO.ShowDialog(this);
                return;
            }
        }
        MessageBox.Show("Conversions that are currently in progress or have completed cannot be edited.");
    }

    private void allToolStripMenuItem_Click(object sender, EventArgs e)
    {
        foreach (ListViewItem item in listView1.Items)
        {
            if (((IsoEntry)item.Tag).Status != IsoEntryStatus.InProgress)
            {
                item.Selected = true;
            }
        }
        listView1.Remove(CListView.RemoveType.Selected);
    }

    private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        foreach (ListViewItem item in listView1.Items)
        {
            if (((IsoEntry)item.Tag).Status == IsoEntryStatus.InProgress)
            {
                item.Selected = false;
            }
        }
        listView1.Remove(CListView.RemoveType.Selected);
    }

    private void completedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        foreach (ListViewItem selectedItem in listView1.SelectedItems)
        {
            if (((IsoEntry)selectedItem.Tag).Status == IsoEntryStatus.Idle)
            {
                selectedItem.Selected = false;
            }
        }
        foreach (ListViewItem item in listView1.Items)
        {
            if (((IsoEntry)item.Tag).Status == IsoEntryStatus.Completed)
            {
                item.Selected = true;
            }
        }
        listView1.Remove(CListView.RemoveType.Selected);
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count > 0)
        {
            listView1_DoubleClick(this, new EventArgs());
        }
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        foreach (ListViewItem selectedItem in listView1.SelectedItems)
        {
            listView1.Items.Remove(selectedItem);
        }
    }

    private void restartFTPUploadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count == 1)
        {
            ListViewItem listViewItem = listView1.SelectedItems[0];
            IsoEntry isoEntry = (IsoEntry)listViewItem.Tag;
            if (isoEntry.Status == IsoEntryStatus.Uploading || isoEntry.Status == IsoEntryStatus.Completed)
            {
                isoEntry.Status = IsoEntryStatus.UploadQueue;
                listViewItem.Tag = isoEntry;
                listViewItem.ForeColor = Color.Blue;
                listViewItem.SubItems[6].Text = "Queued for upload.";
                ((ProgressBar)listView1.GetEmbeddedControl(5, listViewItem.Index)).Value = 0;
                ftpCheck.Enabled = true;
            }
        }
    }
}
