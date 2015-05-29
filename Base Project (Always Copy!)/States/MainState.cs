using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

 

    class MainState : BasicState
    {
        Point p1 = new Point(150, 173);
        Point p2 = new Point(300, 334);
        Point p3 = new Point(157, 273);
        Point mid12, mid13;
        int x, y;
        float perpGrad12, perpGrad13, c12, c13;

        public MainState()
        {

        }

        public override void Update()
        {
        }

        public override void MouseMoved(MouseEventArgs e)
        {
        }

        private void recalculate()
        {
            mid12 = new Point((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
            mid13 = new Point((p3.X + p1.X) / 2, (p3.Y + p1.Y) / 2);

            perpGrad12 = (float) -1 / (float)((float)(p2.Y - p1.Y) / (float)(p2.X - p1.X));
            perpGrad13 = (float) -1 / (float)((float)(p3.Y - p1.Y) / (float)(p3.X - p1.X));

            c12 = mid12.Y - (perpGrad12 * mid12.X);
            c13 = mid13.Y - (perpGrad13 * mid13.X);

            x = (int)((c13 - c12) / (perpGrad12 - perpGrad13));
            y = (int)((perpGrad12 * x) + c12);
        }


        public override void MouseClicked(MouseEventArgs e)
        {
            p1 = new Point(e.X, e.Y);
            recalculate();
        }

        public override void Redraw(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(p1.X - 3, p1.Y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(p2.X - 3, p2.Y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(p3.X - 3, p3.Y - 3, 6, 6));

            e.Graphics.FillEllipse(Brushes.DarkRed, new Rectangle(mid12.X - 3, mid12.Y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.DarkRed, new Rectangle(mid12.X - 3, mid12.Y - 3, 6, 6));


            e.Graphics.DrawLine(Pens.Orange, p1, p2);
            e.Graphics.DrawLine(Pens.Orange, p3, p2);
            e.Graphics.DrawLine(Pens.Orange, p1, p3);

            float radius = (float) Math.Sqrt(Math.Pow(p1.X - mid12.X, 2) + Math.Pow(p1.Y - mid12.Y, 2));
            int topX = (int) (x - radius);
            int topY = (int) (y - radius);
            float width = 2 * radius;
            float height = 2 * radius;

            e.Graphics.DrawEllipse(Pens.Blue, topX, topY, width, height);
        }
    }

