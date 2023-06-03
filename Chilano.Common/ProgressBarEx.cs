using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;

namespace Chilano.Common
{
    public enum ProgressBarDisplayText
    {
        Percentage,
        Text
    }

    class ProgressBarEx : ProgressBar
    {
        //Property to set to decide whether to print a % or Text
        public ProgressBarDisplayText DisplayStyle { get; set; }

        //Property to hold the custom text
        public String Text { get; set; }

        public ProgressBarEx()
        {
            // Modify the ControlStyles flags
            //http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            rect.Inflate(-1, -1);
            if (Value > 0)
            {
                // As we doing this ourselves we need to draw the chunks on the progress bar
                Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                ProgressBarRenderer.DrawHorizontalChunks(g, clip);
            }

            // Set the Display text (Either a % amount or our custom text
            int percent = (int)(((double)this.Value / (double)this.Maximum) * 100);
            if (Text != null)
            {
                string text = DisplayStyle == ProgressBarDisplayText.Percentage ? percent.ToString() + '%' : Text;

                using (Font f = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))))
                {

                    //this will center align our text at the bottom of the image
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Far;

                    //pen for outline - set width parameter
                    Pen outline = new Pen(Color.FromArgb(180, Color.Black), 2);
                    outline.LineJoin = LineJoin.Round; //prevent "spikes" at the path

                    SolidBrush color = new SolidBrush(ColorTranslator.FromHtml("#FFFFFF"));

                    //this will be the rectangle used to draw and auto-wrap the text.
                    //basically = image size
                    Rectangle textrect = new Rectangle(0, -1, rect.Width, rect.Height);

                    GraphicsPath gp = new GraphicsPath();

                    //look mom! no pre-wrapping!
                    gp.AddString(text, f.FontFamily, (int)f.Style, g.DpiY * f.Size / 72, textrect, format);

                    //these affect lines such as those in paths. Textrenderhint doesn't affect
                    //text in a path as it is converted to ..well, a path.    
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.Default;

                    g.DrawPath(outline, gp);
                    g.FillPath(color, gp);
                }
            }
        }
    }
}
