using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2P_Chat.Controls
{
    public class HSeparator : Control
    {
        private Color lineColor;
        private Pen linePen;
        private int lineWidth;

        public int LineWidth
        {
            get
            {
                return this.lineWidth;
            }
            set
            {
                this.lineWidth = value;
                this.linePen = new Pen(this.lineColor, this.lineWidth)
                {
                    Alignment = PenAlignment.Inset
                };
                Refresh();
            }
        }

        public HSeparator()
        {
            this.lineColor = Color.LightGray;
            this.lineWidth = 1;
            this.linePen = new Pen(this.lineColor, this.lineWidth);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        public Color LineColor
        {
            get
            {
                return this.lineColor;
            }
            set
            {
                this.lineColor = value;
                this.linePen = new Pen(this.lineColor, this.lineWidth)
                {
                    Alignment = PenAlignment.Inset
                };
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graph = e.Graphics;
            int x = this.Height / 2;
            graph.DrawLine(linePen, 0, x, this.Width, x);
            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.linePen != null)
            {
                this.linePen.Dispose();
                this.linePen = null;
            }
            base.Dispose(disposing);
        }
    }
}
