using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Chilano.Iso2God.Properties;

namespace Chilano.Iso2God;

public class Settings : Form
{
	private IContainer components;

	private GroupBox groupBox2;

	private TextBox txtOut;

	private Label label3;

	private Button btnOutBrowse;

	private Button btnSave;

	private Button btnCancel;

	private PictureBox pbRipping;

	private ToolTip ttSettings;

	private Button btnRebuild;

	private TextBox txtRebuild;

	private Label label1;

	private GroupBox groupBox1;

	private PictureBox pbOptions;

	private CheckBox cbRebuildCheck;

	private CheckBox cbAlwaysSave;

	private CheckBox cbAutoRename;

	private GroupBox groupBox3;

	private TextBox txtFtpUser;

	private Label label2;

	private PictureBox pbFTP;

	private TextBox txtFtpIp;

	private Label label4;

	private TextBox txtFtpPass;

	private Label label5;

	private CheckBox cbFTP;

	private ToolTip ttFTP;

	private ToolTip ttOptions;

	private Label label6;

	private ComboBox cmbPadding;

	private TextBox txtFtpPort;

	private Label label7;

	private CheckBox cbAutoBrowse;

	public Settings()
	{
		InitializeComponent();
		loadSettings();
		ttSettings.SetToolTip(pbRipping, "Output Path allows you to set a default path for all GODs to be stored in. Each GOD will be stored\nin a sub-directory using the TitleID of the default.xex stored in the ISO.\n\nRebuild Path allows you to specify a default path to store rebuilt ISO images.");
		ttFTP.SetToolTip(pbFTP, "Once an ISO image has been converted to a GOD container, it can be automatically\nuploaded to your Xbox 360 using FTP (if you are running a dashboard such as XexMenu\nwith a built-in FTP server).");
		ttOptions.SetToolTip(pbOptions, "To change the behaviour of Iso2God, you can change the options below.");
	}

