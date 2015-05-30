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
    public List<Triangle> triangles = new List<Triangle>(), superTriangles = new List<Triangle>();
    public List<Vector2> points = new List<Vector2>();
    public List<Edge> voronoiEdges = new List<Edge>();
    Random rand = new Random();

    public MainState()
    {
        //Add SuperTriangles.
        Triangle super1 = new Triangle(new Vector2(0, 0), new Vector2(0, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0)),
            super2 = new Triangle(new Vector2(Screen.WIDTH, Screen.HEIGHT), new Vector2(0, Screen.HEIGHT), new Vector2(Screen.WIDTH, 0));

        triangles.Add(super1);
        triangles.Add(super2);
        superTriangles.Add(super1);
        superTriangles.Add(super2);

        int numberOfPoints = 500;
        double rows = Math.Sqrt(numberOfPoints);

        int GapY = (int)(Screen.HEIGHT / (rows + 1));
        int GapX = (int)(Screen.WIDTH / (rows + 1));



        for (int i = 1; i <= rows; i++)
        {
            double peturbation = GapX;  //Amount to shift the point by (in total, +/- peturbation/2)
            double offsetX = ((rand.NextDouble() * peturbation / 8) - (peturbation / 2)); //Calculates a random offset between -peturbation/2 and peturbation/2;

            //Adding edge points.
            Recalculate(new Vector2((GapX * i) + offsetX, 1));
            offsetX = ((rand.NextDouble() * peturbation) - (peturbation / 2));
            Recalculate(new Vector2((GapX * i) + offsetX, Screen.HEIGHT-1));
            offsetX = ((rand.NextDouble() * peturbation) - (peturbation / 2));
            Recalculate(new Vector2(1, (GapX * i) + offsetX));
            offsetX = ((rand.NextDouble() * peturbation) - (peturbation / 2));
            Recalculate(new Vector2(Screen.WIDTH - 1, (GapX * i) + offsetX));


            for (int j = 1; j <= rows; j++)
            {
                offsetX = ((rand.NextDouble() * peturbation) - (peturbation / 2)); //Calculates a random offset between -peturbation/2 and peturbation/2;
                double offsetY = ((rand.NextDouble() * peturbation) - (peturbation / 2)); //For the non edge nodes we need 2 dimensions of coordinate offset.
                Recalculate(new Vector2(GapX * i + offsetX, GapX * j + offsetY));
            }
        }


        Console.WriteLine(rows.ToString());

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
        foreach (Triangle t in triangles.ToList())
        {
            foreach (Triangle t2 in t.SharesEdge(triangles))
            {
                if (t2.SharesEdge(superTriangles).Count > 0)
                {
                    triangles.Remove(t2);
                }
                else
                {
                    voronoiEdges.Add(new Edge(t.circumcenter, t2.circumcenter));
                }
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

