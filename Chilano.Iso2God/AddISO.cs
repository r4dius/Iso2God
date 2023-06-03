using Chilano.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using System.Windows.Forms;

namespace Chilano.Iso2God;

public class AddISO : Form
{
    private IContainer components;

    private GroupBox groupBox2;

    private PictureBox pbVideo;

    private Button btnDestBrowse;

    private Label label3;

    private TextBox txtDest;

    private Button btnISOBrowse;

    private Label label1;

    private TextBox txtISO;

    private ToolTip ttISO;

    private GroupBox groupBox3;

    private PictureBox pbTime;

    private Label label4;

    private Label label2;

    private TextBox txtMediaID;

    private TextBox txtTitleID;

    private Button btnAddIso;

    private Button btnCancel;

    private TextBox txtName;

    private Label label5;

    private Label label8;

    private Label label9;

    private TextBox txtExType;

    private TextBox txtPlatform;

    private Label label6;

    private TextBox txtDiscCount;

    private TextBox txtDiscNum;

    private Label label7;

    private ToolTip ttSettings;

    private PictureBox pbThumb;

    private ToolTip ttThumb;

    private GroupBox groupBox1;

    private ComboBox cmbPaddingMode;

    private Label label12;

    private Label label14;

    private TextBox txtRebuiltIso;

    private PictureBox pbPadding;

    private Button btnRebuiltBrowse;

    private CheckBox cbSaveRebuilt;

    private ToolTip ttPadding;

    private IsoDetails isoDetails = new IsoDetails();

    private bool edit;

    private string[] fileList;

    private int file;

    private IsoEntryPlatform platform = IsoEntryPlatform.Xbox360;