	private void loadSettings()
	{
		try
		{
			txtOut.Text = Chilano.Iso2God.Properties.Settings.Default["OutputPath"].ToString();
			txtRebuild.Text = Chilano.Iso2God.Properties.Settings.Default["RebuildPath"].ToString();
			cbRebuildCheck.Checked = (bool)Chilano.Iso2God.Properties.Settings.Default["RebuiltCheck"];
			cbAlwaysSave.Checked = (bool)Chilano.Iso2God.Properties.Settings.Default["AlwaysSave"];
			cbAutoRename.Checked = (bool)Chilano.Iso2God.Properties.Settings.Default["AutoRenameMultiDisc"];
			cbAutoBrowse.Checked = (bool)Chilano.Iso2God.Properties.Settings.Default["AutoBrowse"];
			cbFTP.Checked = (bool)Chilano.Iso2God.Properties.Settings.Default["FtpUpload"];
			txtFtpIp.Text = Chilano.Iso2God.Properties.Settings.Default["FtpIP"].ToString();
			txtFtpUser.Text = Chilano.Iso2God.Properties.Settings.Default["FtpUser"].ToString();
			txtFtpPass.Text = Chilano.Iso2God.Properties.Settings.Default["FtpPass"].ToString();
			txtFtpPort.Text = Chilano.Iso2God.Properties.Settings.Default["FtpPort"].ToString();
			cmbPadding.SelectedIndex = (int)Chilano.Iso2God.Properties.Settings.Default["DefaultPadding"];
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
		folderBrowserDialog.ShowNewFolderButton = true;
		folderBrowserDialog.Description = "Choose where to store your GOD containers:";
		if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
		{
			txtOut.Text = folderBrowserDialog.SelectedPath;
			if (!txtOut.Text.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				txtOut.Text += Path.DirectorySeparatorChar;
			}
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Chilano.Iso2God.Properties.Settings.Default["OutputPath"] = txtOut.Text;
		Chilano.Iso2God.Properties.Settings.Default["RebuildPath"] = txtRebuild.Text;
		Chilano.Iso2God.Properties.Settings.Default["RebuiltCheck"] = cbRebuildCheck.Checked;
		Chilano.Iso2God.Properties.Settings.Default["AlwaysSave"] = cbAlwaysSave.Checked;
		Chilano.Iso2God.Properties.Settings.Default["AutoRenameMultiDisc"] = cbAutoRename.Checked;
		Chilano.Iso2God.Properties.Settings.Default["AutoBrowse"] = cbAutoBrowse.Checked;
		Chilano.Iso2God.Properties.Settings.Default["FtpUpload"] = cbFTP.Checked;
		Chilano.Iso2God.Properties.Settings.Default["FtpIP"] = txtFtpIp.Text;
		Chilano.Iso2God.Properties.Settings.Default["FtpUser"] = txtFtpUser.Text;
		Chilano.Iso2God.Properties.Settings.Default["FtpPass"] = txtFtpPass.Text;
		Chilano.Iso2God.Properties.Settings.Default["FtpPort"] = txtFtpPort.Text;
		Chilano.Iso2God.Properties.Settings.Default["DefaultPadding"] = cmbPadding.SelectedIndex;
		Chilano.Iso2God.Properties.Settings.Default.Save();
		(base.Owner as Main).UpdateSpace();
		Close();
	}

	private void btnRebuild_Click(object sender, EventArgs e)
	{
		FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
		folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
		folderBrowserDialog.ShowNewFolderButton = true;
		folderBrowserDialog.Description = "Choose where to store rebuilt ISO images:";
		if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
		{
			txtRebuild.Text = folderBrowserDialog.SelectedPath;
			if (!txtRebuild.Text.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				txtRebuild.Text += Path.DirectorySeparatorChar;
			}
		}
	}

	private void cbFTP_CheckedChanged(object sender, EventArgs e)
	{
		txtFtpIp.Enabled = cbFTP.Checked;
		txtFtpUser.Enabled = cbFTP.Checked;
		txtFtpPass.Enabled = cbFTP.Checked;
		txtFtpPort.Enabled = cbFTP.Checked;
	}

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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRebuild = new System.Windows.Forms.Button();
            this.txtRebuild = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbRipping = new System.Windows.Forms.PictureBox();
            this.btnOutBrowse = new System.Windows.Forms.Button();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAutoBrowse = new System.Windows.Forms.CheckBox();
            this.cmbPadding = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbAutoRename = new System.Windows.Forms.CheckBox();
            this.cbAlwaysSave = new System.Windows.Forms.CheckBox();
            this.cbRebuildCheck = new System.Windows.Forms.CheckBox();
            this.pbOptions = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtFtpPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbFTP = new System.Windows.Forms.CheckBox();
            this.txtFtpPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFtpUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbFTP = new System.Windows.Forms.PictureBox();
            this.txtFtpIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRipping)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOptions)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFTP)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.btnRebuild);
            this.groupBox2.Controls.Add(this.txtRebuild);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.pbRipping);
            this.groupBox2.Controls.Add(this.btnOutBrowse);
            this.groupBox2.Controls.Add(this.txtOut);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 80);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Location Settings";
            // 
            // btnRebuild
            // 
            this.btnRebuild.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRebuild.Location = new System.Drawing.Point(365, 46);
            this.btnRebuild.Name = "btnRebuild";
            this.btnRebuild.Size = new System.Drawing.Size(75, 23);
            this.btnRebuild.TabIndex = 28;
            this.btnRebuild.Text = "&Browse";
            this.btnRebuild.UseVisualStyleBackColor = true;
            this.btnRebuild.Click += new System.EventHandler(this.btnRebuild_Click);
            // 
            // txtRebuild
            // 
            this.txtRebuild.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRebuild.Location = new System.Drawing.Point(84, 46);
            this.txtRebuild.Name = "txtRebuild";
            this.txtRebuild.Size = new System.Drawing.Size(275, 22);
            this.txtRebuild.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Rebuild Path:";
            // 
            // pbRipping
            // 
            this.pbRipping.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.Hint;
            this.pbRipping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbRipping.Location = new System.Drawing.Point(105, 0);
            this.pbRipping.Name = "pbRipping";
            this.pbRipping.Size = new System.Drawing.Size(15, 15);
            this.pbRipping.TabIndex = 26;
            this.pbRipping.TabStop = false;
            // 
            // btnOutBrowse
            // 
            this.btnOutBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutBrowse.Location = new System.Drawing.Point(365, 17);
            this.btnOutBrowse.Name = "btnOutBrowse";
            this.btnOutBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnOutBrowse.TabIndex = 2;
            this.btnOutBrowse.Text = "&Browse";
            this.btnOutBrowse.UseVisualStyleBackColor = true;
            this.btnOutBrowse.Click += new System.EventHandler(this.btnDestBrowse_Click);
            // 
            // txtOut
            // 
            this.txtOut.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOut.Location = new System.Drawing.Point(84, 18);
            this.txtOut.Name = "txtOut";
            this.txtOut.Size = new System.Drawing.Size(275, 22);
            this.txtOut.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Output Path:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(283, 249);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(377, 249);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.cbAutoBrowse);
            this.groupBox1.Controls.Add(this.cmbPadding);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbAutoRename);
            this.groupBox1.Controls.Add(this.cbAlwaysSave);
            this.groupBox1.Controls.Add(this.cbRebuildCheck);
            this.groupBox1.Controls.Add(this.pbOptions);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 145);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // cbAutoBrowse
            // 
            this.cbAutoBrowse.AutoSize = true;
            this.cbAutoBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoBrowse.Location = new System.Drawing.Point(9, 90);
            this.cbAutoBrowse.Name = "cbAutoBrowse";
            this.cbAutoBrowse.Size = new System.Drawing.Size(186, 17);
            this.cbAutoBrowse.TabIndex = 33;
            this.cbAutoBrowse.Text = "Auto-browse when adding ISO";
            this.cbAutoBrowse.UseVisualStyleBackColor = true;
            // 
            // cmbPadding
            // 
            this.cmbPadding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPadding.FormattingEnabled = true;
            this.cmbPadding.Items.AddRange(new object[] {
            "None",
            "Partial (ISO Cropped)",
            "Full (ISO Rebuilt)"});
            this.cmbPadding.Location = new System.Drawing.Point(81, 113);
            this.cmbPadding.Name = "cmbPadding";
            this.cmbPadding.Size = new System.Drawing.Size(133, 21);
            this.cmbPadding.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Padding:";
            // 
            // cbAutoRename
            // 
            this.cbAutoRename.AutoSize = true;
            this.cbAutoRename.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoRename.Location = new System.Drawing.Point(9, 67);
            this.cbAutoRename.Name = "cbAutoRename";
            this.cbAutoRename.Size = new System.Drawing.Size(182, 17);
            this.cbAutoRename.TabIndex = 29;
            this.cbAutoRename.Text = "Auto-rename multi-disc games";
            this.cbAutoRename.UseVisualStyleBackColor = true;
            // 
            // cbAlwaysSave
            // 
            this.cbAlwaysSave.AutoSize = true;
            this.cbAlwaysSave.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAlwaysSave.Location = new System.Drawing.Point(9, 21);
            this.cbAlwaysSave.Name = "cbAlwaysSave";
            this.cbAlwaysSave.Size = new System.Drawing.Size(144, 17);
            this.cbAlwaysSave.TabIndex = 28;
            this.cbAlwaysSave.Text = "Always save rebuilt ISO";
            this.cbAlwaysSave.UseVisualStyleBackColor = true;
            // 
            // cbRebuildCheck
            // 
            this.cbRebuildCheck.AutoSize = true;
            this.cbRebuildCheck.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRebuildCheck.Location = new System.Drawing.Point(9, 44);
            this.cbRebuildCheck.Name = "cbRebuildCheck";
            this.cbRebuildCheck.Size = new System.Drawing.Size(199, 17);
            this.cbRebuildCheck.TabIndex = 27;
            this.cbRebuildCheck.Text = "Ask if rebuilt ISO should be saved";
            this.cbRebuildCheck.UseVisualStyleBackColor = true;
            // 
            // pbOptions
            // 
            this.pbOptions.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.Hint;
            this.pbOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOptions.Location = new System.Drawing.Point(55, 0);
            this.pbOptions.Name = "pbOptions";
            this.pbOptions.Size = new System.Drawing.Size(15, 15);
            this.pbOptions.TabIndex = 26;
            this.pbOptions.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.txtFtpPort);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cbFTP);
            this.groupBox3.Controls.Add(this.txtFtpPass);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtFtpUser);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.pbFTP);
            this.groupBox3.Controls.Add(this.txtFtpIp);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(238, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 145);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FTP Transfer";
            // 
            // txtFtpPort
            // 
            this.txtFtpPort.Enabled = false;
            this.txtFtpPort.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPort.Location = new System.Drawing.Point(78, 105);
            this.txtFtpPort.Name = "txtFtpPort";
            this.txtFtpPort.Size = new System.Drawing.Size(133, 22);
            this.txtFtpPort.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Port:";
            // 
            // cbFTP
            // 
            this.cbFTP.AutoSize = true;
            this.cbFTP.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFTP.Location = new System.Drawing.Point(153, 0);
            this.cbFTP.Name = "cbFTP";
            this.cbFTP.Size = new System.Drawing.Size(61, 17);
            this.cbFTP.TabIndex = 30;
            this.cbFTP.Text = "Enable";
            this.cbFTP.UseVisualStyleBackColor = true;
            this.cbFTP.CheckedChanged += new System.EventHandler(this.cbFTP_CheckedChanged);
            // 
            // txtFtpPass
            // 
            this.txtFtpPass.Enabled = false;
            this.txtFtpPass.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPass.Location = new System.Drawing.Point(78, 77);
            this.txtFtpPass.Name = "txtFtpPass";
            this.txtFtpPass.Size = new System.Drawing.Size(133, 22);
            this.txtFtpPass.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Password:";
            // 
            // txtFtpUser
            // 
            this.txtFtpUser.Enabled = false;
            this.txtFtpUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpUser.Location = new System.Drawing.Point(78, 49);
            this.txtFtpUser.Name = "txtFtpUser";
            this.txtFtpUser.Size = new System.Drawing.Size(133, 22);
            this.txtFtpUser.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Username:";
            // 
            // pbFTP
            // 
            this.pbFTP.BackgroundImage = global::Chilano_Iso2God_Properties_Resources.Hint;
            this.pbFTP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbFTP.Location = new System.Drawing.Point(81, 0);
            this.pbFTP.Name = "pbFTP";
            this.pbFTP.Size = new System.Drawing.Size(15, 15);
            this.pbFTP.TabIndex = 26;
            this.pbFTP.TabStop = false;
            // 
            // txtFtpIp
            // 
            this.txtFtpIp.Enabled = false;
            this.txtFtpIp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpIp.Location = new System.Drawing.Point(78, 21);
            this.txtFtpIp.Name = "txtFtpIp";
            this.txtFtpIp.Size = new System.Drawing.Size(133, 22);
            this.txtFtpIp.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "IP Address:";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 280);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Iso2God Settings";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRipping)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOptions)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFTP)).EndInit();
            this.ResumeLayout(false);

	}
}
