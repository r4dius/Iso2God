using SimpleJson;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace Chilano.Iso2God;

public class About : Form
{
    private PictureBox pictureBox1;
    private Label lblVersion;
    private Button btnClose;
    private Label label1;
    private Button btnCheckUpdate;
    private string _latestReleaseUrl;

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblVersion.BackColor = System.Drawing.Color.White;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(290, 17);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(165, 16);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(410, 278);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(10, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(474, 174);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(-14, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(522, 92);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckUpdate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckUpdate.Location = new System.Drawing.Point(274, 278);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(130, 25);
            this.btnCheckUpdate.TabIndex = 4;
            this.btnCheckUpdate.Text = "Check for update...";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // About
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(494, 312);
            this.Controls.Add(this.btnCheckUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

    }

    public About()
    {
        InitializeComponent();
        lblVersion.BackColor = Color.Transparent;
        lblVersion.Parent = pictureBox1;
    }

    private void About_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "v" + ((Main)base.Owner).getVersion(build: true, revision: false) + " - r4dius github";
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lblVersion_Click(object sender, EventArgs e)
    {
        letsGoToGithub();
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
        letsGoToGithub();
    }

    private void letsGoToGithub()
    {
        System.Diagnostics.Process.Start("https://github.com/r4dius/Iso2God");
    }

    private async void UpdateCheck()
    {
        string Url = "https://api.github.com/repos/r4dius/Iso2God/releases/latest";

        btnCheckUpdate.Text = "Checking for update...";
        btnCheckUpdate.Enabled = false;

        using (HttpClient Client = new HttpClient())
        {
            try
            {
                Client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("product", ProductName));

                HttpResponseMessage Response = await Client.GetAsync(Url);
                Response.EnsureSuccessStatusCode();

                string data = await Response.Content.ReadAsStringAsync();

                var jsonObject = SimpleJson.SimpleJson.DeserializeObject(data) as JsonObject;
                if (jsonObject == null)
                {
                    btnCheckUpdate.Text = "Update check failed...";
                    return;
                }

                string tagString = jsonObject["tag_name"].ToString();
                if (tagString.StartsWith("v")) tagString = tagString.Substring(1);

                Version currentVersion = new Version(Info.version);
                //currentVersion = new Version("1.4.0.0");
                Version latestVersion = new Version(tagString);

                if (latestVersion > currentVersion)
                {
                    string releaseUrl = jsonObject["html_url"].ToString();

                    btnCheckUpdate.Text = $"Get version v{latestVersion}";
                    btnCheckUpdate.Enabled = true;

                    btnCheckUpdate.Click += (s, e) => Process.Start(releaseUrl);
                }
                else
                {
                    btnCheckUpdate.Text = "No update available";
                }
            }
            catch (Exception)
            {
                btnCheckUpdate.Text = "Update check failed...";
            }
        }
    }

    private async void btnCheckUpdate_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_latestReleaseUrl))
        {
            Process.Start(_latestReleaseUrl);
        }
        else
        {
            UpdateCheck();
        }
    }
}
