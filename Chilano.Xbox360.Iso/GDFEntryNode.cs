namespace Chilano.Xbox360.Iso;

public class GDFEntryNode
{
	public enum TreeNodeBalance
	{
		LeftLeft,
		LeftRight,
		RightRight,
		RightLeft,
		Balanced
	}

	public GDFEntryNode Parent;

	public GDFEntryNode Left;

	public GDFEntryNode Right;

	public GDFDirEntry Key;

	public GDFEntryNode()
	{
	}

	public GDFEntryNode(GDFEntryNode Parent)
	{
		this.Parent = Parent;
	}

	public GDFEntryNode(GDFDirEntry Key)
	{
		this.Key = Key;
	}

	public static void Insert(GDFEntryNode Root, GDFDirEntry NewKey)
	{
		if (Root.Key == null)
		{
			Root.Key = NewKey;
		}
		else
		{
			int num = Root.Key.Name.CompareTo(NewKey.Name);
			if (num > 0)
			{
				if (Root.Left == null)
				{
					Root.Left = new GDFEntryNode(Root);
				}
				Insert(Root.Left, NewKey);
			}
			else if (num < 0)
			{
				if (Root.Right == null)
				{
					Root.Right = new GDFEntryNode(Root);
				}
				Insert(Root.Right, NewKey);
			}
		}
		if (Root.Parent != null && Root.Parent.Parent != null)
		{
			Rebalance(Root.Parent.Parent, Root.Parent, GetBalance(Root));
		}
	}

	public static TreeNodeBalance GetBalance(GDFEntryNode Root)
	{
		int num = 0;
		if (Root.Parent != null)
		{
			if (Root.Parent.Left != null)
			{
				num++;
			}
			if (Root.Parent.Right != null)
			{
				num--;
			}
			if (Root.Parent.Parent != null)
			{
				if (num > 0)
				{
					if (Root.Parent.Parent.Left != null && Root.Parent.Parent.Right == null)
					{
						return TreeNodeBalance.LeftLeft;
					}
					if (Root.Parent.Parent.Right != null && Root.Parent.Parent.Left == null)
					{
						return TreeNodeBalance.RightLeft;
					}
				}
				if (num < 0)
				{
					if (Root.Parent.Parent.Left != null && Root.Parent.Parent.Right == null)
					{
						return TreeNodeBalance.LeftRight;
					}
					if (Root.Parent.Parent.Right != null && Root.Parent.Parent.Left == null)
					{
						return TreeNodeBalance.RightRight;
					}
				}
			}
		}
		return TreeNodeBalance.Balanced;
	}

	public static GDFEntryNode Rebalance(GDFEntryNode Root, GDFEntryNode Pivot, TreeNodeBalance Balance)
	{
		return Balance switch
		{
			TreeNodeBalance.RightRight => RotateLeft(Root, Pivot), 
			TreeNodeBalance.RightLeft => RotateRightLeft(Root, Pivot), 
			TreeNodeBalance.LeftLeft => RotateRight(Root, Pivot), 
			TreeNodeBalance.LeftRight => RotateLeftRight(Root, Pivot), 
			_ => null, 
		};
	}

	private static GDFEntryNode RotateRight(GDFEntryNode Root, GDFEntryNode Pivot)
	{
		Root.Left = null;
		Pivot.Right = Root;
		if (Root.Parent == null)
		{
			Pivot.Parent = null;
			Root.Parent = Pivot;
		}
		else
		{
			if (Root.Parent.Right == Root)
			{
				Root.Parent.Right = Pivot;
			}
			else
			{
				Root.Parent.Left = Pivot;
			}
			Root.Parent = Pivot;
		}
		return Pivot;
	}

	private static GDFEntryNode RotateLeft(GDFEntryNode Root, GDFEntryNode Pivot)
	{
		Root.Right = null;
		Pivot.Left = Root;
		if (Root.Parent == null)
		{
			Pivot.Parent = null;
			Root.Parent = Pivot;
		}
		else
		{
			if (Root.Parent.Right == Root)
			{
				Root.Parent.Right = Pivot;
			}
			else
			{
				Root.Parent.Left = Pivot;
			}
			Root.Parent = Pivot;
		}
		return Pivot;
	}

	private static GDFEntryNode RotateLeftRight(GDFEntryNode Root, GDFEntryNode Pivot)
	{
		GDFEntryNode right = Pivot.Right;
		Root.Left = right.Right;
		Pivot.Right = right.Left;
		right.Left = Pivot;
		right.Right = Root;
		if (Root.Parent == null)
		{
			right.Parent = null;
			Root.Parent = right;
			Pivot.Parent = right;
		}
		else
		{
			right.Parent = Root.Parent;
			if (Root.Parent.Right == Root)
			{
				Root.Parent.Right = right;
			}
			else
			{
				Root.Parent.Left = right;
			}
			Root.Parent = right;
			Pivot.Parent = right;
		}
		return right;
	}

	private static GDFEntryNode RotateRightLeft(GDFEntryNode Root, GDFEntryNode Pivot)
	{
		GDFEntryNode left = Pivot.Left;
		Pivot.Left = left.Right;
		Root.Right = left.Left;
		left.Right = Pivot;
		left.Left = Root;
		if (Root.Parent == null)
		{
			left.Parent = null;
			Root.Parent = left;
			Pivot.Parent = left;
		}
		else
		{
			left.Parent = Root.Parent;
			if (Root.Parent.Right == Root)
			{
				Root.Parent.Right = left;
			}
			else
			{
				Root.Parent.Left = left;
			}
			Root.Parent = left;
			Pivot.Parent = left;
		}
		return left;
	}
}
