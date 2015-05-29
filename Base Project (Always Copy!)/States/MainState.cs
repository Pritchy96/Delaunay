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
        public ArrayList triangles = new ArrayList();

        public MainState()
        {
          //Add SuperTriangles.
          triangles.Add(new Triangle(new Vector2(0, 0), new Vector2(0, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0)));
          triangles.Add(new Triangle(new Vector2(Screen.WIDTH, Screen.HEIGHT), new Vector2(0, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0)));
          triangles.Add(new Triangle(new Vector2(Screen.WIDTH- 400, Screen.HEIGHT - 20), new Vector2(200, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0)));
          triangles.Add(new Triangle(new Vector2(Screen.WIDTH- 350, Screen.HEIGHT- 350), new Vector2(0, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0)));
        }

        public override void Update()
        {
        }

        public override void MouseMoved(MouseEventArgs e)
        {
        }

        private void recalculate(Vector2 newPoint)
        {
            ArrayList containsPoint = new ArrayList();

            foreach (Triangle t in triangles)
            {
                //Check whether new point is within circumcircle
                if ((Math.Pow(newPoint.x - t.circumcenter.x, 2)) + (Math.Pow(newPoint.y - t.circumcenter.y, 2)) < Math.Pow(t.circumradius, 2))
                {
                    Console.WriteLine("Huzzah!");
                }
            }
            Console.WriteLine();
        }


        public override void MouseClicked(MouseEventArgs e)
        {
            recalculate(new Vector2(e.X, e.Y));
        }

        public override void Redraw(PaintEventArgs e)
        {
            foreach (Triangle t in triangles)
            {
                t.Redraw(e);
            }
            /*
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)v1.x - 3, (int)v1.y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)v2.x - 3, (int)v2.y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)v3.x - 3, (int)v3.y - 3, 6, 6));

            int topX = (int)(triangle.circumcenter.x - triangle.circumradius);
            int topY = (int)(triangle.circumcenter.y - triangle.circumradius);
            float width = (float)(2 * triangle.circumradius);
            float height = (float)(2 * triangle.circumradius);
            
            e.Graphics.DrawEllipse(Pens.Blue, topX, topY, width, height);
             * 
             * */






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

