namespace Chilano.Xbox360.Iso;

public class AVLTree
{
    public GDFEntryNode Root;

    public void Insert(GDFDirEntry Value)
    {
        if (Root == null)
        {
            Root = new GDFEntryNode(Value);
        }
        else
        {
            GDFEntryNode.Insert(Root, Value);
        }
        while (Root.Parent != null)
        {
            Root = Root.Parent;
        }
    }

    public override string ToString()
    {
        string text = "";
        if (Root != null)
        {
            object obj = text;
            text = string.Concat(obj, "Root = ", Root.Key, ",");
            if (Root.Left != null)
            {
                object obj2 = text;
                text = string.Concat(obj2, "Root.Left = ", Root.Left.Key, ",");
            }
            if (Root.Right != null)
            {
                text = text + "Root.Right = " + Root.Right.Key;
            }
        }
        return text;
    }
}
