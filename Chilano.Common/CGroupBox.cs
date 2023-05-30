using System.Drawing;
using System.Windows.Forms;

namespace Chilano.Common;

public class CGroupBox : GroupBox
{
	private Color borderColor;

	public Color BorderColor
	{
		get
		{
			return borderColor;
		}
		set
		{
			borderColor = value;
		}
	}

	public CGroupBox()
	{
		borderColor = Color.Black;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Size size = TextRenderer.MeasureText(Text, Font);
		Rectangle clipRectangle = e.ClipRectangle;
		clipRectangle.Y += size.Height / 2;
		clipRectangle.Height -= size.Height / 2;
		ControlPaint.DrawBorder(e.Graphics, clipRectangle, borderColor, ButtonBorderStyle.Solid);
		Rectangle clipRectangle2 = e.ClipRectangle;
		clipRectangle2.X += 6;
		clipRectangle2.Width = size.Width;
		clipRectangle2.Height = size.Height;
		e.Graphics.FillRectangle(new SolidBrush(BackColor), clipRectangle2);
		e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), clipRectangle2);
	}
}
