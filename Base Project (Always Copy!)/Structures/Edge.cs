using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_Project__Always_Copy__.Structures
{
    public class Edge
    {
        public Vector2 p1, p2;

        public Edge(Vector2 p1, Vector2 p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        /// <summary>
        /// Checks to see whether edge is listed twice within given list.
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public bool isUnique(ArrayList edges)
        {
            bool foundSelf = false;

            foreach (Edge e in edges)
            {
                //If the two points match up (either way round)
                if (((p1.x == e.p1.x && p1.y == e.p1.y && p2.x == e.p2.x && p2.y == e.p2.y) || (p1.x == e.p2.x && p1.y == e.p2.y && p2.x == e.p1.x && p2.y == e.p1.y)))
                {
                    if (!foundSelf) //The Edge will be listed once in the list itself, which will lead to a false positive.
                    {
                        foundSelf = true;
                    //    Console.WriteLine("Found Self");
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
