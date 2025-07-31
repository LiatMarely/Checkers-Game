
using System;
using System.Windows.Forms;
using Ex05_Liat_207918608.FormUI;

namespace Ex05_Liat_207918608.FormUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

