using Chilano.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Chilano.Iso2God;

public class AddISO : Form
{
    private IContainer components;

    private GroupBox groupBox1;

    private PictureBox pbSource;

    private Button btnDestBrowse;

    private Label label2;

    private TextBox txtDest;

    private Button btnISOBrowse;

    private Label label1;

    private TextBox txtISO;

    private ToolTip ttSource;

    private GroupBox groupBox3;

    private PictureBox pbTitleDetails;

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

    private Label label9;

    private Label label8;

    private ToolTip ttTitleDetails;

    private PictureBox pbThumb;

    private ToolTip ttThumb;

    private GroupBox groupBox4;

    private ComboBox cmbPaddingMode;

    private Label label10;

    private Label label11;

    private TextBox txtRebuiltIso;

    private PictureBox pbOptions;

    private Button btnRebuiltBrowse;

    private ToolTip ttOptions;

    private IsoDetails isoDetails = new IsoDetails();

    private bool edit;

    private bool drop;

    private string[] fileList;

    private int file;

    private int processErrors;

    private int lastLayout;

    private IsoEntryPlatform platform = IsoEntryPlatform.Xbox360;

    private IsoEntry entry;
    private Iso2God iso2God1;
    private Ftp.FtpUploader ftpUploader1;
    private ProgressBarEx progressBarMulti;
    private NumericUpDown txtDiscNum;
    private NumericUpDown txtExType;
    private NumericUpDown txtPlatform;
    private NumericUpDown txtDiscCount;
    private CheckBox cbAutoRename;
    private CheckBox cbDeleteSource;
    private GroupBox groupBox2;
    private PictureBox pbOutput;
    private ComboBox cmbFormat;
    private Label label12;
    private CheckBox cbDeleteGod;
    private ComboBox cmbLayout;
    private Label label13;
    private CheckBox cbFtpUpload;
    private ToolTip ttGodLayout;
    private ToolTip ttOutput;
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
            this.pbSource = new System.Windows.Forms.PictureBox();
            this.btnDestBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.ttSource = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtExType = new System.Windows.Forms.NumericUpDown();
            this.txtPlatform = new System.Windows.Forms.NumericUpDown();
            this.txtDiscCount = new System.Windows.Forms.NumericUpDown();
            this.txtDiscNum = new System.Windows.Forms.NumericUpDown();
            this.pbThumb = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMediaID = new System.Windows.Forms.TextBox();
            this.txtTitleID = new System.Windows.Forms.TextBox();
            this.pbTitleDetails = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbAutoRename = new System.Windows.Forms.CheckBox();
            this.btnAddIso = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ttTitleDetails = new System.Windows.Forms.ToolTip(this.components);
            this.ttThumb = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbFtpUpload = new System.Windows.Forms.CheckBox();
            this.cmbLayout = new System.Windows.Forms.ComboBox();
            this.cbDeleteGod = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbDeleteSource = new System.Windows.Forms.CheckBox();
            this.cmbPaddingMode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pbOptions = new System.Windows.Forms.PictureBox();
            this.btnRebuiltBrowse = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRebuiltIso = new System.Windows.Forms.TextBox();
            this.ttOptions = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbOutput = new System.Windows.Forms.PictureBox();
            this.ttGodLayout = new System.Windows.Forms.ToolTip(this.components);
            this.ttOutput = new System.Windows.Forms.ToolTip(this.components);
            this.progressBarMulti = new Chilano.Common.ProgressBarEx();
            this.iso2God1 = new Chilano.Iso2God.Iso2God();
            this.ftpUploader1 = new Chilano.Iso2God.Ftp.FtpUploader();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlatform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitleDetails)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOptions)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).BeginInit();
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
            this.groupBox1.Controls.Add(this.pbSource);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            // 
            // btnISOBrowse
            // 
            this.btnISOBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnISOBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnISOBrowse.Location = new System.Drawing.Point(386, 19);
            this.btnISOBrowse.Name = "btnISOBrowse";
            this.btnISOBrowse.Size = new System.Drawing.Size(68, 25);
            this.btnISOBrowse.TabIndex = 2;
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
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ISO Image(s):";
            // 
            // txtISO
            // 
            this.txtISO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtISO.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtISO.Location = new System.Drawing.Point(87, 20);
            this.txtISO.Name = "txtISO";
            this.txtISO.ReadOnly = true;
            this.txtISO.Size = new System.Drawing.Size(292, 23);
            this.txtISO.TabIndex = 1;
            // 
            // pbSource
            // 
            this.pbSource.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSource.Location = new System.Drawing.Point(52, 0);
            this.pbSource.Name = "pbSource";
            this.pbSource.Size = new System.Drawing.Size(14, 14);
            this.pbSource.TabIndex = 25;
            this.pbSource.TabStop = false;
            // 
            // btnDestBrowse
            // 
            this.btnDestBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDestBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDestBrowse.Location = new System.Drawing.Point(386, 19);
            this.btnDestBrowse.Name = "btnDestBrowse";
            this.btnDestBrowse.Size = new System.Drawing.Size(68, 25);
            this.btnDestBrowse.TabIndex = 4;
            this.btnDestBrowse.Text = "&Browse";
            this.btnDestBrowse.UseVisualStyleBackColor = true;
            this.btnDestBrowse.Click += new System.EventHandler(this.btnDestBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "GOD Path:";
            // 
            // txtDest
            // 
            this.txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDest.Location = new System.Drawing.Point(87, 20);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
            this.txtDest.Size = new System.Drawing.Size(292, 23);
            this.txtDest.TabIndex = 3;
            // 
            // ttSource
            // 
            this.ttSource.AutoPopDelay = 30000;
            this.ttSource.InitialDelay = 100;
            this.ttSource.IsBalloon = true;
            this.ttSource.ReshowDelay = 100;
            this.ttSource.ShowAlways = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.txtExType);
            this.groupBox3.Controls.Add(this.txtPlatform);
            this.groupBox3.Controls.Add(this.txtDiscCount);
            this.groupBox3.Controls.Add(this.txtDiscNum);
            this.groupBox3.Controls.Add(this.pbThumb);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtName);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtMediaID);
            this.groupBox3.Controls.Add(this.txtTitleID);
            this.groupBox3.Controls.Add(this.pbTitleDetails);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(10, 158);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 119);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Title Details";
            // 
            // txtExType
            // 
            this.txtExType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExType.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtExType.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtExType.Location = new System.Drawing.Point(343, 83);
            this.txtExType.Name = "txtExType";
            this.txtExType.Size = new System.Drawing.Size(36, 22);
            this.txtExType.TabIndex = 13;
            this.txtExType.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ScrollHandlerFunction);
            // 
            // txtPlatform
            // 
            this.txtPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlatform.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtPlatform.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtPlatform.Location = new System.Drawing.Point(279, 83);
            this.txtPlatform.Name = "txtPlatform";
            this.txtPlatform.Size = new System.Drawing.Size(36, 22);
            this.txtPlatform.TabIndex = 12;
            this.txtPlatform.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ScrollHandlerFunction);
            // 
            // txtDiscCount
            // 
            this.txtDiscCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscCount.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDiscCount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtDiscCount.Location = new System.Drawing.Point(343, 52);
            this.txtDiscCount.Name = "txtDiscCount";
            this.txtDiscCount.Size = new System.Drawing.Size(36, 22);
            this.txtDiscCount.TabIndex = 11;
            this.txtDiscCount.ValueChanged += new System.EventHandler(this.txtDiscCount_ValueChanged);
            this.txtDiscCount.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ScrollHandlerFunction);
            // 
            // txtDiscNum
            // 
            this.txtDiscNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscNum.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDiscNum.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtDiscNum.Location = new System.Drawing.Point(279, 52);
            this.txtDiscNum.Name = "txtDiscNum";
            this.txtDiscNum.Size = new System.Drawing.Size(36, 22);
            this.txtDiscNum.TabIndex = 10;
            this.txtDiscNum.ValueChanged += new System.EventHandler(this.txtDiscNum_ValueChanged);
            this.txtDiscNum.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ScrollHandlerFunction);
            // 
            // pbThumb
            // 
            this.pbThumb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbThumb.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.preview;
            this.pbThumb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbThumb.Location = new System.Drawing.Point(387, 20);
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
            this.label6.Location = new System.Drawing.Point(324, 56);
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
            this.label5.Location = new System.Drawing.Point(244, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Disc:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 25);
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
            this.txtName.Size = new System.Drawing.Size(292, 23);
            this.txtName.TabIndex = 7;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(319, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Ex:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Media ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Title ID:";
            // 
            // txtMediaID
            // 
            this.txtMediaID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMediaID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMediaID.Location = new System.Drawing.Point(87, 82);
            this.txtMediaID.MaxLength = 8;
            this.txtMediaID.Name = "txtMediaID";
            this.txtMediaID.Size = new System.Drawing.Size(152, 23);
            this.txtMediaID.TabIndex = 9;
            // 
            // txtTitleID
            // 
            this.txtTitleID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitleID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitleID.Location = new System.Drawing.Point(87, 51);
            this.txtTitleID.MaxLength = 8;
            this.txtTitleID.Name = "txtTitleID";
            this.txtTitleID.Size = new System.Drawing.Size(152, 23);
            this.txtTitleID.TabIndex = 8;
            this.txtTitleID.TextChanged += new System.EventHandler(this.txtTitleID_TextChanged);
            // 
            // pbTitleDetails
            // 
            this.pbTitleDetails.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbTitleDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbTitleDetails.Location = new System.Drawing.Point(77, 0);
            this.pbTitleDetails.Name = "pbTitleDetails";
            this.pbTitleDetails.Size = new System.Drawing.Size(14, 14);
            this.pbTitleDetails.TabIndex = 25;
            this.pbTitleDetails.TabStop = false;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(244, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Plat:";
            // 
            // cbAutoRename
            // 
            this.cbAutoRename.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbAutoRename.AutoSize = true;
            this.cbAutoRename.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoRename.Location = new System.Drawing.Point(249, 88);
            this.cbAutoRename.Name = "cbAutoRename";
            this.cbAutoRename.Size = new System.Drawing.Size(182, 17);
            this.cbAutoRename.TabIndex = 20;
            this.cbAutoRename.Text = "Auto-rename multi-disc games";
            this.cbAutoRename.UseVisualStyleBackColor = true;
            // 
            // btnAddIso
            // 
            this.btnAddIso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddIso.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddIso.Location = new System.Drawing.Point(319, 408);
            this.btnAddIso.Name = "btnAddIso";
            this.btnAddIso.Size = new System.Drawing.Size(75, 25);
            this.btnAddIso.TabIndex = 21;
            this.btnAddIso.Text = "Add";
            this.btnAddIso.UseVisualStyleBackColor = true;
            this.btnAddIso.Click += new System.EventHandler(this.btnAddIso_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(400, 408);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ttTitleDetails
            // 
            this.ttTitleDetails.AutoPopDelay = 30000;
            this.ttTitleDetails.InitialDelay = 100;
            this.ttTitleDetails.IsBalloon = true;
            this.ttTitleDetails.ReshowDelay = 100;
            this.ttTitleDetails.ShowAlways = true;
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
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.cbFtpUpload);
            this.groupBox4.Controls.Add(this.cmbLayout);
            this.groupBox4.Controls.Add(this.cbDeleteGod);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.cmbFormat);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.cbAutoRename);
            this.groupBox4.Controls.Add(this.cbDeleteSource);
            this.groupBox4.Controls.Add(this.cmbPaddingMode);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.pbOptions);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox4.Location = new System.Drawing.Point(10, 281);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(464, 119);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Options";
            // 
            // cbFtpUpload
            // 
            this.cbFtpUpload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbFtpUpload.AutoSize = true;
            this.cbFtpUpload.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFtpUpload.Location = new System.Drawing.Point(249, 22);
            this.cbFtpUpload.Name = "cbFtpUpload";
            this.cbFtpUpload.Size = new System.Drawing.Size(182, 17);
            this.cbFtpUpload.TabIndex = 17;
            this.cbFtpUpload.Text = "Transfer GOD packages via FTP";
            this.cbFtpUpload.UseVisualStyleBackColor = true;
            this.cbFtpUpload.CheckedChanged += new System.EventHandler(this.cbFTP_CheckedChanged);
            // 
            // cmbLayout
            // 
            this.cmbLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLayout.FormattingEnabled = true;
            this.cmbLayout.Items.AddRange(new object[] {
            "\\Title ID\\",
            "\\Title Name\\Title ID\\",
            "\\Title Name Title ID\\",
            "\\Title Name\\"});
            this.cmbLayout.Location = new System.Drawing.Point(88, 82);
            this.cmbLayout.Name = "cmbLayout";
            this.cmbLayout.Size = new System.Drawing.Size(151, 23);
            this.cmbLayout.TabIndex = 16;
            this.cmbLayout.SelectedIndexChanged += new System.EventHandler(this.cmbFolderLayout_SelectedIndexChanged);
            // 
            // cbDeleteGod
            // 
            this.cbDeleteGod.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbDeleteGod.AutoSize = true;
            this.cbDeleteGod.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeleteGod.Location = new System.Drawing.Point(249, 44);
            this.cbDeleteGod.Name = "cbDeleteGod";
            this.cbDeleteGod.Size = new System.Drawing.Size(201, 17);
            this.cbDeleteGod.TabIndex = 18;
            this.cbDeleteGod.Text = "Delete GOD files after FTP transfer";
            this.cbDeleteGod.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "GOD Layout:";
            // 
            // cmbFormat
            // 
            this.cmbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Items.AddRange(new object[] {
            "GOD",
            "GOD + ISO",
            "ISO"});
            this.cmbFormat.Location = new System.Drawing.Point(87, 20);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(152, 23);
            this.cmbFormat.TabIndex = 14;
            this.cmbFormat.SelectedIndexChanged += new System.EventHandler(this.cmbFormat_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Format:";
            // 
            // cbDeleteSource
            // 
            this.cbDeleteSource.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbDeleteSource.AutoSize = true;
            this.cbDeleteSource.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeleteSource.Location = new System.Drawing.Point(249, 66);
            this.cbDeleteSource.Name = "cbDeleteSource";
            this.cbDeleteSource.Size = new System.Drawing.Size(211, 17);
            this.cbDeleteSource.TabIndex = 19;
            this.cbDeleteSource.Text = "Delete original ISO after completion";
            this.cbDeleteSource.UseVisualStyleBackColor = true;
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
            "Remove all"});
            this.cmbPaddingMode.Location = new System.Drawing.Point(87, 51);
            this.cmbPaddingMode.Name = "cmbPaddingMode";
            this.cmbPaddingMode.Size = new System.Drawing.Size(152, 23);
            this.cmbPaddingMode.TabIndex = 15;
            this.cmbPaddingMode.SelectedIndexChanged += new System.EventHandler(this.cmbPaddingMode_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Padding:";
            // 
            // pbOptions
            // 
            this.pbOptions.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOptions.Location = new System.Drawing.Point(58, 0);
            this.pbOptions.Name = "pbOptions";
            this.pbOptions.Size = new System.Drawing.Size(14, 14);
            this.pbOptions.TabIndex = 25;
            this.pbOptions.TabStop = false;
            // 
            // btnRebuiltBrowse
            // 
            this.btnRebuiltBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRebuiltBrowse.Enabled = false;
            this.btnRebuiltBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRebuiltBrowse.Location = new System.Drawing.Point(386, 50);
            this.btnRebuiltBrowse.Name = "btnRebuiltBrowse";
            this.btnRebuiltBrowse.Size = new System.Drawing.Size(68, 25);
            this.btnRebuiltBrowse.TabIndex = 6;
            this.btnRebuiltBrowse.Text = "&Browse";
            this.btnRebuiltBrowse.UseVisualStyleBackColor = true;
            this.btnRebuiltBrowse.Click += new System.EventHandler(this.btnRebuiltBrowse_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "ISO Path:";
            // 
            // txtRebuiltIso
            // 
            this.txtRebuiltIso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRebuiltIso.Enabled = false;
            this.txtRebuiltIso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRebuiltIso.Location = new System.Drawing.Point(87, 51);
            this.txtRebuiltIso.Name = "txtRebuiltIso";
            this.txtRebuiltIso.ReadOnly = true;
            this.txtRebuiltIso.Size = new System.Drawing.Size(292, 23);
            this.txtRebuiltIso.TabIndex = 5;
            // 
            // ttOptions
            // 
            this.ttOptions.AutoPopDelay = 30000;
            this.ttOptions.InitialDelay = 100;
            this.ttOptions.IsBalloon = true;
            this.ttOptions.ReshowDelay = 100;
            this.ttOptions.ShowAlways = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.pbOutput);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnRebuiltBrowse);
            this.groupBox2.Controls.Add(this.txtDest);
            this.groupBox2.Controls.Add(this.btnDestBrowse);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtRebuiltIso);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(10, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 88);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // pbOutput
            // 
            this.pbOutput.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOutput.Location = new System.Drawing.Point(54, 0);
            this.pbOutput.Name = "pbOutput";
            this.pbOutput.Size = new System.Drawing.Size(14, 14);
            this.pbOutput.TabIndex = 25;
            this.pbOutput.TabStop = false;
            // 
            // ttOutput
            // 
            this.ttOutput.AutoPopDelay = 30000;
            this.ttOutput.InitialDelay = 100;
            this.ttOutput.IsBalloon = true;
            this.ttOutput.ReshowDelay = 100;
            this.ttOutput.ShowAlways = true;
            // 
            // progressBarMulti
            // 
            this.progressBarMulti.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMulti.DisplayStyle = Chilano.Common.ProgressBarDisplayText.Text;
            this.progressBarMulti.Location = new System.Drawing.Point(10, 409);
            this.progressBarMulti.Name = "progressBarMulti";
            this.progressBarMulti.Size = new System.Drawing.Size(302, 23);
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
            this.AcceptButton = this.btnAddIso;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 442);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBarMulti);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnAddIso);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddISO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add ISO Image";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlatform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitleDetails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOptions)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).EndInit();
            this.ResumeLayout(false);

    }

    public AddISO(IsoEntryPlatform Platform)
    {
        InitializeComponent();
        //base.Shown += AddISO_Shown;
        platform = Platform;
        entry.Platform = platform;
        progressBarMulti.Visible = false;
        isoDetails.ProgressChanged += isoDetails_ProgressChanged;
        isoDetails.RunWorkerCompleted += isoDetails_RunWorkerCompleted;
        txtDest.Text = Properties.Settings.Default["OutputPath"].ToString();
        txtRebuiltIso.Text = Properties.Settings.Default["RebuildPath"].ToString();
        cmbLayout.SelectedIndex = (int)Properties.Settings.Default["Layout"];
        cmbFormat.SelectedIndex = (int)Properties.Settings.Default["Format"];
        cmbPaddingMode.SelectedIndex = (int)Properties.Settings.Default["DefaultPadding"];
        cbFtpUpload.Checked = (bool)Properties.Settings.Default["FtpUpload"];
        cbDeleteGod.Enabled = cbFtpUpload.Checked;
        cbDeleteGod.Checked = cbDeleteGod.Enabled && (bool)Properties.Settings.Default["DeleteGod"];
        cbDeleteSource.Checked = (bool)Properties.Settings.Default["DeleteSource"];
        cbDeleteSource.CheckedChanged += new EventHandler(cbDeleteSource_CheckedChanged);
        cbAutoRename.Checked = (bool)Properties.Settings.Default["AutoRenameMultiDisc"];
        ttSource.SetToolTip(pbSource,
            "Select the ISO images to convert to GOD packages.\n\n" +
            "Selecting multiple files will automatically add them to the list,\n" +
            "you will then be able to edit them individually from there");
        ttOutput.SetToolTip(pbOutput,
            "Choose locations to output the GOD packages\n" +
            "and for rebuilt or temporary ISO files.");
        ttTitleDetails.SetToolTip(pbTitleDetails,
            "The details are automatically extracted from the default.xex\n" +
            "file for Xbox 360 images and from default.xbe file for Xbox images.\n" +
            "They can be manually edited if required.\n\n" +
            "Title name is extracted from game list file if available,\n" +
            "all the inputs need to be filled");
        ttThumb.SetToolTip(pbThumb, "Click to set a custom thumbnail for this title.");
        ttOptions.SetToolTip(pbOptions,
            "Format: choose to create GOD and / or ISO files\n\n" +
            "Padding: remove unused padding space to reduce file size\n" +
            "  - Untouched: left untouched, biggest file size.\n" +
            "  - Partial: the ISO image is cropped at the end of the file,\n" +
            "    very quick to do, but it will only save 800-1500MB of space.\n" +
            "  - Remove all: padding is completely removed, file size is the smallest.\n\n" +
            "Transfering GOD packages via FTP requires configuration to be set\n" +
            "in the Settings menu.\n\n" +
            "Auto-renaming multi disc will add the disc number to the GOD\n" +
            "package title name, example: My Game Name - Disc 1");
        txtISO.Focus();
    }

    private void AddISO_Shown(object sender, EventArgs e)
    {
        if (!edit && !drop && (bool)Properties.Settings.Default["AutoBrowse"])
        {
            btnISOBrowse_Click(base.Owner, null);
        }
    }

    public void Drop(string[] files)
    {
        drop = true;
        AddFiles(files);
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
        txtISO.Text = entry.Path;
        txtDest.Text = entry.Destination;
        txtRebuiltIso.Text = ((entry.Options.Padding == IsoEntryPaddingRemoval.Full) ? entry.Options.IsoPath : Properties.Settings.Default["RebuildPath"].ToString());
        cmbFormat.SelectedIndex = (int)entry.Options.Format;
        cmbPaddingMode.SelectedIndex = (int)entry.Options.Padding;
        cmbLayout.SelectedIndex = (int)entry.Options.Layout.ID;
        cbFtpUpload.Checked = (bool)entry.Options.FtpUpload;
        cbDeleteGod.Enabled = cbFtpUpload.Checked;
        cbDeleteGod.Checked = (bool)entry.Options.DeleteGod;
        pbThumb.Image = ((entry.Thumb == null) ? null : Image.FromStream(new MemoryStream(entry.Thumb)));
        cbDeleteSource.Checked = entry.Options.DeleteSource;
        cbDeleteSource.CheckedChanged += new EventHandler(cbDeleteSource_CheckedChanged);
        cbAutoRename.Checked = entry.Options.AddDiscNumber;
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
                //txtName.Text = "Failed to read details from ISO image.";

                processErrors++;

                if (fileList.Length > 1)
                {
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
                        progressBarMulti.Text = progressBarMulti.Value + " files processed, (" + processErrors + " failed)";
                    }
                }
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

            string csvInfo = getCsvTitle(isoDetailsResults.TitleID, isoDetailsResults.MediaID);
            if(csvInfo != "")
            {
                txtName.Text = csvInfo;
            }
            else
            {
                txtName.Text = isoDetailsResults.Name;
            }

            if (txtName.Text.Trim().Length == 0)
            {
                txtName.Text = "Undefined";
                message = "The name of the game was not automatically detected";
            }

            int num = 0;
            txtTitleID.Text = isoDetailsResults.TitleID;
            txtMediaID.Text = isoDetailsResults.MediaID;
            txtPlatform.Text = isoDetailsResults.Platform;
            txtExType.Text = isoDetailsResults.ExType;
            Int32.TryParse(isoDetailsResults.DiscNumber, out num);
            txtDiscNum.Text = (num < 1 ? "1" : num.ToString());
            Int32.TryParse(isoDetailsResults.DiscCount, out num);
            txtDiscCount.Text = (num < 1 ? "1" : num.ToString());
            if (isoDetailsResults.Thumbnail != null && isoDetailsResults.RawThumbnail != null)
            {
                pbThumb.Image = (Image)isoDetailsResults.Thumbnail.Clone();
                pbThumb.Tag = (byte[])isoDetailsResults.RawThumbnail.Clone();
                //MessageBox.Show(txtRebuiltIso.Text + txtTitleID.Text + ".bmp");
                //pbThumb.Image.Save(txtRebuiltIso.Text + txtTitleID.Text + ".bmp");
            }

            UpdateLayout();

            if (fileList.Length > 1)
            {
                IsoEntryOptions isoEntryOptions = new IsoEntryOptions();
                isoEntryOptions.Format = (IsoEntryFormat)cmbFormat.SelectedIndex;
                isoEntryOptions.Padding = (IsoEntryPaddingRemoval)cmbPaddingMode.SelectedIndex;
                isoEntryOptions.DeleteSource = cbDeleteSource.Checked;
                string titlename = Utils.sanitizePath(txtName.Text);
                string gameDirectory = txtTitleID.Text;
                if (cmbLayout.SelectedIndex > (int)IsoEntryGameDirectoryLayoutID.MediaID && titlename.Length != 0)
                {
                    switch (cmbLayout.SelectedIndex)
                    {
                        case (int)IsoEntryGameDirectoryLayoutID.TitleName_Slash_MediaID:
                            gameDirectory = titlename + Path.DirectorySeparatorChar + txtTitleID.Text;
                            break;
                        case (int)IsoEntryGameDirectoryLayoutID.TitleName_Space_MediaID:
                            gameDirectory = titlename + " " + txtTitleID.Text;
                            break;
                        case (int)IsoEntryGameDirectoryLayoutID.TitleName:
                            gameDirectory = titlename;
                            break;
                    }
                }
                IsoEntryGameDirectoryLayout isoEntryLayout = new IsoEntryGameDirectoryLayout();
                isoEntryLayout.ID = (IsoEntryGameDirectoryLayoutID)cmbLayout.SelectedIndex;
                isoEntryLayout.Path = gameDirectory;
                isoEntryOptions.Layout = isoEntryLayout;
                isoEntryOptions.FtpUpload = cbFtpUpload.Checked;
                isoEntryOptions.DeleteGod = cbDeleteGod.Checked;
                isoEntryOptions.TempPath = Path.GetTempPath();
                isoEntryOptions.IsoPath = txtRebuiltIso.Text;
                if (!isoEntryOptions.TempPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    isoEntryOptions.TempPath += Path.DirectorySeparatorChar;
                }
                if (!isoEntryOptions.IsoPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    isoEntryOptions.IsoPath += Path.DirectorySeparatorChar;
                }

                IsoEntryID iD = new IsoEntryID(txtTitleID.Text, txtMediaID.Text, byte.Parse(txtDiscNum.Text), byte.Parse(txtDiscCount.Text), byte.Parse(txtPlatform.Text), byte.Parse(txtExType.Text));
                FileInfo fileInfo = new FileInfo(txtISO.Text);
                IsoEntry isoEntry = new IsoEntry(platform, txtISO.Text, txtDest.Text, fileInfo.Length, txtName.Text, iD, (byte[])pbThumb.Tag, isoEntryOptions, message);
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
                    progressBarMulti.Text = progressBarMulti.Value + " files processed" + (processErrors > 0 ? ", (" + processErrors + " failed)" : "");
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
                //MessageBox.Show(isoDetailsResults.ErrorMessage, "Error Reading Title Information");
                txtName.Text = isoDetailsResults.ErrorMessage;
            }
            else
            {
                txtName.Text = isoDetailsResults.ProgressMessage;
            }
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if(isoDetails.IsBusy)
        {
            //isoDetails.CancelAsync();
            isoDetails.Dispose();
        }
        if(!edit && SettingsChanged())
        {
            DialogResult result = MessageBox.Show("Do you want to save settings ?", "Settings changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveSettings();
            }
        }
        Close();
    }

    private bool checkPaths()
    {
        if (txtISO.Text.Length == 0 || txtDest.Text.Length == 0 || (cmbPaddingMode.SelectedIndex > 1 && txtRebuiltIso.Text.Length == 0))
        {
            string message = "";
            string sep = "";
            string messageStart = "Please select";
            string messageISO = "An ISO image to convert";
            string messageGOD = "A destination folder to store the GOD container in";
            string messageRebuild = "A destination folder to store the " + (cmbFormat.SelectedIndex == 0 ? "temporary " : "") + "rebuilt ISO image in";
            int errISO = 0, errGOD = 0, errRebuild = 0;

            if (txtISO.Text.Length == 0) errISO = 1;
            if (txtDest.Text.Length == 0) errGOD = 1;
            if (cmbPaddingMode.SelectedIndex > 1 && txtRebuiltIso.Text.Length == 0) errRebuild = 1;

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

        if (txtName.Text.Length == 0 || txtTitleID.Text.Length != 8 || !IsHexString(txtTitleID.Text) || txtMediaID.Text.Length != 8 || !IsHexString(txtMediaID.Text) || txtDiscNum.Text.Length == 0 || txtDiscNum.Text == "0" || txtPlatform.Text.Length == 0 || txtExType.Text.Length == 0 || (cmbLayout.SelectedIndex > 0 && Utils.sanitizePath(txtName.Text).Trim() == ""))
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

            if (txtName.Text.Trim().Length == 0) errName = 1;
            if (txtTitleID.Text.Length != 8 || !IsHexString(txtTitleID.Text)) errTitle = 1;
            if (txtMediaID.Text.Length != 8 || !IsHexString(txtMediaID.Text)) errMedia = 1;
            if (txtDiscNum.Text.Length == 0 || txtDiscNum.Text == "0") errDisc = 1;
            if (txtPlatform.Text.Length == 0) errPlatform = 1;
            if (txtExType.Text.Length == 0) errEx = 1;
            if (cmbLayout.SelectedIndex > 0 && Utils.sanitizePath(txtName.Text).Trim() == "")
            {
                messageName += " and is a valid directory name";
                errName = 1;
            }
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

    private void btnAddIso_Click(object sender, EventArgs e)
    {
        if (checkFields())
        {
            SaveSettings();
            IsoEntryOptions isoEntryOptions = new IsoEntryOptions();
            isoEntryOptions.Format = (IsoEntryFormat)cmbFormat.SelectedIndex;
            isoEntryOptions.Padding = (IsoEntryPaddingRemoval)cmbPaddingMode.SelectedIndex;
            isoEntryOptions.DeleteSource = cbDeleteSource.Checked;
            isoEntryOptions.AddDiscNumber = cbAutoRename.Checked;
            string titlename = Utils.sanitizePath(txtName.Text);
            string gameDirectory = txtTitleID.Text;
            if (cmbLayout.SelectedIndex > (int)IsoEntryGameDirectoryLayoutID.MediaID && titlename.Length != 0)
            {
                switch (cmbLayout.SelectedIndex)
                {
                    case (int)IsoEntryGameDirectoryLayoutID.TitleName_Slash_MediaID:
                        gameDirectory = titlename + Path.DirectorySeparatorChar + txtTitleID.Text;
                        break;
                    case (int)IsoEntryGameDirectoryLayoutID.TitleName_Space_MediaID:
                        gameDirectory = titlename + " " + txtTitleID.Text;
                        break;
                    case (int)IsoEntryGameDirectoryLayoutID.TitleName:
                        gameDirectory = titlename;
                        break;
                }
            }
            IsoEntryGameDirectoryLayout isoEntryLayout = new IsoEntryGameDirectoryLayout();
            isoEntryLayout.ID = (IsoEntryGameDirectoryLayoutID)cmbLayout.SelectedIndex;
            isoEntryLayout.Path = gameDirectory;
            isoEntryOptions.Layout = isoEntryLayout;
            isoEntryOptions.FtpUpload = cbFtpUpload.Checked;
            isoEntryOptions.DeleteGod = cbDeleteGod.Checked;
            isoEntryOptions.TempPath = Path.GetTempPath();
            isoEntryOptions.IsoPath = txtRebuiltIso.Text;
            if (!isoEntryOptions.TempPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                isoEntryOptions.TempPath += Path.DirectorySeparatorChar;
            }
            if (!isoEntryOptions.IsoPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                isoEntryOptions.IsoPath += Path.DirectorySeparatorChar;
            }
            IsoEntryID iD = new IsoEntryID(txtTitleID.Text, txtMediaID.Text, byte.Parse(txtDiscNum.Text), byte.Parse(txtDiscCount.Text), byte.Parse(txtPlatform.Text), byte.Parse(txtExType.Text));
            FileInfo fileInfo = new FileInfo(txtISO.Text);
            IsoEntry isoEntry = new IsoEntry(platform, txtISO.Text, txtDest.Text, fileInfo.Length, txtName.Text, iD, (byte[])pbThumb.Tag, isoEntryOptions, "");
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
            AddFiles(openFileDialog.FileNames);
        }
    }

    private void AddFiles(string[] files)
    {
        file = 0;
        processErrors = 0;
        fileList = files;
        txtISO.Text = files[0];
        btnCancel.Text = "Cancel";
        clearXexFields();
        if (fileList.Length > 1)
        {
            if (!checkPaths())
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
        }
        else
        {
            btnAddIso.Enabled = true;
        }
        /*
        Debug.WriteLine("txtISO: " + txtISO.Text);
        Debug.WriteLine("pathTemp: " + pathTemp);
        Debug.WriteLine("pathXT: " + pathXT);
        */
        isoDetails.RunWorkerAsync(new IsoDetailsArgs(txtISO.Text, (base.Owner as Main).pathTemp, (base.Owner as Main).pathXT));
        txtName.Text = "Reading default.xex/xbe...";
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

    private void cmbPaddingMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbPaddingMode.SelectedIndex < 2)
        {
            txtRebuiltIso.Enabled = false;
            btnRebuiltBrowse.Enabled = false;
        }
        else
        {
            txtRebuiltIso.Enabled = true;
            btnRebuiltBrowse.Enabled = true;
        }
    }

    private void btnRebuiltBrowse_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
        folderBrowserDialog.SelectedPath = txtRebuiltIso.Text;
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
        cmbFormat.Enabled = status;
        cbAutoRename.Enabled = status;
        if (cmbPaddingMode.SelectedIndex < 2)
        {
            txtRebuiltIso.Enabled = false;
            btnRebuiltBrowse.Enabled = false;
        }
        else
        {
            txtRebuiltIso.Enabled = status;
            btnRebuiltBrowse.Enabled = status;
        }
        btnDestBrowse.Enabled = status;
        btnISOBrowse.Enabled = status;
    }

    private void ScrollHandlerFunction(object sender, MouseEventArgs e)
    {
        ((HandledMouseEventArgs)e).Handled = true;
        var self = (NumericUpDown)sender;
        self.Value = Math.Max(Math.Min(self.Value + ((e.Delta > 0) ? self.Increment : -self.Increment), self.Maximum), self.Minimum);
    }

    private void txtDiscNum_ValueChanged(object sender, EventArgs e)
    {
        if (txtDiscCount.Value < txtDiscNum.Value) txtDiscCount.Value = txtDiscNum.Value;
    }

    private void txtDiscCount_ValueChanged(object sender, EventArgs e)
    {
        if (txtDiscNum.Value > txtDiscCount.Value) txtDiscNum.Value = txtDiscCount.Value;
    }

    public static bool IsHexString(string input)
    {
        return Regex.IsMatch(input, @"\A\b[0-9a-fA-F]+\b\Z");
    }

    public string getCsvTitle(string TitleID, string MediaID)
    {
        var file = (platform == IsoEntryPlatform.Xbox ? Main.file_listxbox : Main.file_listxbox360);
        var title = "";

        if (File.Exists(file))
        {
            try
            {
                var data = new List<string[]>();
                char delimiter = ',';
                Dictionary<string, int> columnIndices = null;

                using (var reader = new StreamReader(file))
                {
                    // Read the first line of the file
                    var firstLine = reader.ReadLine();

                    // Determine the delimiter to use based on the first line
                    if (firstLine.Contains("\t"))
                    {
                        delimiter = '\t';
                    }

                    // Create the regular expression pattern
                    string pattern = $"{delimiter}(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";

                    // Read the header row
                    var headers = Regex.Split(firstLine, pattern);

                    // Create a dictionary to map column names to indices
                    columnIndices = headers.Select((header, index) => new { header, index }).ToDictionary(x => x.header, x => x.index);

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = Regex.Split(line, pattern);

                        data.Add(values);
                    }
                }

                // Now you can search for values in the data array
                foreach (var row in data)
                {
                    string title_id = row[columnIndices["title_id"]];
                    string media_id = (platform == IsoEntryPlatform.Xbox ? row[columnIndices["xbe_md5"]].Substring(0, 8).ToUpper() : row[columnIndices["media_id"]]);

                    if (TitleID == title_id)
                    {
                        title = row[columnIndices["title_name"]];
                        if (MediaID == media_id)
                        {
                            return row[columnIndices["title_name"]];
                        }
                    }
                }
            }
            catch
            {
                // will return empty title
            }
        }
        return title;
    }

    private void cbDeleteSource_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;
        if (checkbox.Checked)
        {
            DialogResult result = MessageBox.Show("This will permanently delete your original ISO files after completion, are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Cancel)
            {
                checkbox.Checked = false;
                return;
            }
        }
    }

    private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch(cmbFormat.SelectedIndex)
        {
            case (int)IsoEntryFormat.God:
                cmbPaddingMode.Enabled = true;
                cbFtpUpload.Enabled = true;
                cmbLayout.Enabled = true;
                cmbLayout.SelectedIndex = lastLayout;
                break;
            case (int)IsoEntryFormat.GodIso:
                cmbPaddingMode.SelectedIndex = 2;
                cmbPaddingMode.Enabled = false;
                cbFtpUpload.Enabled = true;
                cmbLayout.Enabled = true;
                cmbLayout.SelectedIndex = lastLayout;
                break;
            case (int)IsoEntryFormat.Iso:
                cmbPaddingMode.SelectedIndex = 2;
                cmbPaddingMode.Enabled = false;
                cbFtpUpload.Checked = false;
                cbFtpUpload.Enabled = false;
                cmbLayout.Enabled = false;
                cmbLayout.SelectedIndex = -1;
                break;
        }
    }

    private void cbFTP_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;
        if (checkbox.Checked)
        {
            cbDeleteGod.Enabled = true;
        }
        else
        {
            cbDeleteGod.Checked = false;
            cbDeleteGod.Enabled = false;
        }
    }

    private void UpdateLayout()
    {
        string layout = "";
        string name = Utils.sanitizePath(txtName.Text).Length > 0 ? Utils.sanitizePath(txtName.Text) : "";
        string titleid = txtTitleID.Text.Length == 8 && IsHexString(txtTitleID.Text) ? txtTitleID.Text : "";

        if (cmbLayout.SelectedIndex >= 0)
        {
            switch (cmbLayout.SelectedIndex)
            {
                case (int)IsoEntryGameDirectoryLayoutID.MediaID:
                    layout = titleid != "" ? titleid : "";
                    break;
                case (int)IsoEntryGameDirectoryLayoutID.TitleName_Slash_MediaID:
                    layout = name != "" && titleid != "" ? name + Path.DirectorySeparatorChar + titleid : "";
                    break;
                case (int)IsoEntryGameDirectoryLayoutID.TitleName_Space_MediaID:
                    layout = name != "" && titleid != "" ? name + " " + titleid : "";
                    break;
                case (int)IsoEntryGameDirectoryLayoutID.TitleName:
                    layout = name != "" ? name : "";
                    break;
            }
            layout = layout != "" ? Path.DirectorySeparatorChar + layout + Path.DirectorySeparatorChar : "";
        }
        ttGodLayout.SetToolTip(cmbLayout, layout);
    }

    private void txtName_TextChanged(object sender, EventArgs e)
    {
        UpdateLayout();
    }

    private void txtTitleID_TextChanged(object sender, EventArgs e)
    {
        UpdateLayout();
    }

    private bool SettingsChanged()
    {
        if ((string)Properties.Settings.Default["OutputPath"] != txtDest.Text ||
            (string)Properties.Settings.Default["RebuildPath"] != txtRebuiltIso.Text ||
            (int)Properties.Settings.Default["Format"] != cmbFormat.SelectedIndex ||
            (int)Properties.Settings.Default["DefaultPadding"] != cmbPaddingMode.SelectedIndex ||
            (int)Properties.Settings.Default["Layout"] != lastLayout ||
            (bool)Properties.Settings.Default["FtpUpload"] != cbFtpUpload.Checked ||
            (bool)Properties.Settings.Default["DeleteGod"] != cbDeleteGod.Checked ||
            (bool)Properties.Settings.Default["DeleteSource"] != cbDeleteSource.Checked ||
            (bool)Properties.Settings.Default["AutoRenameMultiDisc"] != cbAutoRename.Checked)
        {
            return true;
        }
        return false;
    }

    private void SaveSettings()
    {
        //MessageBox.Show("saved");
        if (txtDest.Text.Length != 0 && !txtDest.Text.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            txtDest.Text += Path.DirectorySeparatorChar;
        }
        if (txtRebuiltIso.Text.Length != 0 && !txtRebuiltIso.Text.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            txtRebuiltIso.Text += Path.DirectorySeparatorChar;
        }
        Properties.Settings.Default["OutputPath"] = txtDest.Text;
        Properties.Settings.Default["RebuildPath"] = txtRebuiltIso.Text;
        Properties.Settings.Default["Format"] = cmbFormat.SelectedIndex;
        Properties.Settings.Default["DefaultPadding"] = cmbPaddingMode.SelectedIndex;
        Properties.Settings.Default["Layout"] = lastLayout;
        Properties.Settings.Default["FtpUpload"] = cbFtpUpload.Checked;
        Properties.Settings.Default["DeleteGod"] = cbDeleteGod.Checked;
        Properties.Settings.Default["DeleteSource"] = cbDeleteSource.Checked;
        Properties.Settings.Default["AutoRenameMultiDisc"] = cbAutoRename.Checked;
        Properties.Settings.Default.Save();
    }

    private void cmbFolderLayout_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbLayout.SelectedIndex >= 0)
        {
            UpdateLayout();
            lastLayout = cmbLayout.SelectedIndex;
        }
    }
}
