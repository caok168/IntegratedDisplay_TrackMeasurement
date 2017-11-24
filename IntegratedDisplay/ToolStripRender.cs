using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IntegratedDisplay
{
    public class ToolStripRender:ToolStripProfessionalRenderer
    {
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
            var item = e.Item as ToolStripButton;
            if (item != null && item.Checked)
            {
                ControlPaint.DrawBorder(e.Graphics, new Rectangle(Point.Empty, e.Item.Size), Color.Red, ButtonBorderStyle.Solid);
            }
        }

    }
}
