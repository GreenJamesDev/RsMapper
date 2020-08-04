using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace RsMapper.Forms.Controls
{
    public class DrawPanel : Panel
    {
        /// <summary>
        /// Determines whether or not to draw a grid onto the panel.
        /// </summary>
        public bool ShowGrid { get; set; }

        /// <summary>
        /// The pen color of the block grid.
        /// </summary>
        public Color GridColor { get; set; }

        /// <summary>
        /// A list of all blocks on the control.
        /// </summary>
        public ControlCollection Blocks { get; set; }

        public DrawPanel()
        {
           
            
            // Enable double buffering.
            this.DoubleBuffered = true;

            // Set styles.
            this.SetStyle(ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.ResizeRedraw |
              ControlStyles.ContainerControl |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.SupportsTransparentBackColor
              , true);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            foreach(Control c in this.Controls)
            {
                Blocks.Add(c);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(e);
             

            if (ShowGrid == true)
            {
                // Draw a grid.
                Graphics g = e.Graphics;
                Pen pen = new Pen(GridColor);

                for (int y = 0; y < 100; ++y)
                {
                    g.DrawLine(pen, 0, y * 50, 100 * 50, y * 50);
                }

                for (int x = 0; x < 100; ++x)
                {
                    g.DrawLine(pen, x * 50, 0, x * 50, 100 * 50);
                }
            }

            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            
        }
    }
}
