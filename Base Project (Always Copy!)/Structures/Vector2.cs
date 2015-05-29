using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_Project__Always_Copy__.Structures
{
    public class Vector2
    {
        public double x, y;

        public Vector2(double x, double y) 
        {
            this.x = x;
            this.y = y;
        }

        public Boolean isSame (Vector2 point)
        {
            if (x == point.x && y == point.y)
                return true;
            else
                return false;

        }

        /// <summary>
        /// Finds Distance between this position and Point
        /// </summary>
        /// <param name="Point">Point to find distance to from this vector2</param>
        /// <returns></returns>
        public Double DistanceTo(Vector2 Point)
        {
            return Math.Sqrt(Math.Pow((x - Point.x), 2) + Math.Pow((y - Point.y), 2));
        }
    }
}
