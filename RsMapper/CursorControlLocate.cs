using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using RsMapper.Forms.Controls;

namespace RsMapper
{
    class CursorControlLocate
    {
        public static Control FindControlAtPoint(Control container, Point pos)
        {
            Control child;
            foreach (Control c in container.Controls)
            {
                if (c.Visible && c.Bounds.Contains(pos))
                {
                    child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
                    if (child == null) return c;
                    else return child;
                }
            }
            return null;
        }

        public static Control FindControlAtCursor(DrawPanel dp)
        {
            Point pos = Cursor.Position;
            if (dp.Bounds.Contains(pos))
                return FindControlAtPoint(dp, dp.PointToClient(pos));
            return null;
        }
    }
}
