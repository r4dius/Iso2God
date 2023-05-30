using System.Windows.Forms;

namespace Chilano.Common;

public class CTextLog : RichTextBox
{
    public CTextLog()
    {
        Multiline = true;
        base.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
    }

    public void Add(string LogMessage)
    {
        Text = Text + LogMessage + "\n";
        ScrollToCaret();
    }
}
