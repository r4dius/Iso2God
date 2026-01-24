using Chilano.Iso2God.Ftp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

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
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private ComboBox cmbFtpPath;
    private RadioButton radCustom;
    private RadioButton radDefault;
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFTP)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.btnSave.Location = new System.Drawing.Point(219, 306);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(300, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.txtFtpPort);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtFtpPass);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtFtpUser);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pbFTP);
            this.groupBox2.Controls.Add(this.txtFtpIp);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(10, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 236);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FTP Server";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.radCustom);
            this.groupBox3.Controls.Add(this.radDefault);
            this.groupBox3.Controls.Add(this.cmbFtpPath);
            this.groupBox3.Controls.Add(this.txtFtpPath);
            this.groupBox3.Location = new System.Drawing.Point(11, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(342, 80);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Path";
            // 
            // radCustom
            // 
            this.radCustom.AutoSize = true;
            this.radCustom.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCustom.Location = new System.Drawing.Point(136, 21);
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
            this.radDefault.Location = new System.Drawing.Point(11, 21);
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
            "USB Port #1 (Usb1)",
            "USB Port #2 (Usb2)"});
            this.cmbFtpPath.Location = new System.Drawing.Point(11, 44);
            this.cmbFtpPath.Name = "cmbFtpPath";
            this.cmbFtpPath.Size = new System.Drawing.Size(320, 23);
            this.cmbFtpPath.TabIndex = 2;
            // 
            // txtFtpPath
            // 
            this.txtFtpPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPath.Location = new System.Drawing.Point(11, 44);
            this.txtFtpPath.Name = "txtFtpPath";
            this.txtFtpPath.Size = new System.Drawing.Size(320, 23);
            this.txtFtpPath.TabIndex = 3;
            // 
            // txtFtpPort
            // 
            this.txtFtpPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPort.Location = new System.Drawing.Point(89, 113);
            this.txtFtpPort.Name = "txtFtpPort";
            this.txtFtpPort.Size = new System.Drawing.Size(264, 23);
            this.txtFtpPort.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Port:";
            // 
            // txtFtpPass
            // 
            this.txtFtpPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpPass.Location = new System.Drawing.Point(89, 82);
            this.txtFtpPass.Name = "txtFtpPass";
            this.txtFtpPass.Size = new System.Drawing.Size(264, 23);
            this.txtFtpPass.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Password:";
            // 
            // txtFtpUser
            // 
            this.txtFtpUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFtpUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFtpUser.Location = new System.Drawing.Point(89, 51);
            this.txtFtpUser.Name = "txtFtpUser";
            this.txtFtpUser.Size = new System.Drawing.Size(264, 23);
            this.txtFtpUser.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 56);
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
            this.txtFtpIp.Location = new System.Drawing.Point(89, 20);
            this.txtFtpIp.Name = "txtFtpIp";
            this.txtFtpIp.Size = new System.Drawing.Size(264, 23);
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
            this.btnTest.Location = new System.Drawing.Point(138, 306);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 25);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Test";
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbAutoBrowse);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(10, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // Settings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 340);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox2);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFTP)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

    }

    public Settings()
    {
        InitializeComponent();
        loadSettings();
        ttFTP.SetToolTip(pbFTP, 
            "Once an ISO image has been converted to a GOD package,\n" +
            "it can be automatically uploaded to your Xbox 360 using FTP,\n" +
            "you'll need to have an FTP server running on it.");
    }

    private void loadSettings()
    {
        try
        {
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
}
