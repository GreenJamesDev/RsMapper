using RsMapper.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace RsMapper
{
    static class Program
    {
        public static bool NoJson = false;
        public static bool CheckUpdates = true;
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RsMapper";

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

            
            } 
            else
            {
                foreach (String arg in args)
                {
                    switch (arg)
                    {
                        
                        // If the runtime argument --disable-json-check
                        // is passed, tell RsMapper to ignore the
                        // Components.json file.
                        case "--disable-json-check":
                            NoJson = true;
                            break;

                        // If the runtime argument --disable-update-check
                        // is passed, tell RsMapper to skip running
                        // the update checker.
                        case "--disable-update-check":
                            CheckUpdates = false;
                            break;

                        // If a proper argument isn't sent.
                        default:

                            // If RsMapper is used to open a modpack file.
                            if (arg.Contains(":\\")){
                                
                                try
                                {
                                    ZipFile.ExtractToDirectory(arg, AppData + "\\Modpacks\\" + Path.GetFileName(arg));
                                    MessageBox.Show("Modpack installed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Modpack installation failed or invalid command line argument.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                
                            }

                            // Check for missing files.
                            PrgmSelfCheck sc = new PrgmSelfCheck();
                            sc.CheckAll();
                            break;
                    }
                }
                
            }

            if (CheckUpdates == true)
            {
                // Create a new update thread.
                Thread thread = new Thread(RunUpdateCheck);
                thread.Start();
            }

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
