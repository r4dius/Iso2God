using Chilano.Iso2God.Ftp;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Chilano.Iso2God;

public class Settings : Form
{
    private IContainer components;
    private Button btnSave;
    private Button btnCancel;
    private ToolTip ttSettings;
    private TextBox txtFtpUser;
    private Label label5;
    private PictureBox pbFTP;
    private TextBox txtFtpIp;
    private Label label4;
    private TextBox txtFtpPass;
    private Label label6;
    private ToolTip ttFTP;
    private ToolTip ttOptions;
    private TextBox txtFtpPort;
    private Button btnTest;
    private Label label7;
    private TextBox txtFtpPath;
    private CheckBox cbAutoBrowse;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private GroupBox groupBox4;
    private ComboBox cmbFtpPath;
    private RadioButton radCustom;
    private RadioButton radDefault;
    private Button btnRebuiltBrowse;
    private TextBox txtRebuiltIso;
    private Label label2;
    private Button btnOutBrowse;
    private Label label1;
    private TextBox txtDest;
    private GroupBox groupBox1;
    private PictureBox pbOutput;
    private FtpTester ftp = new FtpTester();


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
            this.ttFTP = new System.Windows.Forms.ToolTip(this.components);
            this.ttOptions = new System.Windows.Forms.ToolTip(this.components);
            this.ttSettings = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radCustom = new System.Windows.Forms.RadioButton();
            this.radDefault = new System.Windows.Forms.RadioButton();
            this.cmbFtpPath = new System.Windows.Forms.ComboBox();
            this.txtFtpPath = new System.Windows.Forms.TextBox();
            this.txtFtpPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFtpPass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFtpUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pbFTP = new System.Windows.Forms.PictureBox();
            this.txtFtpIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.cbAutoBrowse = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRebuiltBrowse = new System.Windows.Forms.Button();
            this.txtRebuiltIso = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOutBrowse = new System.Windows.Forms.Button();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbOutput = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFTP)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // ttFTP
            // 
            this.ttFTP.AutoPopDelay = 30000;
            this.ttFTP.InitialDelay = 100;
            this.ttFTP.IsBalloon = true;
            this.ttFTP.ReshowDelay = 100;
            this.ttFTP.ShowAlways = true;
            // 
            // ttOptions
            // 
            this.ttOptions.AutoPopDelay = 30000;
            this.ttOptions.InitialDelay = 100;
            this.ttOptions.IsBalloon = true;
            this.ttOptions.ReshowDelay = 100;
            this.ttOptions.ShowAlways = true;
            // 
            // ttSettings
            // 
            this.ttSettings.AutoPopDelay = 30000;
            this.ttSettings.InitialDelay = 100;
            this.ttSettings.IsBalloon = true;
            this.ttSettings.ReshowDelay = 100;
            this.ttSettings.ShowAlways = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(329, 347);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(410, 347);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.txtFtpPort);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtFtpPass);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtFtpUser);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.pbFTP);
            this.groupBox3.Controls.Add(this.txtFtpIp);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(10, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(474, 186);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FTP Server";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.radCustom);
            this.groupBox4.Controls.Add(this.radDefault);
            this.groupBox4.Controls.Add(this.cmbFtpPath);
            this.groupBox4.Controls.Add(this.txtFtpPath);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(11, 87);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(452, 88);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Path";
            // 
            // radCustom
            // 
            this.radCustom.AutoSize = true;
            this.radCustom.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCustom.Location = new System.Drawing.Point(136, 23);
            this.radCustom.Name = "radCustom";
            this.radCustom.Size = new System.Drawing.Size(64, 17);
            this.radCustom.TabIndex = 1;
            this.radCustom.TabStop = true;
            this.radCustom.Text = "Custom";
            this.radCustom.UseVisualStyleBackColor = true;
            this.radCustom.CheckedChanged += new System.EventHandler(this.radCustom_CheckedChanged);
            // 
            // radDefault
            // 
            this.radDefault.AutoSize = true;
            this.radDefault.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDefault.Location = new System.Drawing.Point(11, 23);
            this.radDefault.Name = "radDefault";
            this.radDefault.Size = new System.Drawing.Size(113, 17);
            this.radDefault.TabIndex = 0;
            this.radDefault.TabStop = true;
            this.radDefault.Text = "Default locations";
            this.radDefault.UseVisualStyleBackColor = true;
            this.radDefault.CheckedChanged += new System.EventHandler(this.radDefault_CheckedChanged);
            // 
            // cmbFtpPath
            // 
            this.cmbFtpPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFtpPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFtpPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFtpPath.FormattingEnabled = true;
            this.cmbFtpPath.Items.AddRange(new object[] {
            "Hard Drive (Hdd1/Content/0000000000000000)",
            "USB #1 (Usb0)",
            "USB #2 (Usb1)"});
            this.cmbFtpPath.Location = new System.Drawing.Point(11, 51);
            this.cmbFtpPath.Name = "cmbFtpPath";
            this.cmbFtpPath.Size = new System.Drawing.Size(430, 23);
            this.cmbFtpPath.TabIndex = 2;
            // 
            // txtFtpPath
            // 
            this.txtFtpPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPath.Location = new System.Drawing.Point(11, 51);
            this.txtFtpPath.Name = "txtFtpPath";
            this.txtFtpPath.Size = new System.Drawing.Size(430, 23);
            this.txtFtpPath.TabIndex = 3;
            this.txtFtpPath.TextChanged += new System.EventHandler(this.txtFtpPath_TextChanged);
            // 
            // txtFtpPort
            // 
            this.txtFtpPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPort.Location = new System.Drawing.Point(84, 51);
            this.txtFtpPort.Name = "txtFtpPort";
            this.txtFtpPort.Size = new System.Drawing.Size(145, 23);
            this.txtFtpPort.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Port:";
            // 
            // txtFtpPass
            // 
            this.txtFtpPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPass.Location = new System.Drawing.Point(318, 51);
            this.txtFtpPass.Name = "txtFtpPass";
            this.txtFtpPass.Size = new System.Drawing.Size(145, 23);
            this.txtFtpPass.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(239, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Password:";
            // 
            // txtFtpUser
            // 
            this.txtFtpUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpUser.Location = new System.Drawing.Point(318, 20);
            this.txtFtpUser.Name = "txtFtpUser";
            this.txtFtpUser.Size = new System.Drawing.Size(145, 23);
            this.txtFtpUser.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(239, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Username:";
            // 
            // pbFTP
            // 
            this.pbFTP.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbFTP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbFTP.Location = new System.Drawing.Point(71, 0);
            this.pbFTP.Name = "pbFTP";
            this.pbFTP.Size = new System.Drawing.Size(14, 14);
            this.pbFTP.TabIndex = 26;
            this.pbFTP.TabStop = false;
            // 
            // txtFtpIp
            // 
            this.txtFtpIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpIp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpIp.Location = new System.Drawing.Point(84, 20);
            this.txtFtpIp.Name = "txtFtpIp";
            this.txtFtpIp.Size = new System.Drawing.Size(145, 23);
            this.txtFtpIp.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "IP Address:";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(248, 347);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 25);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test FTP";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // cbAutoBrowse
            // 
            this.cbAutoBrowse.AutoSize = true;
            this.cbAutoBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoBrowse.Location = new System.Drawing.Point(10, 22);
            this.cbAutoBrowse.Name = "cbAutoBrowse";
            this.cbAutoBrowse.Size = new System.Drawing.Size(186, 17);
            this.cbAutoBrowse.TabIndex = 0;
            this.cbAutoBrowse.Text = "Auto-browse when adding ISO";
            this.cbAutoBrowse.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbAutoBrowse);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(10, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 52);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Various";
            // 
            // btnRebuiltBrowse
            // 
            this.btnRebuiltBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRebuiltBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRebuiltBrowse.Location = new System.Drawing.Point(396, 50);
            this.btnRebuiltBrowse.Name = "btnRebuiltBrowse";
            this.btnRebuiltBrowse.Size = new System.Drawing.Size(68, 25);
            this.btnRebuiltBrowse.TabIndex = 3;
            this.btnRebuiltBrowse.Text = "&Browse";
            this.btnRebuiltBrowse.UseVisualStyleBackColor = true;
            this.btnRebuiltBrowse.Click += new System.EventHandler(this.btnRebuiltBrowse_Click);
            // 
            // txtRebuiltIso
            // 
            this.txtRebuiltIso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRebuiltIso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRebuiltIso.Location = new System.Drawing.Point(84, 51);
            this.txtRebuiltIso.Name = "txtRebuiltIso";
            this.txtRebuiltIso.ReadOnly = true;
            this.txtRebuiltIso.Size = new System.Drawing.Size(305, 23);
            this.txtRebuiltIso.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "ISO Path:";
            // 
            // btnOutBrowse
            // 
            this.btnOutBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutBrowse.Location = new System.Drawing.Point(396, 19);
            this.btnOutBrowse.Name = "btnOutBrowse";
            this.btnOutBrowse.Size = new System.Drawing.Size(68, 25);
            this.btnOutBrowse.TabIndex = 1;
            this.btnOutBrowse.Text = "&Browse";
            this.btnOutBrowse.UseVisualStyleBackColor = true;
            this.btnOutBrowse.Click += new System.EventHandler(this.btnDestBrowse_Click);
            // 
            // txtDest
            // 
            this.txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDest.Location = new System.Drawing.Point(84, 20);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
            this.txtDest.Size = new System.Drawing.Size(305, 23);
            this.txtDest.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GOD Path:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.pbOutput);
            this.groupBox1.Controls.Add(this.btnRebuiltBrowse);
            this.groupBox1.Controls.Add(this.txtRebuiltIso);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnOutBrowse);
            this.groupBox1.Controls.Add(this.txtDest);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(474, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output Locations";
            // 
            // pbOutput
            // 
            this.pbOutput.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOutput.Location = new System.Drawing.Point(107, 0);
            this.pbOutput.Name = "pbOutput";
            this.pbOutput.Size = new System.Drawing.Size(14, 14);
            this.pbOutput.TabIndex = 27;
            this.pbOutput.TabStop = false;
            // 
            // Settings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(494, 381);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFTP)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).EndInit();
            this.ResumeLayout(false);

    }

    public Settings()
    {
        InitializeComponent();
        loadSettings();
        ttSettings.SetToolTip(pbOutput,
            "GOD Path allows you to set a default path for all GODs to be stored in.\n" +
            "Each GOD will be written into sub-directories named using the selected GOD Layout.\n\n" +
            "ISO Path allows you to specify a default path to store rebuilt ISO images.");
        ttFTP.SetToolTip(pbFTP, 
            "Once an ISO image has been converted to a GOD package,\n" +
            "it can be automatically uploaded to your Xbox 360 using FTP,\n" +
            "you'll need to have an FTP server running on it.");
    }

    private void loadSettings()
    {
        try
        {
            txtDest.Text = Properties.Settings.Default["OutputPath"].ToString();
            txtRebuiltIso.Text = Properties.Settings.Default["RebuildPath"].ToString();
            cbAutoBrowse.Checked = (bool)Properties.Settings.Default["AutoBrowse"];
            txtFtpIp.Text = Properties.Settings.Default["FtpIP"].ToString();
            txtFtpUser.Text = Properties.Settings.Default["FtpUser"].ToString();
            txtFtpPass.Text = Properties.Settings.Default["FtpPass"].ToString();
            txtFtpPort.Text = Properties.Settings.Default["FtpPort"].ToString();
            txtFtpPath.Text = Properties.Settings.Default["FtpPathCustom"].ToString();
            cmbFtpPath.SelectedIndex = Properties.Settings.Default.FtpPathDefaults;
            if (Properties.Settings.Default.FtpPathType == 0) radDefault.Checked = true;
            else radCustom.Checked = true;
        }
        catch
        {
            MessageBox.Show("Unable to load User Settings.\n\nIf this is the first time you've seen this message, the problem will be resolved when you save your settings.\n\nIf you've seen it before, please contact the author about this issue.");
        }
    }

    private void ftp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (ftp.Errors.Count == 0)
        {
            MessageBox.Show("Connection test was successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
            if (e.Cancelled)
            {
                return;
            }
            btnTest.Text = "Test";
            foreach (Exception error in ftp.Errors)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        btnTest.Enabled = true;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
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
        Properties.Settings.Default["AutoBrowse"] = cbAutoBrowse.Checked;
        Properties.Settings.Default["FtpIP"] = txtFtpIp.Text;
        Properties.Settings.Default["FtpUser"] = txtFtpUser.Text;
        Properties.Settings.Default["FtpPass"] = txtFtpPass.Text;
        Properties.Settings.Default["FtpPort"] = txtFtpPort.Text;
        Properties.Settings.Default["FtpPathCustom"] = txtFtpPath.Text;
        Properties.Settings.Default.FtpPathDefaults = cmbFtpPath.SelectedIndex;
        Properties.Settings.Default.FtpPathType = radDefault.Checked ? 0 : 1;
        Properties.Settings.Default.Save();
        if (base.Owner.Name != "AddISO")
        {
            (base.Owner as Main).UpdateSpace();
        }
        Close();
    }

    private void btnTest_Click(object sender, EventArgs e)
    {
        string ip = txtFtpIp.Text;
        if (ip.Trim().Length == 0)
        {
            MessageBox.Show("Please enter a valid IP address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        string user = txtFtpUser.Text;
        string pass = txtFtpPass.Text;
        string port = txtFtpPort.Text;
        ftp = new FtpTester();
        ftp.WorkerSupportsCancellation = true;
        ftp.RunWorkerCompleted += ftp_RunWorkerCompleted;
        ftp.RunWorkerAsync(new FtpTesterArgs(ip, user, pass, port));
        btnTest.Text = "Testing...";
        btnTest.Enabled = false;
    }

    private void Settings_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (ftp.IsBusy)
        {
            ftp.CancelAsync();
            ftp.Dispose();
        }
    }

    private void radDefault_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rb = (RadioButton)sender;

        if (rb.Checked)
        {
            cmbFtpPath.Visible = true;
            txtFtpPath.Visible = false;
        }
    }

    private void radCustom_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rb = (RadioButton)sender;

        if (rb.Checked)
        {
            txtFtpPath.Visible = true;
            cmbFtpPath.Visible = false;
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

    private void txtFtpPath_TextChanged(object sender, EventArgs e)
    {

    }
}
