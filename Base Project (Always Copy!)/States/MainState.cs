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
        public List<Triangle> triangles = new List<Triangle>();
        public List<Vector2> points = new List<Vector2>();
        public List<Edge> voronoiEdges = new List<Edge>();
        Random rand = new Random();

        public MainState()
        {
          //Add SuperTriangles.
          triangles.Add(new Triangle(new Vector2(0, 0), new Vector2(0, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0)));
          triangles.Add(new Triangle(new Vector2(Screen.WIDTH, Screen.HEIGHT), new Vector2(0, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0)));
          //triangles.Add(new Triangle(new Vector2(200, 10), new Vector2(0, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0)));

          for (int i = 0; i < 200; i++)
          {
           //  Recalculate(new Vector2(rand.NextDouble() * Screen.WIDTH, rand.NextDouble() * Screen.HEIGHT));
          }
          GenerateVoronoi();
        }

        public override void Update()
        {
        }

        public override void MouseMoved(MouseEventArgs e)
        {
        }

        private void Recalculate(Vector2 newPoint)
        {
            ArrayList containsPoint = new ArrayList();
            ArrayList containedEdges = new ArrayList();
            ArrayList uniqueEdges = new ArrayList();

            foreach (Triangle t in triangles.ToArray())
            {
                //Check whether new point is within circumcircle
                if ((Math.Pow(newPoint.x - t.circumcenter.x, 2)) + (Math.Pow(newPoint.y - t.circumcenter.y, 2)) < Math.Pow(t.circumradius, 2))
                {
                  //  Console.WriteLine("New Point within Circumcircle");
                    containsPoint.Add(t);
                    //This triangle will be broken up into smaller triangles, so remove it from triangles list.
                    triangles.Remove(t);

                    //Add edges to list.
                    containedEdges.Add(new Edge(t.p1, t.p2));
                    containedEdges.Add(new Edge(t.p2, t.p3));
                    containedEdges.Add(new Edge(t.p3, t.p1));
                   // Console.WriteLine("Added Contained Edges!");
                }
            }

            foreach (Edge e in containedEdges)
            {
                if (e.isUnique(containedEdges))
                {
                    uniqueEdges.Add(e);
                    //Console.WriteLine("Found unique edge!");
                }
            }

            foreach (Edge e in uniqueEdges)
            {
                triangles.Add(new Triangle(e.p1, e.p2, newPoint));
            }
        }

        public void GenerateVoronoi()
        {
            voronoiEdges.Clear();
            foreach (Triangle t in triangles)
            {
                foreach (Triangle t2 in t.SharesEdge(triangles))
                {
                    voronoiEdges.Add(new Edge(t.circumcenter, t2.circumcenter));
                }
            }
        }


        public override void MouseClicked(MouseEventArgs e)
        {
            Recalculate(new Vector2(e.X, e.Y));
            GenerateVoronoi();
        }

        public override void Redraw(PaintEventArgs e)
        {
            foreach (Triangle t in triangles)
            {
                t.Redraw(e);     
            }

            foreach (Edge edge in voronoiEdges)
            {
                e.Graphics.DrawLine(Pens.Red, new Point((int)edge.p1.x, (int)edge.p1.y), new Point((int)edge.p2.x, (int)edge.p2.y));
            }
        }
    }

