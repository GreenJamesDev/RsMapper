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
    class NNPictureBox : PictureBox
    {

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
    }
}
