using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Chilano.Common.Design;

[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
public class MultiPaneControlDesigner : ParentControlDesigner
{
	private DesignerVerbCollection myVerbs;

	private DesignerVerb myRemoveVerb;

	private DesignerVerb myAddVerb;

	private DesignerVerb mySwitchVerb;

	private MultiPanePage mySelectedPage;

	private bool myInTransaction = false;

	public override DesignerVerbCollection Verbs => myVerbs;

	public MultiPaneControl DesignedControl => (MultiPaneControl)Control;

	public MultiPanePage DesignerSelectedPage
	{
		get
		{
			return mySelectedPage;
		}
		set
		{
			if (mySelectedPage != null)
			{
				mySelectedPage.Visible = false;
			}
			mySelectedPage = value;
			if (mySelectedPage != null)
			{
				mySelectedPage.Visible = true;
			}
		}
	}

	private void CheckVerbStatus()
	{
		if (Control == null)
		{
			DesignerVerb designerVerb = myRemoveVerb;
			DesignerVerb designerVerb2 = myAddVerb;
			bool flag2 = (mySwitchVerb.Enabled = false);
			flag2 = (designerVerb2.Enabled = flag2);
			designerVerb.Enabled = flag2;
		}
		else
		{
			myAddVerb.Enabled = true;
			myRemoveVerb.Enabled = Control.Controls.Count > 1;
			mySwitchVerb.Enabled = Control.Controls.Count > 1;
		}
	}

	private MultiPanePageDesigner GetSelectedPageDesigner()
	{
		MultiPanePage multiPanePage = mySelectedPage;
		if (multiPanePage == null)
		{
			return null;
		}
		MultiPanePageDesigner result = null;
		IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
		if (designerHost != null)
		{
			result = (MultiPanePageDesigner)designerHost.GetDesigner(multiPanePage);
		}
		return result;
	}

	private static MultiPanePage GetPageOfControl(object theControl)
	{
		if (!(theControl is Control))
		{
			return null;
		}
		Control control = (Control)theControl;
		while (control != null && !(control is MultiPanePage))
		{
			control = control.Parent;
		}
		return (MultiPanePage)control;
	}

	private object Transaction_AddPage(IDesignerHost theHost, object theParam)
	{
		MultiPaneControl designedControl = DesignedControl;
		MultiPanePage multiPanePage = (MultiPanePage)theHost.CreateComponent(typeof(MultiPanePage));
		MemberDescriptor member = TypeDescriptor.GetProperties(designedControl)["Controls"];
		RaiseComponentChanging(member);
		designedControl.Controls.Add(multiPanePage);
		DesignerSelectedPage = multiPanePage;
		RaiseComponentChanged(member, null, null);
		return null;
	}

	private object Transaction_RemovePage(IDesignerHost theHost, object theParam)
	{
		if (mySelectedPage == null)
		{
			return null;
		}
		MultiPaneControl designedControl = DesignedControl;
		MemberDescriptor member = TypeDescriptor.GetProperties(designedControl)["Controls"];
		RaiseComponentChanging(member);
		try
		{
			theHost.DestroyComponent(mySelectedPage);
		}
		catch
		{
		}
		RaiseComponentChanged(member, null, null);
		return null;
	}

	private object Transaction_UpdateSelectedPage(IDesignerHost theHost, object theParam)
	{
		MultiPaneControl designedControl = DesignedControl;
		MultiPanePage multiPanePage = mySelectedPage;
		int num = designedControl.Controls.IndexOf(mySelectedPage);
		if (mySelectedPage == designedControl.SelectedPage)
		{
			MemberDescriptor member = TypeDescriptor.GetProperties(designedControl)["SelectedPage"];
			RaiseComponentChanging(member);
			if (designedControl.Controls.Count > 1)
			{
				if (num == designedControl.Controls.Count - 1)
				{
					designedControl.SelectedPage = (MultiPanePage)designedControl.Controls[num - 1];
				}
				else
				{
					designedControl.SelectedPage = (MultiPanePage)designedControl.Controls[num + 1];
				}
			}
			else
			{
				designedControl.SelectedPage = null;
			}
			RaiseComponentChanged(member, null, null);
		}
		else if (designedControl.Controls.Count > 1)
		{
			if (num == designedControl.Controls.Count - 1)
			{
				DesignerSelectedPage = (MultiPanePage)designedControl.Controls[num - 1];
			}
			else
			{
				DesignerSelectedPage = (MultiPanePage)designedControl.Controls[num + 1];
			}
		}
		else
		{
			DesignerSelectedPage = null;
		}
		return null;
	}

	private object Transaction_SetSelectedPageAsConcrete(IDesignerHost theHost, object theParam)
	{
		MultiPaneControl designedControl = DesignedControl;
		MemberDescriptor member = TypeDescriptor.GetProperties(designedControl)["SelectedPage"];
		RaiseComponentChanging(member);
		designedControl.SelectedPage = (MultiPanePage)theParam;
		RaiseComponentChanged(member, null, null);
		return null;
	}

	private void Handler_SelectedPageChanged(object theSender, EventArgs theArgs)
	{
		mySelectedPage = DesignedControl.SelectedPage;
	}

	private void Handler_AddPage(object theSender, EventArgs theArgs)
	{
		IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
		if (designerHost != null)
		{
			myInTransaction = true;
			DesignerTransactionUtility.DoInTransaction(designerHost, "MultiPaneControlAddPage", Transaction_AddPage, null);
			myInTransaction = false;
		}
	}

	private void Handler_RemovePage(object sender, EventArgs eevent)
	{
		MultiPaneControl designedControl = DesignedControl;
		if (designedControl != null && designedControl.Controls.Count >= 1)
		{
			IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				myInTransaction = true;
				DesignerTransactionUtility.DoInTransaction(designerHost, "MultiPaneControlRemovePage", Transaction_RemovePage, null);
				myInTransaction = false;
			}
		}
	}

	private void Handler_SwitchPage(object theSender, EventArgs theArgs)
	{
		frmSwitchPages frmSwitchPages2 = new frmSwitchPages(this);
		DialogResult dialogResult = frmSwitchPages2.ShowDialog();
		if (dialogResult != DialogResult.OK)
		{
			return;
		}
		if (frmSwitchPages2.SetSelectedPage)
		{
			IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				DesignerTransactionUtility.DoInTransaction(designerHost, "MultiPaneControlSetSelectedPageAsConcrete", Transaction_SetSelectedPageAsConcrete, frmSwitchPages2.FutureSelection);
			}
		}
		else
		{
			DesignerSelectedPage = frmSwitchPages2.FutureSelection;
		}
	}

	private void Handler_ComponentChanged(object theSender, ComponentChangedEventArgs theArgs)
	{
		CheckVerbStatus();
	}

	private void Handler_ComponentRemoving(object theSender, ComponentEventArgs theArgs)
	{
		if (!(theArgs.Component is MultiPanePage))
		{
			return;
		}
		MultiPaneControl designedControl = DesignedControl;
		MultiPanePage control = (MultiPanePage)theArgs.Component;
		if (designedControl.Controls.Contains(control))
		{
			IDesignerHost theHost = (IDesignerHost)GetService(typeof(IDesignerHost));
			if (!myInTransaction)
			{
				myInTransaction = true;
				DesignerTransactionUtility.DoInTransaction(theHost, "MultiPaneControlRemoveComponent", Transaction_UpdateSelectedPage, null);
				myInTransaction = false;
			}
			else
			{
				Transaction_UpdateSelectedPage(theHost, null);
			}
			CheckVerbStatus();
		}
	}

	private void Handler_SelectionChanged(object sender, EventArgs e)
	{
		ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
		if (selectionService == null)
		{
			return;
		}
		ICollection selectedComponents = selectionService.GetSelectedComponents();
		MultiPaneControl designedControl = DesignedControl;
		foreach (object item in selectedComponents)
		{
			MultiPanePage pageOfControl = GetPageOfControl(item);
			if (pageOfControl != null && pageOfControl.Parent == designedControl)
			{
				DesignerSelectedPage = pageOfControl;
				break;
			}
		}
	}

	protected override void OnDragDrop(DragEventArgs theDragEvents)
	{
		GetSelectedPageDesigner()?.InternalOnDragDrop(theDragEvents);
	}

	protected override void OnDragEnter(DragEventArgs theDragEvents)
	{
		GetSelectedPageDesigner()?.InternalOnDragEnter(theDragEvents);
	}

	protected override void OnDragLeave(EventArgs theArgs)
	{
		GetSelectedPageDesigner()?.InternalOnDragLeave(theArgs);
	}

	protected override void OnDragOver(DragEventArgs theDragEvents)
	{
		MultiPaneControl designedControl = DesignedControl;
		Point pt = designedControl.PointToClient(new Point(theDragEvents.X, theDragEvents.Y));
		if (!designedControl.DisplayRectangle.Contains(pt))
		{
			theDragEvents.Effect = DragDropEffects.None;
		}
		else
		{
			GetSelectedPageDesigner()?.InternalOnDragOver(theDragEvents);
		}
	}

	protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
	{
		GetSelectedPageDesigner()?.InternalOnGiveFeedback(e);
	}

	public override void Initialize(IComponent theComponent)
	{
		base.Initialize(theComponent);
		ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
		if (selectionService != null)
		{
			selectionService.SelectionChanged += Handler_SelectionChanged;
		}
		IComponentChangeService componentChangeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
		if (componentChangeService != null)
		{
			componentChangeService.ComponentRemoving += Handler_ComponentRemoving;
			componentChangeService.ComponentChanged += Handler_ComponentChanged;
		}
		DesignedControl.SelectedPageChanged += Handler_SelectedPageChanged;
		myAddVerb = new DesignerVerb("Add page", Handler_AddPage);
		myRemoveVerb = new DesignerVerb("Remove page", Handler_RemovePage);
		mySwitchVerb = new DesignerVerb("Switch pages...", Handler_SwitchPage);
		myVerbs = new DesignerVerbCollection();
		myVerbs.AddRange(new DesignerVerb[3] { myAddVerb, myRemoveVerb, mySwitchVerb });
	}

	protected override void Dispose(bool theDisposing)
	{
		if (theDisposing)
		{
			ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SelectionChanged -= Handler_SelectionChanged;
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRemoving -= Handler_ComponentRemoving;
				componentChangeService.ComponentChanged -= Handler_ComponentChanged;
			}
			DesignedControl.SelectedPageChanged -= Handler_SelectedPageChanged;
		}
		base.Dispose(theDisposing);
	}

	public override bool CanParent(Control theControl)
	{
		if (theControl is MultiPanePage)
		{
			return !Control.Contains(theControl);
		}
		return false;
	}
}
