using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Forms;
using RsMapper.Forms;

namespace RsMapper
{
    public class PrgmSelfCheck
    {
        // DEPENDANCIES
        public static string ComponentsJson = "Components.json";
        public static string JsonNet =        "Newtonsoft.Json.dll";
        public static string JsonNetXml =     "Newtonsoft.Json.xml";
        

        WebClient wc = new WebClient();

        /// <summary>
        /// Check for any missing files that are required to start the program.
        /// </summary>
        public void CheckAll()
        {
            UpdateCheck uc = new UpdateCheck();
            uc.ShowDialog();
            
            if (File.Exists(ComponentsJson) == false)
            {

                // Check for components settings file.
                if(MessageBox.Show("The file " + ComponentsJson + " is missing. Would you like to redownload it?", "Missing Dependancy", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {

                    DownloadProgress downloadProgress = new DownloadProgress("https://raw.githubusercontent.com/GreenJamesDev/RsMapper/master/RsMapper/Components.json", ComponentsJson);
                    downloadProgress.ShowDialog();

                } else
                {

                    // If the user chooses not to reinstall the file, exit RsMapper.
                    Application.Exit();
                }

            }

            uc.Dispose();

        }
        

    }
}
