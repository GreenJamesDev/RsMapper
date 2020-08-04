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
        public static bool NoJson = false;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
            {
                // Check for missing files.
                PrgmSelfCheck sc = new PrgmSelfCheck();
                sc.CheckAll();

            
            } else if (args[0] == "--disable-json-check")
            {
                // If the runtime argument --disable-update-check
                // is passed, tell RsMapper to ignore the
                // Components.json file.
                NoJson = true;

            }
            else
            {
                // Check for missing files.
                PrgmSelfCheck sc = new PrgmSelfCheck();
                sc.CheckAll();


            }
           
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
