using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Chilano.Common.Design;

namespace Chilano.Common;

[Designer(typeof(MultiPaneControlDesigner))]
[ToolboxItem(typeof(MultiPaneControlToolboxItem))]
public class MultiPaneControl : Control
{
	protected static readonly Size ourDefaultSize = new Size(200, 100);

	protected MultiPanePage mySelectedPage;

	protected override Size DefaultSize => ourDefaultSize;

	[Editor(typeof(MultiPaneControlSelectedPageEditor), typeof(UITypeEditor))]
	public MultiPanePage SelectedPage
	{
		get
		{
			return mySelectedPage;
		}
		set
		{
			if (mySelectedPage != value)
			{
				if (this.SelectedPageChanging != null)
				{
					this.SelectedPageChanging(this, EventArgs.Empty);
				}
				if (mySelectedPage != null)
				{
					mySelectedPage.Visible = false;
				}
				mySelectedPage = value;
				mySelectedPage.Refresh();
				if (mySelectedPage != null)
				{
					mySelectedPage.Visible = true;
				}
				if (this.SelectedPageChanged != null)
				{
					this.SelectedPageChanged(this, EventArgs.Empty);
				}
			}
		}
	}

	public event EventHandler SelectedPageChanging;

	public event EventHandler SelectedPageChanged;

	public MultiPaneControl()
	{
		base.ControlAdded += Handler_ControlAdded;
		base.SizeChanged += Handler_SizeChanged;
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		BackColor = Color.Transparent;
	}

	private void Handler_ControlAdded(object theSender, ControlEventArgs theArgs)
	{
		if (theArgs.Control is MultiPanePage)
		{
			MultiPanePage multiPanePage = (MultiPanePage)theArgs.Control;
			multiPanePage.Location = new Point(0, 0);
			multiPanePage.Size = base.ClientSize;
			multiPanePage.Dock = DockStyle.Fill;
			if (SelectedPage == null)
			{
				SelectedPage = multiPanePage;
			}
			else
			{
				multiPanePage.Visible = false;
			}
		}
		else
		{
			base.Controls.Remove(theArgs.Control);
		}
	}

	private void Handler_SizeChanged(object sender, EventArgs e)
	{
		foreach (MultiPanePage control in base.Controls)
		{
			control.Size = base.ClientSize;
		}
	}
}
