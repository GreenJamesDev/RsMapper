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
        public static string ComponentsJson = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"RsMapper\\Components.json");
        public static string JsonNet =        "Newtonsoft.Json.dll";
        public static string JsonNetXml =     "Newtonsoft.Json.xml";
        

        WebClient wc = new WebClient();

        /// <summary>
        /// Check for any missing files that are required to start the program.
        /// </summary>
        public async void CheckAll()
        {
            
            
            if (File.Exists(ComponentsJson) == false)
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RsMapper"));
                DownloadProgress downloadProgress = new DownloadProgress("https://raw.githubusercontent.com/GreenJamesDev/RsMapper/master/RsMapper/Components.json", ComponentsJson);
                downloadProgress.ShowDialog();
                downloadProgress.Dispose();
                

            }

        }
        

    }
}
