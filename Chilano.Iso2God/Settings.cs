using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Chilano.Iso2God;

public class Settings : Form
{
    private IContainer components;

    private GroupBox groupBox1;

    private TextBox txtOut;

    private Label label1;

    private Button btnOutBrowse;

    private Button btnSave;

    private Button btnCancel;

    private PictureBox pbRipping;

    private ToolTip ttSettings;

    private Button btnRebuild;

    private TextBox txtRebuild;

    private Label label2;

    private GroupBox groupBox2;

    private PictureBox pbOptions;

    private CheckBox cbDeleteSource;

    private CheckBox cbDeleteRebuilt;

    private CheckBox cbAutoRename;

    private GroupBox groupBox3;

    private TextBox txtFtpUser;

    private Label label5;

    private PictureBox pbFTP;

    private TextBox txtFtpIp;

    private Label label4;

    private TextBox txtFtpPass;

    private Label label6;

    private CheckBox cbFTP;

    private ToolTip ttFTP;

    private ToolTip ttOptions;

    private Label label3;

    private ComboBox cmbPadding;

    private TextBox txtFtpPort;

    private Label label7;
    private CheckBox cbTitleDirectory;
    private CheckBox cbAutoBrowse;


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
            this.btnRebuild = new System.Windows.Forms.Button();
            this.txtRebuild = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbRipping = new System.Windows.Forms.PictureBox();
            this.btnOutBrowse = new System.Windows.Forms.Button();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ttFTP = new System.Windows.Forms.ToolTip(this.components);
            this.ttOptions = new System.Windows.Forms.ToolTip(this.components);
            this.ttSettings = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbTitleDirectory = new System.Windows.Forms.CheckBox();
            this.cbAutoBrowse = new System.Windows.Forms.CheckBox();
            this.cmbPadding = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAutoRename = new System.Windows.Forms.CheckBox();
            this.cbDeleteRebuilt = new System.Windows.Forms.CheckBox();
            this.cbDeleteSource = new System.Windows.Forms.CheckBox();
            this.pbOptions = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtFtpPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbFTP = new System.Windows.Forms.CheckBox();
            this.txtFtpPass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFtpUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pbFTP = new System.Windows.Forms.PictureBox();
            this.txtFtpIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRipping)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOptions)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFTP)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btnRebuild);
            this.groupBox1.Controls.Add(this.txtRebuild);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pbRipping);
            this.groupBox1.Controls.Add(this.btnOutBrowse);
            this.groupBox1.Controls.Add(this.txtOut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Location Settings";
            // 
            // btnRebuild
            // 
            this.btnRebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRebuild.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRebuild.Location = new System.Drawing.Point(384, 50);
            this.btnRebuild.Name = "btnRebuild";
            this.btnRebuild.Size = new System.Drawing.Size(70, 25);
            this.btnRebuild.TabIndex = 4;
            this.btnRebuild.Text = "&Browse";
            this.btnRebuild.UseVisualStyleBackColor = true;
            this.btnRebuild.Click += new System.EventHandler(this.btnRebuild_Click);
            // 
            // txtRebuild
            // 
            this.txtRebuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRebuild.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRebuild.Location = new System.Drawing.Point(86, 51);
            this.txtRebuild.Name = "txtRebuild";
            this.txtRebuild.ReadOnly = true;
            this.txtRebuild.Size = new System.Drawing.Size(291, 23);
            this.txtRebuild.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Rebuild Path:";
            // 
            // pbRipping
            // 
            this.pbRipping.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbRipping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbRipping.Location = new System.Drawing.Point(107, 0);
            this.pbRipping.Name = "pbRipping";
            this.pbRipping.Size = new System.Drawing.Size(14, 14);
            this.pbRipping.TabIndex = 26;
            this.pbRipping.TabStop = false;
            // 
            // btnOutBrowse
            // 
            this.btnOutBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutBrowse.Location = new System.Drawing.Point(384, 19);
            this.btnOutBrowse.Name = "btnOutBrowse";
            this.btnOutBrowse.Size = new System.Drawing.Size(70, 25);
            this.btnOutBrowse.TabIndex = 2;
            this.btnOutBrowse.Text = "&Browse";
            this.btnOutBrowse.UseVisualStyleBackColor = true;
            this.btnOutBrowse.Click += new System.EventHandler(this.btnDestBrowse_Click);
            // 
            // txtOut
            // 
            this.txtOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOut.Location = new System.Drawing.Point(86, 20);
            this.txtOut.Name = "txtOut";
            this.txtOut.ReadOnly = true;
            this.txtOut.Size = new System.Drawing.Size(291, 23);
            this.txtOut.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output Path:";
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
            this.btnSave.Location = new System.Drawing.Point(319, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(400, 255);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.cbTitleDirectory);
            this.groupBox2.Controls.Add(this.cbAutoBrowse);
            this.groupBox2.Controls.Add(this.cmbPadding);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbAutoRename);
            this.groupBox2.Controls.Add(this.cbDeleteRebuilt);
            this.groupBox2.Controls.Add(this.cbDeleteSource);
            this.groupBox2.Controls.Add(this.pbOptions);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(10, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(227, 170);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // cbTitleDirectory
            // 
            this.cbTitleDirectory.AutoSize = true;
            this.cbTitleDirectory.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTitleDirectory.Location = new System.Drawing.Point(9, 109);
            this.cbTitleDirectory.Name = "cbTitleDirectory";
            this.cbTitleDirectory.Size = new System.Drawing.Size(161, 17);
            this.cbTitleDirectory.TabIndex = 9;
            this.cbTitleDirectory.Text = "Use title name as directory";
            this.cbTitleDirectory.UseVisualStyleBackColor = true;
            // 
            // cbAutoBrowse
            // 
            this.cbAutoBrowse.AutoSize = true;
            this.cbAutoBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoBrowse.Location = new System.Drawing.Point(9, 87);
            this.cbAutoBrowse.Name = "cbAutoBrowse";
            this.cbAutoBrowse.Size = new System.Drawing.Size(186, 17);
            this.cbAutoBrowse.TabIndex = 8;
            this.cbAutoBrowse.Text = "Auto-browse when adding ISO";
            this.cbAutoBrowse.UseVisualStyleBackColor = true;
            // 
            // cmbPadding
            // 
            this.cmbPadding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPadding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPadding.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPadding.FormattingEnabled = true;
            this.cmbPadding.ItemHeight = 15;
            this.cmbPadding.Items.AddRange(new object[] {
            "Untouched",
            "Partial",
            "Remove All"});
            this.cmbPadding.Location = new System.Drawing.Point(65, 133);
            this.cmbPadding.Name = "cmbPadding";
            this.cmbPadding.Size = new System.Drawing.Size(151, 23);
            this.cmbPadding.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Padding:";
            // 
            // cbAutoRename
            // 
            this.cbAutoRename.AutoSize = true;
            this.cbAutoRename.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoRename.Location = new System.Drawing.Point(9, 65);
            this.cbAutoRename.Name = "cbAutoRename";
            this.cbAutoRename.Size = new System.Drawing.Size(182, 17);
            this.cbAutoRename.TabIndex = 7;
            this.cbAutoRename.Text = "Auto-rename multi-disc games";
            this.cbAutoRename.UseVisualStyleBackColor = true;
            // 
            // cbDeleteRebuilt
            // 
            this.cbDeleteRebuilt.AutoSize = true;
            this.cbDeleteRebuilt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeleteRebuilt.Location = new System.Drawing.Point(9, 43);
            this.cbDeleteRebuilt.Name = "cbDeleteRebuilt";
            this.cbDeleteRebuilt.Size = new System.Drawing.Size(205, 17);
            this.cbDeleteRebuilt.TabIndex = 6;
            this.cbDeleteRebuilt.Text = "Delete rebuilt ISO after completion";
            this.cbDeleteRebuilt.UseVisualStyleBackColor = true;
            // 
            // cbDeleteSource
            // 
            this.cbDeleteSource.AutoSize = true;
            this.cbDeleteSource.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeleteSource.Location = new System.Drawing.Point(9, 21);
            this.cbDeleteSource.Name = "cbDeleteSource";
            this.cbDeleteSource.Size = new System.Drawing.Size(211, 17);
            this.cbDeleteSource.TabIndex = 5;
            this.cbDeleteSource.Text = "Delete original ISO after completion";
            this.cbDeleteSource.UseVisualStyleBackColor = true;
            // 
            // pbOptions
            // 
            this.pbOptions.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOptions.Location = new System.Drawing.Point(58, 0);
            this.pbOptions.Name = "pbOptions";
            this.pbOptions.Size = new System.Drawing.Size(14, 14);
            this.pbOptions.TabIndex = 26;
            this.pbOptions.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.txtFtpPort);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cbFTP);
            this.groupBox3.Controls.Add(this.txtFtpPass);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtFtpUser);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.pbFTP);
            this.groupBox3.Controls.Add(this.txtFtpIp);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(247, 97);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 150);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FTP Transfer";
            // 
            // txtFtpPort
            // 
            this.txtFtpPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpPort.Enabled = false;
            this.txtFtpPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPort.Location = new System.Drawing.Point(75, 113);
            this.txtFtpPort.Name = "txtFtpPort";
            this.txtFtpPort.Size = new System.Drawing.Size(141, 23);
            this.txtFtpPort.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Port:";
            // 
            // cbFTP
            // 
            this.cbFTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFTP.AutoSize = true;
            this.cbFTP.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFTP.Location = new System.Drawing.Point(160, -1);
            this.cbFTP.Margin = new System.Windows.Forms.Padding(0);
            this.cbFTP.Name = "cbFTP";
            this.cbFTP.Size = new System.Drawing.Size(61, 17);
            this.cbFTP.TabIndex = 11;
            this.cbFTP.Text = "Enable";
            this.cbFTP.UseVisualStyleBackColor = true;
            this.cbFTP.CheckedChanged += new System.EventHandler(this.cbFTP_CheckedChanged);
            // 
            // txtFtpPass
            // 
            this.txtFtpPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpPass.Enabled = false;
            this.txtFtpPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPass.Location = new System.Drawing.Point(75, 82);
            this.txtFtpPass.Name = "txtFtpPass";
            this.txtFtpPass.Size = new System.Drawing.Size(141, 23);
            this.txtFtpPass.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Password:";
            // 
            // txtFtpUser
            // 
            this.txtFtpUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpUser.Enabled = false;
            this.txtFtpUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpUser.Location = new System.Drawing.Point(75, 51);
            this.txtFtpUser.Name = "txtFtpUser";
            this.txtFtpUser.Size = new System.Drawing.Size(141, 23);
            this.txtFtpUser.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Username:";
            // 
            // pbFTP
            // 
            this.pbFTP.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.icon_hint;
            this.pbFTP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbFTP.Location = new System.Drawing.Point(80, 0);
            this.pbFTP.Name = "pbFTP";
            this.pbFTP.Size = new System.Drawing.Size(14, 14);
            this.pbFTP.TabIndex = 26;
            this.pbFTP.TabStop = false;
            // 
            // txtFtpIp
            // 
            this.txtFtpIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpIp.Enabled = false;
            this.txtFtpIp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpIp.Location = new System.Drawing.Point(75, 20);
            this.txtFtpIp.Name = "txtFtpIp";
            this.txtFtpIp.Size = new System.Drawing.Size(141, 23);
            this.txtFtpIp.TabIndex = 12;
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
            // Settings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 289);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Iso2God Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRipping)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOptions)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFTP)).EndInit();
            this.ResumeLayout(false);

    }

    public Settings()
    {
        InitializeComponent();
        loadSettings();
        ttSettings.SetToolTip(pbRipping, 
            "Output Path allows you to set a default path for all GODs to be stored in.\n" +
            "Each GOD will be written into sub-directories named using the TitleID.\n\n" +
            "Rebuild Path allows you to specify a default path to store rebuilt ISO images.");
        ttFTP.SetToolTip(pbFTP, 
            "Once an ISO image has been converted to a GOD container,\n" +
            "it can be automatically uploaded to your Xbox 360 using FTP,\n" +
            "you'll need to have an FTP server running on it.");
        ttOptions.SetToolTip(pbOptions, "To change the behaviour of Iso2God, you can change the options below.");
    }

    private void loadSettings()
    {
        try
        {
            txtOut.Text = Properties.Settings.Default["OutputPath"].ToString();
            txtRebuild.Text = Properties.Settings.Default["RebuildPath"].ToString();
            cbDeleteSource.Checked = (bool)Properties.Settings.Default["DeleteSource"];
            cbDeleteRebuilt.Checked = (bool)Properties.Settings.Default["DeleteRebuilt"];
            cbAutoRename.Checked = (bool)Properties.Settings.Default["AutoRenameMultiDisc"];
            cbAutoBrowse.Checked = (bool)Properties.Settings.Default["AutoBrowse"];
            cbFTP.Checked = (bool)Properties.Settings.Default["FtpUpload"];
            txtFtpIp.Text = Properties.Settings.Default["FtpIP"].ToString();
            txtFtpUser.Text = Properties.Settings.Default["FtpUser"].ToString();
            txtFtpPass.Text = Properties.Settings.Default["FtpPass"].ToString();
            txtFtpPort.Text = Properties.Settings.Default["FtpPort"].ToString();
            cmbPadding.SelectedIndex = (int)Properties.Settings.Default["DefaultPadding"];
            cbTitleDirectory.Checked = (bool)Properties.Settings.Default["TitleDirectory"];
        }
        catch
        {
            MessageBox.Show("Unable to load User Settings.\n\nIf this is the first time you've seen this message, the problem will be resolved when you save your settings.\n\nIf you've seen it before, please contact the author about this issue.");
        }
    }

    private void btnDestBrowse_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
        folderBrowserDialog.SelectedPath = txtOut.Text;
        folderBrowserDialog.ShowNewFolderButton = true;
        folderBrowserDialog.Description = "Choose where to store your GOD containers:";
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            txtOut.Text = folderBrowserDialog.SelectedPath;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (txtOut.Text.Length != 0 && !txtOut.Text.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            txtOut.Text += Path.DirectorySeparatorChar;
        }
        if (txtRebuild.Text.Length != 0 && !txtRebuild.Text.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            txtRebuild.Text += Path.DirectorySeparatorChar;
        }
        Properties.Settings.Default["OutputPath"] = txtOut.Text;
        Properties.Settings.Default["RebuildPath"] = txtRebuild.Text;
        Properties.Settings.Default["DeleteSource"] = cbDeleteSource.Checked;
        Properties.Settings.Default["DeleteRebuilt"] = cbDeleteRebuilt.Checked;
        Properties.Settings.Default["AutoRenameMultiDisc"] = cbAutoRename.Checked;
        Properties.Settings.Default["AutoBrowse"] = cbAutoBrowse.Checked;
        Properties.Settings.Default["FtpUpload"] = cbFTP.Checked;
        Properties.Settings.Default["FtpIP"] = txtFtpIp.Text;
        Properties.Settings.Default["FtpUser"] = txtFtpUser.Text;
        Properties.Settings.Default["FtpPass"] = txtFtpPass.Text;
        Properties.Settings.Default["FtpPort"] = txtFtpPort.Text;
        Properties.Settings.Default["DefaultPadding"] = cmbPadding.SelectedIndex;
        Properties.Settings.Default["TitleDirectory"] = cbTitleDirectory.Checked;
        Properties.Settings.Default.Save();
        (base.Owner as Main).UpdateSpace();
        Close();
    }

    private void btnRebuild_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
        folderBrowserDialog.SelectedPath = txtRebuild.Text;
        folderBrowserDialog.ShowNewFolderButton = true;
        folderBrowserDialog.Description = "Choose where to store rebuilt ISO images:";
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            txtRebuild.Text = folderBrowserDialog.SelectedPath;
        }
    }

    private void cbFTP_CheckedChanged(object sender, EventArgs e)
    {
        txtFtpIp.Enabled = cbFTP.Checked;
        txtFtpUser.Enabled = cbFTP.Checked;
        txtFtpPass.Enabled = cbFTP.Checked;
        txtFtpPort.Enabled = cbFTP.Checked;
    }
}
