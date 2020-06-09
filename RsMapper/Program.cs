using RsMapper.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsMapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check for missing files.
            PrgmSelfCheck sc = new PrgmSelfCheck();
            sc.CheckAll();

            // Create a new update thread.
            Thread thread = new Thread(RunUpdateCheck);
            thread.Start();
            
            // Run the main form.
            Application.Run(new Form1());
        }

        public static void RunUpdateCheck()
        {
            UpdateCheck uc = new UpdateCheck();
            uc.ShowDialog();
            uc.Dispose();
        }
    }
}
