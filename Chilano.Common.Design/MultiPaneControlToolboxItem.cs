using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Chilano.Common.Design;

[Serializable]
[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
public class MultiPaneControlToolboxItem : ToolboxItem
{
    public MultiPaneControlToolboxItem()
        : base(typeof(MultiPaneControl))
    {
    }

    public MultiPaneControlToolboxItem(SerializationInfo theInfo, StreamingContext theContext)
    {
        Deserialize(theInfo, theContext);
    }

    protected override IComponent[] CreateComponentsCore(IDesignerHost theHost)
    {
        return DesignerTransactionUtility.DoInTransaction(theHost, "MultiPaneControlTooblxItem_CreateControl", CreateControlWithOnePage, null) as IComponent[];
    }

    public object CreateControlWithOnePage(IDesignerHost theHost, object theParam)
    {
        MultiPaneControl multiPaneControl = (MultiPaneControl)theHost.CreateComponent(typeof(MultiPaneControl));
        MultiPanePage value = (MultiPanePage)theHost.CreateComponent(typeof(MultiPanePage));
        multiPaneControl.Controls.Add(value);
        return new IComponent[1] { multiPaneControl };
    }
}
