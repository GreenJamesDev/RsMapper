using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

namespace RsMapper.Forms
{
    public partial class DownloadProgress : Form
    {

        WebClient wc = new WebClient();
        string source;
        string local;

        public DownloadProgress(string downloadSrc, string localFile)
        {
            InitializeComponent();
            source = downloadSrc;
            local = localFile;
        }

        private void DownloadProgress_Load(object sender, EventArgs e)
        {
            try
            {
                // Reinstall the file if the user clicks yes.
                wc.DownloadFileAsync(new Uri(source), local);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unable to download file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            wc.Dispose();
            this.Close();
            this.Dispose();

        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Downloading: " + e.ProgressPercentage + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wc.CancelAsync();
            Application.Exit();
        }
    }
}
