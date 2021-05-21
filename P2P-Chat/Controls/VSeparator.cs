using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace P2P_Chat.Controls
{
    public class VSeparator : Control
    {
        private Color lineColor;
        private Pen linePen;
        private int lineWidth;

        public int LineWidth {
            get
            {
                return this.lineWidth;
            }
            set 
            {
                this.lineWidth = value;
                this.linePen = new Pen(this.lineColor, this.lineWidth);
                this.linePen.Alignment = PenAlignment.Inset;
                Refresh();
            } 
        }

        public VSeparator()
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
            int x = this.Width / 2;
            graph.DrawLine(linePen, x, 0, x, this.Height);
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
