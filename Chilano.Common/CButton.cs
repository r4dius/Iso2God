using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chilano.Common;

public class CButton : Button
{
	private Bitmap bNormal;

	private Bitmap bOver;

	private Bitmap bDown;

	private bool isOver = false;

	private bool isDown = false;

	public CButton()
	{
		base.MouseEnter += CButton_MouseEnter;
		base.MouseLeave += CButton_MouseLeave;
		base.MouseDown += CButton_MouseDown;
		base.MouseUp += CButton_MouseUp;
	}

	public void SetImages(Bitmap Normal, Bitmap Over, Bitmap Down)
	{
		bNormal = Normal;
		bOver = Over;
		bDown = Down;
		BackgroundImage = Normal;
	}

	private void CButton_MouseUp(object sender, MouseEventArgs e)
	{
		isDown = false;
		BackgroundImage = (isOver ? bOver : bNormal);
	}

	private void CButton_MouseDown(object sender, MouseEventArgs e)
	{
		isDown = true;
		BackgroundImage = bDown;
	}

	private void CButton_MouseLeave(object sender, EventArgs e)
	{
		isOver = false;
		BackgroundImage = bNormal;
	}

	private void CButton_MouseEnter(object sender, EventArgs e)
	{
		isOver = true;
		BackgroundImage = bOver;
	}
}