    private IsoEntry entry;
    private Iso2God iso2God1;
    private Ftp.FtpUploader ftpUploader1;
    private ProgressBarEx progressBarMulti;
    private int entryIndex;

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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnISOBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtISO = new System.Windows.Forms.TextBox();
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.btnDestBrowse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.ttISO = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pbThumb = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtExType = new System.Windows.Forms.TextBox();
            this.txtPlatform = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiscCount = new System.Windows.Forms.TextBox();
            this.txtDiscNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMediaID = new System.Windows.Forms.TextBox();
            this.txtTitleID = new System.Windows.Forms.TextBox();
            this.pbTime = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAddIso = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ttSettings = new System.Windows.Forms.ToolTip(this.components);
            this.ttThumb = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSaveRebuilt = new System.Windows.Forms.CheckBox();
            this.btnRebuiltBrowse = new System.Windows.Forms.Button();
            this.cmbPaddingMode = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRebuiltIso = new System.Windows.Forms.TextBox();
            this.pbPadding = new System.Windows.Forms.PictureBox();
            this.ttPadding = new System.Windows.Forms.ToolTip(this.components);
            this.progressBarMulti = new Chilano.Common.ProgressBarEx();
            this.iso2God1 = new Chilano.Iso2God.Iso2God();
            this.ftpUploader1 = new Chilano.Iso2God.Ftp.FtpUploader();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTime)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPadding)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.btnISOBrowse);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtISO);
            this.groupBox2.Controls.Add(this.pbVideo);
            this.groupBox2.Controls.Add(this.btnDestBrowse);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDest);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(10, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 88);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ISO Details";
            // 
            // btnISOBrowse
            // 
            this.btnISOBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnISOBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnISOBrowse.Location = new System.Drawing.Point(364, 19);
            this.btnISOBrowse.Name = "btnISOBrowse";
            this.btnISOBrowse.Size = new System.Drawing.Size(70, 25);
            this.btnISOBrowse.TabIndex = 1;
            this.btnISOBrowse.Text = "&Browse";
            this.btnISOBrowse.UseVisualStyleBackColor = true;
            this.btnISOBrowse.Click += new System.EventHandler(this.btnISOBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "ISO Image:";
            // 
            // txtISO
            // 
            this.txtISO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtISO.Enabled = false;
            this.txtISO.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtISO.Location = new System.Drawing.Point(87, 20);
            this.txtISO.Name = "txtISO";
            this.txtISO.Size = new System.Drawing.Size(270, 23);
            this.txtISO.TabIndex = 0;
            // 
            // pbVideo
            // 
            this.pbVideo.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbVideo.Location = new System.Drawing.Point(72, 0);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(14, 14);
            this.pbVideo.TabIndex = 25;
            this.pbVideo.TabStop = false;
            // 
            // btnDestBrowse
            // 
            this.btnDestBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDestBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDestBrowse.Location = new System.Drawing.Point(364, 50);
            this.btnDestBrowse.Name = "btnDestBrowse";
            this.btnDestBrowse.Size = new System.Drawing.Size(70, 25);
            this.btnDestBrowse.TabIndex = 3;
            this.btnDestBrowse.Text = "&Browse";
            this.btnDestBrowse.UseVisualStyleBackColor = true;
            this.btnDestBrowse.Click += new System.EventHandler(this.btnDestBrowse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Output Path:";
            // 
            // txtDest
            // 
            this.txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDest.Enabled = false;
            this.txtDest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDest.Location = new System.Drawing.Point(87, 51);
            this.txtDest.Name = "txtDest";
            this.txtDest.Size = new System.Drawing.Size(270, 23);
            this.txtDest.TabIndex = 2;
            // 
            // ttISO
            // 
            this.ttISO.AutoPopDelay = 30000;
            this.ttISO.InitialDelay = 100;
            this.ttISO.IsBalloon = true;
            this.ttISO.ReshowDelay = 100;
            this.ttISO.ShowAlways = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.pbThumb);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtExType);
            this.groupBox3.Controls.Add(this.txtPlatform);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtName);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtDiscCount);
            this.groupBox3.Controls.Add(this.txtDiscNum);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtMediaID);
            this.groupBox3.Controls.Add(this.txtTitleID);
            this.groupBox3.Controls.Add(this.pbTime);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(10, 97);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 119);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Title Details";
            // 
            // pbThumb
            // 
            this.pbThumb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbThumb.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.preview;
            this.pbThumb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbThumb.Location = new System.Drawing.Point(366, 30);
            this.pbThumb.Name = "pbThumb";
            this.pbThumb.Padding = new System.Windows.Forms.Padding(1);
            this.pbThumb.Size = new System.Drawing.Size(66, 66);
            this.pbThumb.TabIndex = 40;
            this.pbThumb.TabStop = false;
            this.pbThumb.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(308, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "/";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(233, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "Disc:";
            // 
            // txtExType
            // 
            this.txtExType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExType.Location = new System.Drawing.Point(327, 82);
            this.txtExType.Name = "txtExType";
            this.txtExType.Size = new System.Drawing.Size(30, 23);
            this.txtExType.TabIndex = 10;
            // 
            // txtPlatform
            // 
            this.txtPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlatform.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlatform.Location = new System.Drawing.Point(269, 82);
            this.txtPlatform.Name = "txtPlatform";
            this.txtPlatform.Size = new System.Drawing.Size(30, 23);
            this.txtPlatform.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(87, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(270, 23);
            this.txtName.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(303, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Ex:";
            // 
            // txtDiscCount
            // 
            this.txtDiscCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscCount.Location = new System.Drawing.Point(327, 51);
            this.txtDiscCount.Name = "txtDiscCount";
            this.txtDiscCount.Size = new System.Drawing.Size(30, 23);
            this.txtDiscCount.TabIndex = 7;
            // 
            // txtDiscNum
            // 
            this.txtDiscNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscNum.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscNum.Location = new System.Drawing.Point(269, 51);
            this.txtDiscNum.Name = "txtDiscNum";
            this.txtDiscNum.Size = new System.Drawing.Size(30, 23);
            this.txtDiscNum.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Media ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Title ID:";
            // 
            // txtMediaID
            // 
            this.txtMediaID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMediaID.Location = new System.Drawing.Point(87, 82);
            this.txtMediaID.Name = "txtMediaID";
            this.txtMediaID.Size = new System.Drawing.Size(141, 23);
            this.txtMediaID.TabIndex = 8;
            // 
            // txtTitleID
            // 
            this.txtTitleID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitleID.Location = new System.Drawing.Point(87, 51);
            this.txtTitleID.Name = "txtTitleID";
            this.txtTitleID.Size = new System.Drawing.Size(141, 23);
            this.txtTitleID.TabIndex = 5;
            // 
            // pbTime
            // 
            this.pbTime.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbTime.Location = new System.Drawing.Point(77, 0);
            this.pbTime.Name = "pbTime";
            this.pbTime.Size = new System.Drawing.Size(14, 14);
            this.pbTime.TabIndex = 25;
            this.pbTime.TabStop = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(234, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Plat:";
            // 
            // btnAddIso
            // 
            this.btnAddIso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddIso.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddIso.Location = new System.Drawing.Point(299, 316);
            this.btnAddIso.Name = "btnAddIso";
            this.btnAddIso.Size = new System.Drawing.Size(75, 25);
            this.btnAddIso.TabIndex = 15;
            this.btnAddIso.Text = "Add";
            this.btnAddIso.UseVisualStyleBackColor = true;
            this.btnAddIso.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(380, 316);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // ttSettings
            // 
            this.ttSettings.AutoPopDelay = 30000;
            this.ttSettings.InitialDelay = 100;
            this.ttSettings.IsBalloon = true;
            this.ttSettings.ReshowDelay = 100;
            this.ttSettings.ShowAlways = true;
            // 
            // ttThumb
            // 
            this.ttThumb.AutomaticDelay = 10;
            this.ttThumb.AutoPopDelay = 30000;
            this.ttThumb.InitialDelay = 100;
            this.ttThumb.IsBalloon = true;
            this.ttThumb.ReshowDelay = 100;
            this.ttThumb.ShowAlways = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.cbSaveRebuilt);
            this.groupBox1.Controls.Add(this.btnRebuiltBrowse);
            this.groupBox1.Controls.Add(this.cmbPaddingMode);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtRebuiltIso);
            this.groupBox1.Controls.Add(this.pbPadding);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(10, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 88);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Padding";
            // 
            // cbSaveRebuilt
            // 
            this.cbSaveRebuilt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSaveRebuilt.AutoSize = true;
            this.cbSaveRebuilt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSaveRebuilt.Location = new System.Drawing.Point(288, 24);
            this.cbSaveRebuilt.Name = "cbSaveRebuilt";
            this.cbSaveRebuilt.Size = new System.Drawing.Size(149, 17);
            this.cbSaveRebuilt.TabIndex = 12;
            this.cbSaveRebuilt.Text = "Save Rebuilt ISO Image?";
            this.cbSaveRebuilt.UseVisualStyleBackColor = true;
            // 
            // btnRebuiltBrowse
            // 
            this.btnRebuiltBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRebuiltBrowse.Enabled = false;
            this.btnRebuiltBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRebuiltBrowse.Location = new System.Drawing.Point(364, 50);
            this.btnRebuiltBrowse.Name = "btnRebuiltBrowse";
            this.btnRebuiltBrowse.Size = new System.Drawing.Size(70, 25);
            this.btnRebuiltBrowse.TabIndex = 14;
            this.btnRebuiltBrowse.Text = "&Browse";
            this.btnRebuiltBrowse.UseVisualStyleBackColor = true;
            this.btnRebuiltBrowse.Click += new System.EventHandler(this.btnRebuiltBrowse_Click);
            // 
            // cmbPaddingMode
            // 
            this.cmbPaddingMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPaddingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaddingMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaddingMode.FormattingEnabled = true;
            this.cmbPaddingMode.Items.AddRange(new object[] {
            "Untouched",
            "Partial",
            "Remove All"});
            this.cmbPaddingMode.Location = new System.Drawing.Point(87, 20);
            this.cmbPaddingMode.Name = "cmbPaddingMode";
            this.cmbPaddingMode.Size = new System.Drawing.Size(141, 23);
            this.cmbPaddingMode.TabIndex = 11;
            this.cmbPaddingMode.SelectedIndexChanged += new System.EventHandler(this.cmbPaddingMode_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "Mode:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 56);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Rebuild Path:";
            // 
            // txtRebuiltIso
            // 
            this.txtRebuiltIso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRebuiltIso.Enabled = false;
            this.txtRebuiltIso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRebuiltIso.Location = new System.Drawing.Point(87, 51);
            this.txtRebuiltIso.Name = "txtRebuiltIso";
            this.txtRebuiltIso.Size = new System.Drawing.Size(270, 23);
            this.txtRebuiltIso.TabIndex = 13;
            // 
            // pbPadding
            // 
            this.pbPadding.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbPadding.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPadding.Location = new System.Drawing.Point(61, 0);
            this.pbPadding.Name = "pbPadding";
            this.pbPadding.Size = new System.Drawing.Size(14, 14);
            this.pbPadding.TabIndex = 25;
            this.pbPadding.TabStop = false;
            // 
            // ttPadding
            // 
            this.ttPadding.AutoPopDelay = 30000;
            this.ttPadding.InitialDelay = 100;
            this.ttPadding.IsBalloon = true;
            this.ttPadding.ReshowDelay = 100;
            this.ttPadding.ShowAlways = true;
            // 
            // progressBarMulti
            // 
            this.progressBarMulti.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMulti.DisplayStyle = Chilano.Common.ProgressBarDisplayText.Text;
            this.progressBarMulti.Location = new System.Drawing.Point(10, 317);
            this.progressBarMulti.Name = "progressBarMulti";
            this.progressBarMulti.Size = new System.Drawing.Size(282, 23);
            this.progressBarMulti.TabIndex = 42;
            // 
            // iso2God1
            // 
            this.iso2God1.WorkerReportsProgress = true;
            this.iso2God1.WorkerSupportsCancellation = true;
            // 
            // ftpUploader1
            // 
            this.ftpUploader1.WorkerReportsProgress = true;
            // 
            // AddISO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 350);
            this.Controls.Add(this.progressBarMulti);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAddIso);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddISO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add ISO Image";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTime)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPadding)).EndInit();
            this.ResumeLayout(false);

    }

    public AddISO(IsoEntryPlatform Platform)
    {
        InitializeComponent();
        base.Shown += AddISO_Shown;
        cmbPaddingMode.SelectedIndex = (int)Properties.Settings.Default["DefaultPadding"];
        platform = Platform;
        entry.Platform = platform;
        progressBarMulti.Visible = false;
        isoDetails.ProgressChanged += isoDetails_ProgressChanged;
        isoDetails.RunWorkerCompleted += isoDetails_RunWorkerCompleted;
        txtDest.Text = Properties.Settings.Default["OutputPath"].ToString();
        txtRebuiltIso.Text = Properties.Settings.Default["RebuildPath"].ToString();
        cbSaveRebuilt.Checked = (bool)Properties.Settings.Default["AlwaysSave"];
        ttISO.SetToolTip(pbVideo, 
            "Select the ISO images to convert to Games on Demand packages.\n" +
            "Selecting multiple files will automatically add them, you can then\n" +
            "individually edit each file details in the main list before conversion.\n\n" +
            "Choose a location to output the GOD packages to.\n" +
            "It will be written into sub-directories named using the TitleID.\n" +
            "A default location can be set in the Settings screen.");
        ttSettings.SetToolTip(pbTime, 
            "The details are automatically extracted from default.xex in the root\n" +
            "directory of the ISO image. They can be manually altered if required.\n\n" +
            "The Title Name MUST be entered, or else it will show up as an \n" +
            "unknown game on the 360.");
        ttThumb.SetToolTip(pbThumb, "Click to set a custom thumbnail for this title.");
        ttPadding.SetToolTip(pbPadding, 
            "Unused padding sectors can be removed from the ISO image.\n\n" +
            "Three modes are available:\n\n" +
            "  - Untouched\n" +
            "     ISO image is left untouched.\n\n" +
            "  - Partial\n" +
            "     The ISO image is cropped after the end of the last used sector.\n" +
            "     Very quick to do, but it will only save 800-1500MB of space.\n\n" +
            "  - Remove All\n" +
            "     ISO image is processed and completely rebuilt to remove all padding.\n" +
            "     Rebuilt image can be saved temporarily or kept for future use.");
        txtISO.Focus();
    }

    private void AddISO_Shown(object sender, EventArgs e)
    {
        if (!edit && (bool)Properties.Settings.Default["AutoBrowse"])
        {
            btnISOBrowse_Click(base.Owner, null);
        }
    }

    public void Edit(int Index, IsoEntry Entry)
    {
        edit = true;
        entry = Entry;
        entryIndex = Index;
        txtName.Text = entry.TitleName;
        txtTitleID.Text = entry.ID.TitleID;
        txtMediaID.Text = entry.ID.MediaID;
        txtPlatform.Text = entry.ID.Platform.ToString();
        txtExType.Text = entry.ID.ExType.ToString();
        txtDiscCount.Text = entry.ID.DiscCount.ToString();
        txtDiscNum.Text = entry.ID.DiscNumber.ToString();
        txtDest.Text = entry.Destination;
        txtISO.Text = entry.Path;
        txtRebuiltIso.Text = ((entry.Padding.Type == IsoEntryPaddingRemoval.Full) ? entry.Padding.IsoPath : Properties.Settings.Default["RebuildPath"].ToString());
        cbSaveRebuilt.Checked = entry.Padding.KeepIso;
        cmbPaddingMode.SelectedIndex = (int)entry.Padding.Type;
        pbThumb.Image = ((entry.Thumb == null) ? null : Image.FromStream(new MemoryStream(entry.Thumb)));
        pbThumb.Tag = entry.Thumb;
        Text = "Edit ISO Image";
        btnAddIso.Text = "Save";
    }

    private void isoDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (Application.OpenForms[Name] != null)
        {
            if (e.Result == null)
            {
                txtName.Text = "Failed to read details from ISO image.";
                return;
            }

            IsoDetailsResults isoDetailsResults = (IsoDetailsResults)e.Result;
            switch (isoDetailsResults.ConsolePlatform)
            {
                case IsoDetailsPlatform.Xbox:
                    platform = IsoEntryPlatform.Xbox;
                    break;
                case IsoDetailsPlatform.Xbox360:
                    platform = IsoEntryPlatform.Xbox360;
                    break;
            }
            bool flag = (bool)Properties.Settings.Default["AutoRenameMultiDisc"];
            int result = 0;
            int.TryParse(isoDetailsResults.DiscCount, out result);
            txtName.Text = ((flag && result > 1) ? (isoDetailsResults.Name + " - Disc " + isoDetailsResults.DiscNumber) : isoDetailsResults.Name);
            txtTitleID.Text = isoDetailsResults.TitleID;
            txtMediaID.Text = isoDetailsResults.MediaID;
            txtPlatform.Text = isoDetailsResults.Platform;
            txtExType.Text = isoDetailsResults.ExType;
            txtDiscNum.Text = isoDetailsResults.DiscNumber;
            txtDiscCount.Text = isoDetailsResults.DiscCount;
            if (isoDetailsResults.Thumbnail != null && isoDetailsResults.RawThumbnail != null)
            {
                pbThumb.Image = (Image)isoDetailsResults.Thumbnail.Clone();
                pbThumb.Tag = (byte[])isoDetailsResults.RawThumbnail.Clone();
                //MessageBox.Show(txtRebuiltIso.Text + txtTitleID.Text + ".bmp");
                //pbThumb.Image.Save(txtRebuiltIso.Text + txtTitleID.Text + ".bmp");
            }

            if (fileList.Length > 1)
            {
                IsoEntryPadding isoEntryPadding = new IsoEntryPadding();
                isoEntryPadding.Type = (IsoEntryPaddingRemoval)cmbPaddingMode.SelectedIndex;
                isoEntryPadding.TempPath = Path.GetTempPath();
                isoEntryPadding.IsoPath = txtRebuiltIso.Text;
                isoEntryPadding.KeepIso = cbSaveRebuilt.Checked;
                if (!isoEntryPadding.TempPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    isoEntryPadding.TempPath += Path.DirectorySeparatorChar;
                }
                if (!isoEntryPadding.IsoPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    isoEntryPadding.IsoPath += Path.DirectorySeparatorChar;
                }
                IsoEntryID iD = new IsoEntryID(txtTitleID.Text, txtMediaID.Text, byte.Parse(txtDiscNum.Text), byte.Parse(txtDiscCount.Text), byte.Parse(txtPlatform.Text), byte.Parse(txtExType.Text));
                FileInfo fileInfo = new FileInfo(txtISO.Text);
                IsoEntry isoEntry = new IsoEntry(platform, txtISO.Text, txtDest.Text, fileInfo.Length, txtName.Text, iD, (byte[])pbThumb.Tag, isoEntryPadding);
                if (edit)
                {
                    (base.Owner as Main).UpdateISOEntry(entryIndex, isoEntry);
                }
                else
                {
                    (base.Owner as Main).AddISOEntry(isoEntry);
                }

                progressBarMulti.PerformStep();
                progressBarMulti.Text = "Adding " + progressBarMulti.Value + " / " + fileList.Length + " files";

                file++;
                if (file < fileList.Length)
                {
                    txtISO.Text = fileList[file];
                    clearXexFields();
                    isoDetails.RunWorkerAsync(new IsoDetailsArgs(txtISO.Text, (base.Owner as Main).pathTemp, (base.Owner as Main).pathXT));
                }
                else
                {
                    btnCancel.Text = "OK";
                    EnablePageControls(true);
                    progressBarMulti.Text = "Added " + progressBarMulti.Value + " files";
                }
            }
        }
    }

    private void isoDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (Application.OpenForms[Name] != null)
        {
            IsoDetailsResults isoDetailsResults = (IsoDetailsResults)e.UserState;
            if (isoDetailsResults.Results == IsoDetailsResultsType.Error)
            {
                MessageBox.Show(isoDetailsResults.ErrorMessage, "Error Reading Title Information");
            }
            else
            {
                txtName.Text = isoDetailsResults.ProgressMessage;
            }
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if(isoDetails.IsBusy)
        {
            //isoDetails.CancelAsync();
            isoDetails.Dispose();
        }
        Close();
    }

    private bool checkPaths()
    {
        if (txtISO.Text.Length == 0 || txtDest.Text.Length == 0 || (cmbPaddingMode.SelectedIndex > 1 && txtRebuiltIso.Text.Length == 0))
        {
            string messageStart = "Please select";
            string messageIso = "an ISO image to convert";
            string messageGOD = "a destination folder to store the GOD container in";
            string messageRebuild = "a destination folder to store the " + (cbSaveRebuilt.Checked ? "" : "temporary ") + "rebuilt ISO image in";

            if (txtISO.Text.Length == 0 && txtDest.Text.Length == 0 && (cmbPaddingMode.SelectedIndex > 1 && txtRebuiltIso.Text.Length == 0))
            {
                MessageBox.Show(messageStart + ":\n- " + messageIso + "\n- " + messageGOD + "\n- " + messageRebuild);
                return false;
            }
            if (txtISO.Text.Length == 0 && txtDest.Text.Length == 0)
            {
                MessageBox.Show(messageStart + ":\n- " + messageIso + "\n- " + messageGOD);
                return false;
            }
            if (txtISO.Text.Length == 0 && (cmbPaddingMode.SelectedIndex > 1 && txtRebuiltIso.Text.Length == 0))
            {
                MessageBox.Show(messageStart + ":\n- " + messageIso + "\n- " + messageRebuild);
                return false;
            }
            if (txtDest.Text.Length == 0 && (cmbPaddingMode.SelectedIndex > 1 && txtRebuiltIso.Text.Length == 0))
            {
                MessageBox.Show(messageStart + ":\n- " + messageGOD + "\n- " + messageRebuild);
                return false;
            }
            if (txtISO.Text.Length == 0)
            {
                MessageBox.Show(messageStart + " " + messageIso);
                return false;
            }
            if (txtDest.Text.Length == 0)
            {
                MessageBox.Show(messageStart + " " + messageGOD);
                return false;
            }
            if ((cmbPaddingMode.SelectedIndex > 1 && txtRebuiltIso.Text.Length == 0))
            {
                MessageBox.Show(messageStart + " " + messageRebuild);
                return false;
            }
        }
        return true;
    }

    private bool checkFields()
    {
        if (!checkPaths())
        {
            return false;
        }

        if (txtTitleID.Text.Length != 8 || txtMediaID.Text.Length != 8 || txtExType.Text.Length == 0 || txtPlatform.Text.Length == 0)
        {
            MessageBox.Show("If you are overriding the Title Info fields, please ensure the Title and Media IDs are\n8 character Hex strings. All other fields should be no more than 1 character.\n\nIf these details were all blank after choosing an ISO image, an error has occured\nso the conversion is unlikely to work.");
            return false;
        }
        if (txtName.Text.Length == 0)
        {
            MessageBox.Show("The name of the game is currently not automatically detected.\n\nPlease enter this manually in the Name field above.");
            return false;
        }
        if (cmbPaddingMode.SelectedIndex > 1)
        {
            if (!cbSaveRebuilt.Checked && (bool)Properties.Settings.Default["RebuiltCheck"])
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to discard the temporary ISO after it has been rebuilt?", "Discard Temporary ISO Image", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (checkFields())
        {
            IsoEntryPadding isoEntryPadding = new IsoEntryPadding();
            isoEntryPadding.Type = (IsoEntryPaddingRemoval)cmbPaddingMode.SelectedIndex;
            isoEntryPadding.TempPath = Path.GetTempPath();
            isoEntryPadding.IsoPath = txtRebuiltIso.Text;
            isoEntryPadding.KeepIso = cbSaveRebuilt.Checked;
            if (!isoEntryPadding.TempPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                isoEntryPadding.TempPath += Path.DirectorySeparatorChar;
            }
            if (!isoEntryPadding.IsoPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                isoEntryPadding.IsoPath += Path.DirectorySeparatorChar;
            }
            IsoEntryID iD = new IsoEntryID(txtTitleID.Text, txtMediaID.Text, byte.Parse(txtDiscNum.Text), byte.Parse(txtDiscCount.Text), byte.Parse(txtPlatform.Text), byte.Parse(txtExType.Text));
            FileInfo fileInfo = new FileInfo(txtISO.Text);
            IsoEntry isoEntry = new IsoEntry(platform, txtISO.Text, txtDest.Text, fileInfo.Length, txtName.Text, iD, (byte[])pbThumb.Tag, isoEntryPadding);
            if (edit)
            {
                (base.Owner as Main).UpdateISOEntry(entryIndex, isoEntry);
            }
            else
            {
                (base.Owner as Main).AddISOEntry(isoEntry);
            }
            GC.Collect();
            Close();
        }
    }

    private void btnISOBrowse_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
        openFileDialog.Title = "Choose location of your ISO.";
        openFileDialog.Multiselect = !edit;
        openFileDialog.Filter = "ISO Images (*.iso, *.000)|*.iso;*.000";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            file = 0;
            fileList = openFileDialog.FileNames;
            txtISO.Text = openFileDialog.FileName;
            btnCancel.Text = "Cancel";
            clearXexFields();
            if (fileList.Length > 1)
            {
                if(!checkPaths())
                {
                    return;
                }
                progressBarMulti.Maximum = fileList.Length;
                progressBarMulti.Step = 1;
                progressBarMulti.Value = 0;
                progressBarMulti.Visible = true;
                EnablePageControls(false);
                btnAddIso.Enabled = false;
            } else
            {
                btnAddIso.Enabled = true;
            }
            switch (entry.Platform)
            {
                case IsoEntryPlatform.Xbox360:
                    isoDetails.RunWorkerAsync(new IsoDetailsArgs(txtISO.Text, (base.Owner as Main).pathTemp, (base.Owner as Main).pathXT));
                    txtName.Text = "Reading default.xex...";
                    break;
                case IsoEntryPlatform.Xbox:
                    isoDetails.RunWorkerAsync(new IsoDetailsArgs(txtISO.Text, (base.Owner as Main).pathTemp, (base.Owner as Main).pathXT));
                    txtName.Text = "Reading default.xbe...";
                    break;
            }
        }
    }

    private void btnDestBrowse_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
        folderBrowserDialog.ShowNewFolderButton = true;
        folderBrowserDialog.Description = "Choose where to save the GOD Package to:";
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            txtDest.Text = folderBrowserDialog.SelectedPath;
            if (!txtDest.Text.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                txtDest.Text += Path.DirectorySeparatorChar;
            }
        }
    }

    private void clearXexFields()
    {
        txtTitleID.Text = "";
        txtMediaID.Text = "";
        txtDiscNum.Text = "";
        txtDiscCount.Text = "";
        txtExType.Text = "";
        txtPlatform.Text = "";
        pbThumb.Image = null;
        pbThumb.Tag = null;
        txtName.Text = "";
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
        openFileDialog.Title = "Choose an image to use as the Title Thumbnail:";
        openFileDialog.Filter = "Supported Images (*.png, *.jpg, *.bmp)|*.png;*.jpg;*.bmp";
        openFileDialog.Multiselect = false;
        if (openFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        if (!File.Exists(openFileDialog.FileName))
        {
            MessageBox.Show("Could not locate specified thumbnail.");
            return;
        }
        byte[] tag = File.ReadAllBytes(openFileDialog.FileName);
        Image image = Image.FromFile(openFileDialog.FileName);
        Image image2 = new Bitmap(64, 64);
        if (image.Width > 64 || image.Height > 64)
        {
            Graphics graphics = Graphics.FromImage(image2);
            graphics.DrawImage(image, 0, 0, 64, 64);
            pbThumb.Image = (Image)image2.Clone();
            MemoryStream memoryStream = new MemoryStream();
            image2.Save(memoryStream, ImageFormat.Png);
            pbThumb.Tag = memoryStream.ToArray();
        }
        else
        {
            pbThumb.Image = (Image)image.Clone();
            pbThumb.Tag = tag;
        }
    }

    private void cbSaveRebuilt_CheckedChanged(object sender, EventArgs e)
    {
        if (!(bool)Properties.Settings.Default["AlwaysSave"] || (cmbPaddingMode.SelectedIndex < 2))
        {
            cbSaveRebuilt.Checked = false;
            cbSaveRebuilt.Enabled = false;
        } else
        {
            cbSaveRebuilt.Enabled = true;
        }
    }

    private void cmbPaddingMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbPaddingMode.SelectedIndex < 2)
        {
            /* if (!(bool)Properties.Settings.Default["AlwaysSave"])
            {
                cbSaveRebuilt.Checked = false;
            }*/
            cbSaveRebuilt.Enabled = false;
            txtRebuiltIso.Enabled = false;
            btnRebuiltBrowse.Enabled = false;
        }
        else
        {
            cbSaveRebuilt.Enabled = true;
            txtRebuiltIso.Enabled = true;
            btnRebuiltBrowse.Enabled = true;
        }
    }

    private void btnRebuiltBrowse_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
        folderBrowserDialog.ShowNewFolderButton = true;
        folderBrowserDialog.Description = "Choose where to save the rebuilt ISO to:";
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            txtRebuiltIso.Text = folderBrowserDialog.SelectedPath;
            if (!txtRebuiltIso.Text.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                txtRebuiltIso.Text += Path.DirectorySeparatorChar;
            }
        }
    }

    private void EnablePageControls(bool status)
    {
        txtName.Enabled = status;
        txtTitleID.Enabled = status;
        txtMediaID.Enabled = status;
        txtDiscNum.Enabled = status;
        txtDiscCount.Enabled = status;
        txtPlatform.Enabled = status;
        txtExType.Enabled = status;
        pbThumb.Enabled = status;
        cbSaveRebuilt.Enabled = status;
        cmbPaddingMode.Enabled = status;
        if (cmbPaddingMode.SelectedIndex < 2)
        {
            cbSaveRebuilt.Enabled = false;
            txtRebuiltIso.Enabled = false;
        }
        else
        {
            cbSaveRebuilt.Enabled = status;
            txtRebuiltIso.Enabled = status;
        }
        btnRebuiltBrowse.Enabled = status;
    }
}
