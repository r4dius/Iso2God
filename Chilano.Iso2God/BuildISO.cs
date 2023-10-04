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

public class BuildISO : Form
{
    private IContainer components;

    private GroupBox groupBox1;

    private PictureBox pbVideo;

    private Button btnDestBrowse;

    private Label label2;

    private TextBox txtDest;

    private Button btnISOBrowse;

    private Label label1;

    private TextBox txtISO;

    private ToolTip ttISO;

    private GroupBox groupBox2;

    private PictureBox pbTime;

    private Label label7;

    private Label label4;

    private TextBox txtMediaID;

    private TextBox txtTitleID;

    private Button btnAddIso;

    private Button btnCancel;

    private TextBox txtName;

    private Label label3;

    private Label label6;

    private Label label5;

    private TextBox txtExType;

    private TextBox txtPlatform;

    private Label label9;

    private TextBox txtDiscCount;

    private TextBox txtDiscNum;

    private Label label8;

    private ToolTip ttSettings;

    private PictureBox pbThumb;

    private ToolTip ttThumb;

    private GroupBox groupBox3;

    private ComboBox cmbPaddingMode;

    private Label label10;

    private PictureBox pbPadding;

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

    private ToolTip ttFTP;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnISOBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtISO = new System.Windows.Forms.TextBox();
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.btnDestBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.ttISO = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbThumb = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExType = new System.Windows.Forms.TextBox();
            this.txtPlatform = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDiscCount = new System.Windows.Forms.TextBox();
            this.txtDiscNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMediaID = new System.Windows.Forms.TextBox();
            this.txtTitleID = new System.Windows.Forms.TextBox();
            this.pbTime = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddIso = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ttSettings = new System.Windows.Forms.ToolTip(this.components);
            this.ttThumb = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbPaddingMode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pbPadding = new System.Windows.Forms.PictureBox();
            this.ttPadding = new System.Windows.Forms.ToolTip(this.components);
            this.ttFTP = new System.Windows.Forms.ToolTip(this.components);
            this.progressBarMulti = new Chilano.Common.ProgressBarEx();
            this.iso2God1 = new Chilano.Iso2God.Iso2God();
            this.ftpUploader1 = new Chilano.Iso2God.Ftp.FtpUploader();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTime)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPadding)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btnISOBrowse);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtISO);
            this.groupBox1.Controls.Add(this.pbVideo);
            this.groupBox1.Controls.Add(this.btnDestBrowse);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDest);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ISO Details";
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
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ISO Image:";
            // 
            // txtISO
            // 
            this.txtISO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtISO.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtISO.Location = new System.Drawing.Point(87, 20);
            this.txtISO.Name = "txtISO";
            this.txtISO.ReadOnly = true;
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
            this.btnDestBrowse.Location = new System.Drawing.Point(364, 48);
            this.btnDestBrowse.Name = "btnDestBrowse";
            this.btnDestBrowse.Size = new System.Drawing.Size(70, 25);
            this.btnDestBrowse.TabIndex = 3;
            this.btnDestBrowse.Text = "&Browse";
            this.btnDestBrowse.UseVisualStyleBackColor = true;
            this.btnDestBrowse.Click += new System.EventHandler(this.btnDestBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Output Path:";
            // 
            // txtDest
            // 
            this.txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDest.Location = new System.Drawing.Point(87, 49);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.pbThumb);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtExType);
            this.groupBox2.Controls.Add(this.txtPlatform);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtDiscCount);
            this.groupBox2.Controls.Add(this.txtDiscNum);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtMediaID);
            this.groupBox2.Controls.Add(this.txtTitleID);
            this.groupBox2.Controls.Add(this.pbTime);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(10, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 115);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Title Details";
            // 
            // pbThumb
            // 
            this.pbThumb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbThumb.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.preview;
            this.pbThumb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbThumb.Location = new System.Drawing.Point(366, 28);
            this.pbThumb.Name = "pbThumb";
            this.pbThumb.Padding = new System.Windows.Forms.Padding(1);
            this.pbThumb.Size = new System.Drawing.Size(66, 66);
            this.pbThumb.TabIndex = 40;
            this.pbThumb.TabStop = false;
            this.pbThumb.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(308, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "/";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(234, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Disc:";
            // 
            // txtExType
            // 
            this.txtExType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExType.Location = new System.Drawing.Point(327, 78);
            this.txtExType.MaxLength = 1;
            this.txtExType.Name = "txtExType";
            this.txtExType.Size = new System.Drawing.Size(30, 23);
            this.txtExType.TabIndex = 10;
            // 
            // txtPlatform
            // 
            this.txtPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlatform.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlatform.Location = new System.Drawing.Point(269, 78);
            this.txtPlatform.MaxLength = 1;
            this.txtPlatform.Name = "txtPlatform";
            this.txtPlatform.Size = new System.Drawing.Size(30, 23);
            this.txtPlatform.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
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
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(303, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Ex:";
            // 
            // txtDiscCount
            // 
            this.txtDiscCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscCount.Location = new System.Drawing.Point(327, 49);
            this.txtDiscCount.MaxLength = 1;
            this.txtDiscCount.Name = "txtDiscCount";
            this.txtDiscCount.Size = new System.Drawing.Size(30, 23);
            this.txtDiscCount.TabIndex = 7;
            // 
            // txtDiscNum
            // 
            this.txtDiscNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscNum.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscNum.Location = new System.Drawing.Point(269, 49);
            this.txtDiscNum.MaxLength = 1;
            this.txtDiscNum.Name = "txtDiscNum";
            this.txtDiscNum.Size = new System.Drawing.Size(30, 23);
            this.txtDiscNum.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Media ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Title ID:";
            // 
            // txtMediaID
            // 
            this.txtMediaID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMediaID.Location = new System.Drawing.Point(87, 78);
            this.txtMediaID.Name = "txtMediaID";
            this.txtMediaID.Size = new System.Drawing.Size(141, 23);
            this.txtMediaID.TabIndex = 8;
            // 
            // txtTitleID
            // 
            this.txtTitleID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitleID.Location = new System.Drawing.Point(87, 49);
            this.txtTitleID.MaxLength = 8;
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
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(234, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Plat:";
            // 
            // btnAddIso
            // 
            this.btnAddIso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddIso.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddIso.Location = new System.Drawing.Point(299, 280);
            this.btnAddIso.Name = "btnAddIso";
            this.btnAddIso.Size = new System.Drawing.Size(75, 25);
            this.btnAddIso.TabIndex = 3;
            this.btnAddIso.Text = "Add";
            this.btnAddIso.UseVisualStyleBackColor = true;
            this.btnAddIso.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(380, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
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
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.cmbPaddingMode);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.pbPadding);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(10, 214);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 58);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // cmbPaddingMode
            // 
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
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Padding:";
            // 
            // pbPadding
            // 
            this.pbPadding.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbPadding.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPadding.Location = new System.Drawing.Point(58, 0);
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
            // ttFTP
            // 
            this.ttFTP.AutoPopDelay = 30000;
            this.ttFTP.InitialDelay = 100;
            this.ttFTP.IsBalloon = true;
            this.ttFTP.ReshowDelay = 100;
            this.ttFTP.ShowAlways = true;
            // 
            // progressBarMulti
            // 
            this.progressBarMulti.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMulti.DisplayStyle = Chilano.Common.ProgressBarDisplayText.Text;
            this.progressBarMulti.Location = new System.Drawing.Point(10, 281);
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
            // BuildISO
            // 
            this.AcceptButton = this.btnAddIso;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(464, 314);
            this.Controls.Add(this.progressBarMulti);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnAddIso);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuildISO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Build ISO Image";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTime)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPadding)).EndInit();
            this.ResumeLayout(false);

    }

    public BuildISO(IsoEntryPlatform Platform)
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
            string message = "";
            bool flag = (bool)Properties.Settings.Default["AutoRenameMultiDisc"];
            int result = 0;
            int.TryParse(isoDetailsResults.DiscCount, out result);
            txtName.Text = ((flag && result > 1) ? (isoDetailsResults.Name + " - Disc " + isoDetailsResults.DiscNumber) : isoDetailsResults.Name);
            if (txtName.Text.Length == 0)
            {
                txtName.Text = "?";
                message = "The name of the game was not automatically detected";
            }
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
                IsoEntry isoEntry = new IsoEntry(platform, txtISO.Text, txtDest.Text, fileInfo.Length, txtName.Text, iD, (byte[])pbThumb.Tag, isoEntryPadding, message, false) ;
                if (edit)
                {
                    (base.Owner as Main).UpdateISOEntry(entryIndex, isoEntry);
                }
                else
                {
                    (base.Owner as Main).AddISOEntry(isoEntry);
                }

                progressBarMulti.PerformStep();
                progressBarMulti.Text = "Adding " + (progressBarMulti.Value + 1) + " / " + fileList.Length + " files";

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
                    progressBarMulti.Text = progressBarMulti.Value + " files added";
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
        if (txtISO.Text.Length == 0 || txtDest.Text.Length == 0 || (cmbPaddingMode.SelectedIndex > 1))
        {
            string message = "";
            string sep = "";
            string messageStart = "Please select";
            string messageISO = "An ISO image to convert";
            string messageGOD = "A destination folder to store the GOD container in";
            string messageRebuild = "A destination folder to store the rebuilt ISO image in";
            int errISO = 0, errGOD = 0, errRebuild = 0;

            if (txtISO.Text.Length == 0) errISO = 1;
            if (txtDest.Text.Length == 0) errGOD = 1;
            if (cmbPaddingMode.SelectedIndex > 1) errRebuild = 1;

            if (errISO + errGOD + errRebuild > 1)
            {
                sep = "\n - ";
                messageStart += ":";
            }
            message = (errISO == 1 ? sep + messageISO : "") + (errGOD == 1 ? sep + messageGOD : "") + (errRebuild == 1 ? sep + messageRebuild : "");
            if (errISO + errGOD + errRebuild > 1) {
                message = char.ToLower(message[0]) + message.Substring(1);
            }
            MessageBox.Show(messageStart + " " + message);
            return false;
        }
        return true;
    }

    private bool checkFields()
    {
        if (!checkPaths())
        {
            return false;
        }

        if (txtName.Text.Length == 0 || txtTitleID.Text.Length != 8 || txtMediaID.Text.Length != 8 || txtDiscNum.Text.Length == 0 || txtDiscNum.Text == "0" || txtPlatform.Text.Length == 0 || txtExType.Text.Length == 0)
        {
            string message = "";
            string sep = "";
            string messageStart = "Please ensure";
            string messageName = "The Name of the game is not empty";
            string messageTitle = "The Title ID is 8 character Hex strings";
            string messageMedia = "The Media ID is 8 character Hex strings";
            string messageDisc = "Disk number is greater than 0";
            string messagePlatform = "The Platform number (Plat) is not empty";
            string messageEx = "The Ex number is not empty";
            int errName = 0, errTitle = 0, errMedia = 0, errDisc = 0, errPlatform = 0, errEx = 0;

            if (txtName.Text.Length == 0) errName = 1;
            if (txtTitleID.Text.Length != 8) errTitle = 1;
            if (txtMediaID.Text.Length != 8) errMedia = 1;
            if (txtDiscNum.Text.Length == 0 || txtDiscNum.Text == "0") errDisc = 1;
            if (txtPlatform.Text.Length == 0) errPlatform = 1;
            if (txtExType.Text.Length == 0) errEx = 1;

            if (errName + errTitle + errMedia + errDisc + errPlatform + errEx > 1)
            {
                sep = "\n - ";
                messageStart += ":";
            }
            message = (errName == 1 ? sep + messageName : "") + (errTitle == 1 ? sep + messageTitle : "") + (errMedia == 1 ? sep + messageMedia : "") + (errDisc == 1 ? sep + messageDisc : "") + (errPlatform == 1 ? sep + messagePlatform : "") + (errEx == 1 ? sep + messageEx : "");
            if (errName + errTitle + errMedia + errDisc + errPlatform + errEx > 1)
            {
                message = char.ToLower(message[0]) + message.Substring(1);
            }
            MessageBox.Show(messageStart + " " + message);
            return false;
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
            isoEntryPadding.KeepGod = false;
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
            IsoEntry isoEntry = new IsoEntry(platform, txtISO.Text, txtDest.Text, fileInfo.Length, txtName.Text, iD, (byte[])pbThumb.Tag, isoEntryPadding, "", false);
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
                progressBarMulti.Text = "Adding 1 / " + fileList.Length + " files";
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
        folderBrowserDialog.SelectedPath = txtDest.Text;
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

    private void EnablePageControls(bool status)
    {
        txtISO.Enabled = status;
        txtDest.Enabled = status;
        txtName.Enabled = status;
        txtTitleID.Enabled = status;
        txtMediaID.Enabled = status;
        txtDiscNum.Enabled = status;
        txtDiscCount.Enabled = status;
        txtPlatform.Enabled = status;
        txtExType.Enabled = status;
        pbThumb.Enabled = status;
        cmbPaddingMode.Enabled = status;
        btnDestBrowse.Enabled = status;
        btnISOBrowse.Enabled = status;
    }
}
