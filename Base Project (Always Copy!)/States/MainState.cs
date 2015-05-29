using Base_Project__Always_Copy__.Structures;
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
        Vector2 v1 = new Vector2(400, 120), v2 = new Vector2(34, 180), v3 = new Vector2(160, 190);
        Triangle triangle;

        public MainState()
        {
            triangle = new Triangle(v1, v2, v3);
            Console.WriteLine("Center: X: " + triangle.circumcenter.x.ToString() + ", Y: " + triangle.circumcenter.y.ToString() + ", Radius: " + triangle.circumradius.ToString());
        
        }

        public override void Update()
        {
        }

        public override void MouseMoved(MouseEventArgs e)
        {
        }

        private void recalculate()
        {

        }


        public override void MouseClicked(MouseEventArgs e)
        {
            
            v1 = new Vector2(e.X, e.Y);
            triangle = new Triangle(v1, v2, v3);
 
        }

        public override void Redraw(PaintEventArgs e)
        {

            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)v1.x - 3, (int)v1.y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)v2.x - 3, (int)v2.y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)v3.x - 3, (int)v3.y - 3, 6, 6));

            int topX = (int)(triangle.circumcenter.x - triangle.circumradius);
            int topY = (int)(triangle.circumcenter.y - triangle.circumradius);
            float width = (float)(2 * triangle.circumradius);
            float height = (float)(2 * triangle.circumradius);
            
            e.Graphics.DrawEllipse(Pens.Blue, topX, topY, width, height);

            /*
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
             */
        }
    }

