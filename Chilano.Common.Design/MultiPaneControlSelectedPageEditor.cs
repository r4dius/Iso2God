using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Chilano.Common.Design;

internal class MultiPaneControlSelectedPageEditor : ObjectSelectorEditor
{
    protected override void FillTreeWithData(Selector theSel, ITypeDescriptorContext theCtx, IServiceProvider theProvider)
    {
        base.FillTreeWithData(theSel, theCtx, theProvider);
        MultiPaneControl multiPaneControl = (MultiPaneControl)theCtx.Instance;
        foreach (MultiPanePage control in multiPaneControl.Controls)
        {
            SelectorNode selectorNode = new SelectorNode(control.Name, control);
            theSel.Nodes.Add(selectorNode);
            if (control == multiPaneControl.SelectedPage)
            {
                theSel.SelectedNode = selectorNode;
            }
        }
    }
}
