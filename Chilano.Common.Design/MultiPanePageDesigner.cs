using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Chilano.Common.Design;

internal class MultiPanePageDesigner : ScrollableControlDesigner
{
	private Pen myBorderPen_Light;

	private Pen myBorderPen_Dark;

	private int myOrigX = -1;

	private int myOrigY = -1;

	private bool myMouseMovement = false;

	public override SelectionRules SelectionRules
	{
		get
		{
			SelectionRules selectionRules = base.SelectionRules;
			if (Control.Parent is MultiPaneControl)
			{
				selectionRules &= ~SelectionRules.AllSizeable;
			}
			return selectionRules;
		}
	}

	public override DesignerVerbCollection Verbs
	{
		get
		{
			DesignerVerbCollection designerVerbCollection = new DesignerVerbCollection();
			foreach (DesignerVerb verb in base.Verbs)
			{
				designerVerbCollection.Add(verb);
			}
			MultiPaneControlDesigner parentControlDesigner = GetParentControlDesigner();
			if (parentControlDesigner != null)
			{
				foreach (DesignerVerb verb2 in parentControlDesigner.Verbs)
				{
					designerVerbCollection.Add(verb2);
				}
			}
			return designerVerbCollection;
		}
	}

	protected MultiPanePage DesignedControl => (MultiPanePage)Control;

	protected Pen BorderPen
	{
		get
		{
			if ((double)Control.BackColor.GetBrightness() < 0.5)
			{
				return InternalEnsureLightPenCreated();
			}
			return InternalEnsureDarkPenCreated();
		}
	}

	protected override void Dispose(bool theDisposing)
	{
		if (theDisposing)
		{
			if (myBorderPen_Dark != null)
			{
				myBorderPen_Dark.Dispose();
			}
			if (myBorderPen_Light != null)
			{
				myBorderPen_Light.Dispose();
			}
		}
		base.Dispose(theDisposing);
	}

	protected override void OnPaintAdornments(PaintEventArgs pe)
	{
		DrawBorder(pe.Graphics);
		base.OnPaintAdornments(pe);
	}

	public override bool CanBeParentedTo(IDesigner theParentDesigner)
	{
		if (theParentDesigner != null)
		{
			return theParentDesigner.Component is MultiPaneControl;
		}
		return false;
	}

	protected override bool GetHitTest(Point pt)
	{
		return false;
	}

	protected override void OnMouseDragBegin(int theX, int theY)
	{
		myOrigX = theX;
		myOrigY = theY;
	}

	protected override void OnMouseDragMove(int theX, int theY)
	{
		if (theX > myOrigX + 3 || theX < myOrigX - 3 || theY > myOrigY + 3 || theY < myOrigY - 3)
		{
			myMouseMovement = true;
			base.OnMouseDragBegin(myOrigX, myOrigY);
			base.OnMouseDragMove(theX, theY);
		}
	}

	protected override void OnMouseDragEnd(bool theCancel)
	{
		bool flag = !myMouseMovement && Control.Parent != null;
		if (flag)
		{
			ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SetSelectedComponents(new Control[1] { Control.Parent });
			}
			else
			{
				flag = false;
			}
		}
		if (!flag)
		{
			base.OnMouseDragEnd(theCancel);
		}
		myMouseMovement = false;
	}

	private Pen InternalEnsureDarkPenCreated()
	{
		if (myBorderPen_Dark == null)
		{
			myBorderPen_Dark = InternalCreatePen(Color.Black);
		}
		return myBorderPen_Dark;
	}

	private Pen InternalEnsureLightPenCreated()
	{
		if (myBorderPen_Light == null)
		{
			myBorderPen_Light = InternalCreatePen(Color.White);
		}
		return myBorderPen_Light;
	}

	private static Pen InternalCreatePen(Color theClr)
	{
		Pen pen = new Pen(theClr);
		pen.DashStyle = DashStyle.Dash;
		return pen;
	}

	internal void InternalOnDragDrop(DragEventArgs theArgs)
	{
		OnDragDrop(theArgs);
	}

	internal void InternalOnDragEnter(DragEventArgs theArgs)
	{
		OnDragEnter(theArgs);
	}

	internal void InternalOnDragLeave(EventArgs theArgs)
	{
		OnDragLeave(theArgs);
	}

	internal void InternalOnGiveFeedback(GiveFeedbackEventArgs theArgs)
	{
		OnGiveFeedback(theArgs);
	}

	internal void InternalOnDragOver(DragEventArgs theArgs)
	{
		OnDragOver(theArgs);
	}

	protected void DrawBorder(Graphics theG)
	{
		MultiPanePage designedControl = DesignedControl;
		if (designedControl != null && designedControl.Visible)
		{
			Rectangle clientRectangle = designedControl.ClientRectangle;
			clientRectangle.Width--;
			clientRectangle.Height--;
			theG.DrawRectangle(BorderPen, clientRectangle);
		}
	}

	private MultiPaneControlDesigner GetParentControlDesigner()
	{
		MultiPaneControlDesigner result = null;
		if (Control != null && Control.Parent != null)
		{
			IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				result = (MultiPaneControlDesigner)designerHost.GetDesigner(Control.Parent);
			}
		}
		return result;
	}
}
