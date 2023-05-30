using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chilano.Common.Design;

namespace Chilano.Common;

[ToolboxItem(false)]
[Designer(typeof(MultiPanePageDesigner))]
[DesignTimeVisible(false)]
public class MultiPanePage : Panel
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			base.Dock = value;
		}
	}

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Point Location
	{
		get
		{
			return base.Location;
		}
		set
		{
			base.Location = value;
		}
	}
}
