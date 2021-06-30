using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsMapper.Forms.Controls
{
    public class NNPictureBox : PictureBox
    {
        /// <summary>
        /// The name of the block associated with the control.
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// Extra block info.
        /// </summary>
        public string ComponentTag { get; set; }

        /// <summary>
        /// List to add the component to after deletion
        /// for undoing.
        /// </summary>
        public List<PictureBox> RedoList;

        /// <summary>
        /// Determines whether or not the user can interact with the control.
        /// </summary>
        public bool IsInteractable;

        public ToolStripMenuItem redoMItem;

        public NNPictureBox()
        {

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Use nearest neighbor when zooming and get rid of any half-pixel offset.
            pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            base.OnPaint(pe);
            
            Graphics g = pe.Graphics;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            if (IsInteractable == true)
            {
                ToolTip tt = new ToolTip();

                tt.SetToolTip(this, ComponentName + "\n" + ComponentTag);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Delete component.
            base.OnMouseDown(e);
            if (IsInteractable == true)
            {
                if (e.Button == MouseButtons.Middle)
                {
                    DeleteComponent();
                }
            }
        }

        /// <summary>
        /// Delete the component and add it to the redo list.
        /// </summary>
        void DeleteComponent()
        {
            Visible = false;
            RedoList.Add(this);
            redoMItem.Enabled = true;
        }
    }
}
