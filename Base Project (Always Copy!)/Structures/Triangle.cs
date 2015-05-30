using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base_Project__Always_Copy__.Structures
{
    class Triangle
    {
        public Vector2 p1 = new Vector2(0, 0), p2 = new Vector2(0, 0), p3 = new Vector2(0, 0), circumcenter;
        public double circumradius;

        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            calculateCircumscribedCircle();
        } 


        //Code dragged from http://www.war-worlds.com/blog/2012/06/planet-rendering---part-2-voroni-diagrams-and-delaunay-triangulation for speed, will rewrite later.
        public void calculateCircumscribedCircle()
        {
            const double EPSILON = 0.000000001;

            if (Math.Abs(p1.y - p2.y) < EPSILON && Math.Abs(p2.y - p3.y) < EPSILON)
            {
                // the points are coincident, we can't do this...
                return;
            }

            double xc, yc;
            if (Math.Abs(p2.y - p1.y) < EPSILON)
            {
                double m = -(p3.x - p2.x) / (p3.y - p2.y);
                double mx = (p2.x + p3.x) / 2.0;
                double my = (p2.y + p3.y) / 2.0;
                xc = (p2.x + p1.x) / 2.0;
                yc = m * (xc - mx) + my;
            }
            else if (Math.Abs(p3.y - p2.y) < EPSILON)
            {
                double m = -(p2.x - p1.x) / (p2.y - p1.y);
                double mx = (p1.x + p2.x) / 2.0;
                double my = (p1.y + p2.y) / 2.0;
                xc = (p3.x + p2.x) / 2.0;
                yc = m * (xc - mx) + my;
            }
            else
            {
                double m1 = -(p2.x - p1.x) / (p2.y - p1.y);
                double m2 = -(p3.x - p2.x) / (p3.y - p2.y);
                double mx1 = (p1.x + p2.x) / 2.0;
                double mx2 = (p2.x + p3.x) / 2.0;
                double my1 = (p1.y + p2.y) / 2.0;
                double my2 = (p2.y + p3.y) / 2.0;
                xc = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
                yc = m1 * (xc - mx1) + my1;
            }

            circumcenter = new Vector2((int)xc, (int)yc);
            circumradius = circumcenter.DistanceTo(p2);
        }


        /// <summary>
        /// Checks if a triangle in the given list shares an edge with this one (excluding itself).
        /// </summary>
        /// <param name="triangles">List of Triangles to check for shared edges</param>
        /// <returns></returns>
        public List<Triangle> SharesEdge (List<Triangle> triangles)
        {
            List<Triangle> hasCommonEdge = new List<Triangle>();

            foreach (Triangle t in triangles)
            {
                int pointMatches = 0;   //If any two points of the triangles are equal, it must share an edge.

                if ((p1.Equals(t.p1) || p1.Equals(t.p2) || p1.Equals(t.p3)))
                {
                    pointMatches++;
                }
                if ((p2.Equals(t.p1) || p2.Equals(t.p2) || p2.Equals(t.p3)))
                {
                    pointMatches++;
                }
                if ((p3.Equals(t.p1) || p3.Equals(t.p2) || p3.Equals(t.p3)))
                {
                    pointMatches++;
                }

                //They share an edge!
                if (pointMatches == 2)  //If < 2, it does not share an edge, if = 3 it is the triangle we are checking.
                {
                    hasCommonEdge.Add(t);
                }
            }
            return hasCommonEdge;
        }

        public void Redraw(PaintEventArgs e)
        {
            
           // e.Graphics.FillEllipse(Brushes.Orange, new Rectangle((int)circumcenter.x - 3, (int)circumcenter.y - 3, 6, 6));

            Point point1 = new Point((int)p1.x, (int)p1.y), point2 = new Point((int)p2.x, (int)p2.y), point3 = new Point((int)p3.x, (int)p3.y);
            /*
            e.Graphics.DrawLine(Pens.Blue, point1, point2);
            e.Graphics.DrawLine(Pens.Blue, point2, point3);
            e.Graphics.DrawLine(Pens.Blue, point3, point1);
             * */
                                                                                            
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)p1.x - 2, (int)p1.y - 2, 4, 4));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)p2.x - 2, (int)p2.y - 2, 4, 4));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)p3.x - 2, (int)p3.y - 2, 4, 4));

            e.Graphics.FillEllipse(Brushes.Orange, new Rectangle((int)circumcenter.x - 2, (int)circumcenter.y - 2, 4, 4));

            /*
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)p1.x - 3, (int)p1.y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)p2.x - 3, (int)p2.y - 3, 6, 6));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle((int)p3.x - 3, (int)p3.y - 3, 6, 6));

            e.Graphics.FillEllipse(Brushes.Orange, new Rectangle((int)circumcenter.x - 3, (int)circumcenter.y - 3, 6, 6));

            Point point1 = new Point((int)p1.x, (int)p1.y), point2 = new Point((int)p2.x, (int)p2.y), point3 =  new Point((int)p3.x, (int)p3.y);
            e.Graphics.DrawLine(Pens.Black, point1, point2);
            e.Graphics.DrawLine(Pens.Black, point2, point3);
            e.Graphics.DrawLine(Pens.Black, point3, point1);*/

            /*
            int topX = (int)(circumcenter.x - circumradius);
            int topY = (int)(circumcenter.y - circumradius);
            float width = (float)(2 * circumradius);
            float height = (float)(2 * circumradius);
            e.Graphics.DrawEllipse(Pens.Blue, topX, topY, width, height);
             */
        }
         
    }
}
