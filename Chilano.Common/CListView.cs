using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Chilano.Common;

public class CListView : ListView
{
	public enum RemoveType
	{
		All,
		Selected,
		Matching
	}

	private struct EmbeddedControl
	{
		public Control Control;

		public ListViewItem Item;

		public DockStyle Dock;

		public int Row;

		public int Col;

		public EmbeddedControl(Control Control, ListViewItem Item, DockStyle Dock, int Row, int Col)
		{
			this.Control = Control;
			this.Item = Item;
			this.Dock = Dock;
			this.Row = Row;
			this.Col = Col;
		}
	}

	private const int LVM_FIRST = 4096;

	private const int LVM_GETCOLUMNORDERARRAY = 4155;

	private const int WM_PAINT = 15;

	private ArrayList _embeddedControls;

	[DefaultValue(View.LargeIcon)]
	public new View View
	{
		get
		{
			return base.View;
		}
		set
		{
			foreach (EmbeddedControl embeddedControl in _embeddedControls)
			{
				embeddedControl.Control.Visible = value == View.Details;
			}
			base.View = value;
		}
	}

	[DllImport("user32.dll")]
	private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wPar, IntPtr lPar);

	public CListView()
	{
		base.FullRowSelect = true;
		base.GridLines = true;
		_embeddedControls = new ArrayList();
	}

	public void Remove(RemoveType Type)
	{
		if (Type == RemoveType.Matching)
		{
			throw new ArgumentException("Must specify a column and type to match.");
		}
		Remove(Type, 0, "");
	}

	public void Remove(RemoveType Type, int Col, string Match)
	{
		switch (Type)
		{
		case RemoveType.All:
			foreach (ListViewItem item in base.Items)
			{
				base.Items.Remove(item);
			}
			break;
		case RemoveType.Selected:
			foreach (ListViewItem selectedItem in base.SelectedItems)
			{
				base.Items.Remove(selectedItem);
			}
			break;
		case RemoveType.Matching:
			foreach (ListViewItem item2 in base.Items)
			{
				if (item2.SubItems[Col].Text == Match)
				{
					base.Items.Remove(item2);
				}
			}
			break;
		}
		for (int i = 0; i < _embeddedControls.Count; i++)
		{
			EmbeddedControl embeddedControl = (EmbeddedControl)_embeddedControls[i];
			embeddedControl.Row = base.Items.IndexOf(embeddedControl.Item);
			_embeddedControls[i] = embeddedControl;
		}
	}

	public void Remove(ListViewItem Item)
	{
		if (_embeddedControls.Count >= Item.Index)
		{
			EmbeddedControl embeddedControl = (EmbeddedControl)_embeddedControls[Item.Index];
			RemoveEmbeddedControl(Item);
		}
		base.Items.Remove(Item);
		for (int i = 0; i < _embeddedControls.Count; i++)
		{
			EmbeddedControl embeddedControl2 = (EmbeddedControl)_embeddedControls[i];
			embeddedControl2.Row = base.Items.IndexOf(embeddedControl2.Item);
			_embeddedControls[i] = embeddedControl2;
		}
	}

	public void SelectAll()
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			base.Items[i].Selected = true;
		}
	}

	public void SelectNone()
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			base.Items[i].Selected = false;
		}
	}

	public void SelectInverse()
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			base.Items[i].Selected = !base.Items[i].Selected;
		}
	}

	public void SelectSimilar(int column)
	{
		if (base.SelectedItems.Count <= 0)
		{
			return;
		}
		string text = base.SelectedItems[0].SubItems[column].Text;
		for (int i = 0; i < base.Items.Count; i++)
		{
			if (base.Items[i].SubItems[column].Text == text)
			{
				base.Items[i].Selected = true;
			}
		}
	}

	public void CheckAll()
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			base.Items[i].Checked = true;
		}
	}

	public void CheckNone()
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			base.Items[i].Checked = false;
		}
	}

	public void CheckInverse()
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			base.Items[i].Checked = !base.Items[i].Checked;
		}
	}

	protected int[] GetColumnOrder()
	{
		IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * base.Columns.Count);
		if (SendMessage(base.Handle, 4155, new IntPtr(base.Columns.Count), intPtr).ToInt32() == 0)
		{
			Marshal.FreeHGlobal(intPtr);
			return null;
		}
		int[] array = new int[base.Columns.Count];
		Marshal.Copy(intPtr, array, 0, base.Columns.Count);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	protected Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
	{
		Rectangle empty = Rectangle.Empty;
		if (Item == null)
		{
			throw new ArgumentNullException("Item");
		}
		int[] columnOrder = GetColumnOrder();
		if (columnOrder == null)
		{
			return empty;
		}
		if (SubItem >= columnOrder.Length)
		{
			throw new IndexOutOfRangeException("SubItem " + SubItem + " out of range");
		}
		Rectangle bounds = Item.GetBounds(ItemBoundsPortion.Entire);
		int num = bounds.Left;
		int i;
		for (i = 0; i < columnOrder.Length; i++)
		{
			ColumnHeader columnHeader = base.Columns[columnOrder[i]];
			if (columnHeader.Index == SubItem)
			{
				break;
			}
			num += columnHeader.Width;
		}
		return new Rectangle(num, bounds.Top, base.Columns[columnOrder[i]].Width, bounds.Height);
	}

	public void AddEmbeddedControl(Control c, int col, int row)
	{
		AddEmbeddedControl(c, col, row, DockStyle.Fill);
	}

	public void AddEmbeddedControl(Control c, int col, int row, DockStyle dock)
	{
		if (c == null)
		{
			throw new ArgumentNullException();
		}
		if (col >= base.Columns.Count || row >= base.Items.Count)
		{
			throw new ArgumentOutOfRangeException();
		}
		EmbeddedControl embeddedControl = default(EmbeddedControl);
		embeddedControl.Control = c;
		embeddedControl.Col = col;
		embeddedControl.Row = row;
		embeddedControl.Dock = dock;
		embeddedControl.Item = base.Items[row];
		_embeddedControls.Add(embeddedControl);
		c.Click += _embeddedControl_Click;
		base.Controls.Add(c);
	}

	public void RemoveEmbeddedControl(ListViewItem l)
	{
		for (int i = 0; i < _embeddedControls.Count; i++)
		{
			if (((EmbeddedControl)_embeddedControls[i]).Item == l)
			{
				RemoveEmbeddedControl(((EmbeddedControl)_embeddedControls[i]).Control);
			}
		}
	}

	public void RemoveEmbeddedControl(Control c)
	{
		if (c == null)
		{
			return;
		}
		for (int i = 0; i < _embeddedControls.Count; i++)
		{
			if (((EmbeddedControl)_embeddedControls[i]).Control == c)
			{
				c.Click -= _embeddedControl_Click;
				base.Controls.Remove(c);
				_embeddedControls.RemoveAt(i);
				return;
			}
		}
		throw new Exception("Control not found!");
	}

	public Control GetEmbeddedControl(int col, int row)
	{
		foreach (EmbeddedControl embeddedControl in _embeddedControls)
		{
			if (embeddedControl.Row == row && embeddedControl.Col == col)
			{
				return embeddedControl.Control;
			}
		}
		return null;
	}

	protected override void WndProc(ref Message m)
	{
		int msg = m.Msg;
		if (msg == 15 && View == View.Details)
		{
			foreach (EmbeddedControl embeddedControl in _embeddedControls)
			{
				Rectangle subItemBounds = GetSubItemBounds(embeddedControl.Item, embeddedControl.Col);
				if (base.HeaderStyle != 0 && subItemBounds.Top < Font.Height)
				{
					embeddedControl.Control.Visible = false;
					continue;
				}
				embeddedControl.Control.Visible = true;
				switch (embeddedControl.Dock)
				{
				case DockStyle.Top:
					subItemBounds.Height = embeddedControl.Control.Height;
					break;
				case DockStyle.Left:
					subItemBounds.Width = embeddedControl.Control.Width;
					break;
				case DockStyle.Bottom:
					subItemBounds.Offset(0, subItemBounds.Height - embeddedControl.Control.Height);
					subItemBounds.Height = embeddedControl.Control.Height;
					break;
				case DockStyle.Right:
					subItemBounds.Offset(subItemBounds.Width - embeddedControl.Control.Width, 0);
					subItemBounds.Width = embeddedControl.Control.Width;
					break;
				case DockStyle.None:
					subItemBounds.Size = embeddedControl.Control.Size;
					break;
				}
				embeddedControl.Control.Bounds = subItemBounds;
			}
		}
		base.WndProc(ref m);
	}

	private void _embeddedControl_Click(object sender, EventArgs e)
	{
		foreach (EmbeddedControl embeddedControl in _embeddedControls)
		{
			if (embeddedControl.Control == (Control)sender)
			{
				base.SelectedItems.Clear();
				embeddedControl.Item.Selected = true;
			}
		}
	}
}
