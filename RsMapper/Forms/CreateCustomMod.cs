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
using Newtonsoft.Json;

namespace RsMapper.Forms
{
    public partial class CreateCustomMod : Form
    {

        Image picd1;    // Image associated with component.
        MenuItem delMI; // Delete component menu item.



        public CreateCustomMod()
        {
            InitializeComponent();
        }

        // Export all of the components into a modpack file.
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                // If there are no components in the list view, error.
                MessageBox.Show("The modpack must contain at least one component.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Setup the save file dialog.
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Export Modpack";
                saveFileDialog.Filter = "RsMapper Modpacks|*.rsmp";
                // If the result is ok, begin the modpack creation process. 
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string infox;
                    string typex;
                    string acceptswirex;

                    ModRootObject mro = new ModRootObject();
                    List<modData> data = new List<modData>();

                    try
                    {
                        Directory.CreateDirectory(saveFileDialog.FileName + "_temp\\Imgs");
                        foreach (ListViewItem listViewItem in listView1.Items)
                        {
                            Image img = listView1.LargeImageList.Images[listViewItem.Index];
                            img.Save(saveFileDialog.FileName + "_temp\\Imgs\\" + listViewItem.Text + ".png");

                            // Split the tool tip into useful information for the json file.
                            string[] infoArray = listViewItem.ToolTipText.Split('\n');
                            infox        = infoArray[0];
                            typex        = infoArray[1];
                            acceptswirex = infoArray[2];

                            data.Add(new modData()
                            {
                                name = listViewItem.Text,
                                type = typex,
                                image = "\\Modpacks\\" + Path.GetFileName(saveFileDialog.FileName) + "\\Imgs\\" + listViewItem.Text + ".png",
                                info = infox,
                                acceptswire = acceptswirex
                            });

                        }

                        mro.RsComponents = data.ToArray();

                        // Create the components json for the modpack.
                        string json = JsonConvert.SerializeObject(mro);
                        System.IO.File.WriteAllText(saveFileDialog.FileName + "_temp\\Components.json", json);

                        // Compress the modpack into a .rsmp file.
                        ZipFile.CreateFromDirectory(saveFileDialog.FileName + "_temp", saveFileDialog.FileName);
                        Directory.Delete(saveFileDialog.FileName + "_temp", true);
                    } catch(Exception ex)
                    {
                        MessageBox.Show("Unable to create modpack file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Allow the user to select an image for the component.
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose Texture";
            ofd.Filter = "PNG Files|*.png";
            ofd.Multiselect = false;
            

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string imageFIlePath = ofd.FileName;
                picd1 = Image.FromFile(imageFIlePath);
                nnPictureBox1.Image = picd1;
            }
        }

        // Add the component to the components list of the modpack.
        private void button3_Click(object sender, EventArgs e)
        {
            // Check if all of the required fields have information.
            if(nnPictureBox1.Image == null || textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("One or more fields are missing information.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // Check if there are any duplicate items in the list view.
            } else if (DuplicateListItem(textBox1.Text) == true)
            {
                MessageBox.Show("A component with this name already exists in the modpack.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                
                listView1.LargeImageList.Images.Add(picd1);
                ListViewItem ModItem = new ListViewItem(textBox1.Text, listView1.Items.Count);
                ModItem.ToolTipText = textBox2.Text + "\n" + comboBox1.SelectedItem.ToString() + "\nAccepts Wire: " + checkBox1.Checked;
                listView1.Items.Add(ModItem);
               
            }
        }

        private void CreateCustomMod_Load(object sender, EventArgs e)
        {
            listView1.LargeImageList = new ImageList();

            // Setup the right click menu for the list view.
            ContextMenu cm = new ContextMenu();
            cm.Popup += new EventHandler(this.CmOnPopup);
            delMI = new MenuItem("Delete");
            delMI.Click += new EventHandler(this.DeleteButton_Click);
            cm.MenuItems.Add(delMI);
            listView1.ContextMenu = cm;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                
            }
        }

        // WHen the delete context menu item is pressed.
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            listView1.SelectedItems[0].Remove();
        }

        // If there are no selected items, disable the delete button.
        private void CmOnPopup(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 0)
            {
                delMI.Enabled = false;
            } else
            {
                delMI.Enabled = true;
            }
        }

        bool DuplicateListItem(string text)
        {
            bool duplicates = false;
            foreach(ListViewItem lvi in listView1.Items)
            {
                if(lvi.Text == text)
                {
                    duplicates = true;
                } 
            }
            return duplicates;
        }
    }

    public class modData
    {
        public string name { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public string info { get; set; }
        public string acceptswire { get; set; }
    }

    public class ModRootObject
    {
        [JsonProperty("RsComponents")]
        public modData[] RsComponents { get; set; }
    }
}
