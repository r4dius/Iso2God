using System;
using System.Windows.Forms;

namespace Chilano.Iso2God
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Enable visual styles for the application
            Application.EnableVisualStyles();

            // Ensure the text rendering is compatible with the default setting
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);

            // Run the main form of the application
            Application.Run(new Main());
        }
    }
}

