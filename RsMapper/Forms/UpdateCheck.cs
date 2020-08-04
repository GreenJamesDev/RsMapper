using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsMapper.Properties;
using System.Windows.Forms;
using System.Net;
using Octokit;
using System.Reflection;
using System.Diagnostics;

namespace RsMapper.Forms
{
    public partial class UpdateCheck : Form
    {
        

        public UpdateCheck()
        {
            InitializeComponent();

        }

        private void UpdateCheck_Load(object sender, EventArgs e)
        {
            
            // The the update void.
            CheckUpdates();
        }

        private async void CheckUpdates()
        {
            // About box to get current version.
            AboutBox1 abt = new AboutBox1();
            
            // Create a new client.
            var client = new GitHubClient(new ProductHeaderValue("RsMapper"));
            try
            {
                // Get all of the releases on the repo.
                var releases = await client.Repository.Release.GetAll("GreenJamesDev", "RsMapper");
                var latest = releases[0]; // Get the latest release.

                // Compare versions.
                if (latest.TagName != abt.AssemblyVersion)
                {
                    // Is a new update.
                    if (MessageBox.Show("An update for RsMapper is available! Would you like to download it?", "Update Found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(latest.HtmlUrl);
                        
                    }
                    // Isn't a new update.
                    else
                    {
                        this.Dispose();
                    }
                }
                else
                {
                    this.Dispose();
                }


            } catch (Exception e)
            {
                this.Dispose();
            }
            this.Dispose();
            
        }
    }
}
