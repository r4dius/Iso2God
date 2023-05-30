using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Chilano.Common.Design;

public class frmSwitchPages : Form
{
	private class MultiPanePageItem
	{
		private MultiPanePage myPage;

		public MultiPanePage Page => myPage;

		public MultiPanePageItem(MultiPanePage thePg)
		{
			myPage = thePg;
		}

		public override string ToString()
		{
			return myPage.Name;
		}
	}

	private Label myCtlLblSwitchPage;

	private ComboBox myCtlCmbItems;

	private CheckBox myCtlChkSetSelectedPage;

	private Button myCtlBtnOK;

	private Button myCtlBtnCancel;

	private MultiPaneControlDesigner myDesigner;

	private MultiPanePage myFutureSelectedItem;

	private bool mySetSelectedPage;

	private IContainer components = null;

	public MultiPanePage FutureSelection => myFutureSelectedItem;

	public bool SetSelectedPage => mySetSelectedPage;

	public frmSwitchPages(MultiPaneControlDesigner theDesigner)
	{
		myDesigner = theDesigner;
		InitializeComponent();
	}

	protected override void OnLoad(EventArgs e)
	{
		foreach (MultiPanePage control in myDesigner.DesignedControl.Controls)
		{
			MultiPanePageItem multiPanePageItem = new MultiPanePageItem(control);
			myCtlCmbItems.Items.Add(multiPanePageItem);
			if (myDesigner.DesignerSelectedPage == control)
			{
				myCtlCmbItems.SelectedItem = multiPanePageItem;
			}
		}
	}

	private void Handler_OK(object sender, EventArgs e)
	{
		myFutureSelectedItem = ((MultiPanePageItem)myCtlCmbItems.SelectedItem).Page;
		mySetSelectedPage = myCtlChkSetSelectedPage.Checked;
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
		this.myCtlLblSwitchPage = new System.Windows.Forms.Label();
		this.myCtlCmbItems = new System.Windows.Forms.ComboBox();
		this.myCtlChkSetSelectedPage = new System.Windows.Forms.CheckBox();
		this.myCtlBtnOK = new System.Windows.Forms.Button();
		this.myCtlBtnCancel = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.myCtlLblSwitchPage.AutoSize = true;
		this.myCtlLblSwitchPage.Location = new System.Drawing.Point(9, 9);
		this.myCtlLblSwitchPage.Name = "myCtlLblSwitchPage";
		this.myCtlLblSwitchPage.TabIndex = 0;
		this.myCtlLblSwitchPage.Text = "Switch the page to:";
		this.myCtlCmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.myCtlCmbItems.Location = new System.Drawing.Point(12, 25);
		this.myCtlCmbItems.Name = "myCtlCmbItems";
		this.myCtlCmbItems.Size = new System.Drawing.Size(227, 21);
		this.myCtlCmbItems.TabIndex = 1;
		this.myCtlChkSetSelectedPage.Location = new System.Drawing.Point(12, 61);
		this.myCtlChkSetSelectedPage.Name = "myCtlChkSetSelectedPage";
		this.myCtlChkSetSelectedPage.Size = new System.Drawing.Size(220, 17);
		this.myCtlChkSetSelectedPage.TabIndex = 2;
		this.myCtlChkSetSelectedPage.Text = "Also set the SelectedPage property";
		this.myCtlBtnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.myCtlBtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.myCtlBtnOK.Location = new System.Drawing.Point(77, 97);
		this.myCtlBtnOK.Name = "myCtlBtnOK";
		this.myCtlBtnOK.TabIndex = 3;
		this.myCtlBtnOK.Text = "OK";
		this.myCtlBtnOK.Click += new System.EventHandler(Handler_OK);
		this.myCtlBtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.myCtlBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.myCtlBtnCancel.Location = new System.Drawing.Point(164, 97);
		this.myCtlBtnCancel.Name = "myCtlBtnCancel";
		this.myCtlBtnCancel.TabIndex = 4;
		this.myCtlBtnCancel.Text = "Cancel";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		base.ClientSize = new System.Drawing.Size(251, 132);
		base.Controls.Add(this.myCtlLblSwitchPage);
		base.Controls.Add(this.myCtlCmbItems);
		base.Controls.Add(this.myCtlChkSetSelectedPage);
		base.Controls.Add(this.myCtlBtnCancel);
		base.Controls.Add(this.myCtlBtnOK);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmSwitchPages";
		base.ShowInTaskbar = false;
		this.Text = "Switch Pages";
		base.ResumeLayout(false);
	}
}
