using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAnki.Core.Forms.Controls
{
    public partial class Marker : UserControl
    {
        private Color assignedColor = Color.White;
        public Marker(Color assignedColor)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint, true);

            this.BackColor = Color.Transparent;
            this.Size = new Size(6, 24);
            this.assignedColor = assignedColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(assignedColor))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillRectangle(brush, 0, 0, Width - 1, Height - 1);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTTRANSPARENT = -1;

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
                return;
            }

            base.WndProc(ref m);
        }
    }
}
