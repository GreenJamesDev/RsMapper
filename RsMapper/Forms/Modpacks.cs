using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace RsMapper.Forms
{
    public partial class Modpacks : Form
    {

        string AppData;
        

        public Modpacks(bool modpackOpen = false, string path = null)
        {
            InitializeComponent();
            AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RsMapper";

            
        }

        private void Modpacks_Load(object sender, EventArgs e)
        {
            LoadModList();
        }

        // Close and dispose the form.
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        // Delete selected modpack.
        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to remove this modpack? You can always reinstall it from its .rsmp file.", "Remove Modpack", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                foreach(ListViewItem lvi in listView1.SelectedItems)
                {
                    Directory.Delete(lvi.Text, true);
                }

                // Reload the modpacks list.
                listView1.Clear();
                LoadModList();
            }
        }

        // Load the installed modpacks into the list view.
        void LoadModList()
        {
            foreach (string directory in Directory.GetDirectories(AppData + "\\Modpacks"))
            {
                listView1.Items.Add(new ListViewItem(directory));
            }
        }

        // Install a modpack.
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Modpack File";
            ofd.Filter = "RsMapper Modpacks|*.rsmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ZipFile.ExtractToDirectory(ofd.FileName, AppData + "\\Modpacks\\" + Path.GetFileName(ofd.FileName));
                } catch(Exception ex)
                {
                    MessageBox.Show("Modpack installation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            listView1.Clear();
            LoadModList();
        }
    }
}
