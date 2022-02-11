using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public class Point : IEquatable<Point>
    {
        public int X;
        public int Y;

        public bool isNullPoint { get { return (X == 0 && Y == 0); } }

        public bool isInvPoint
        {
            get
            {
                return X < 1 || X > Field.SIZE || Y < 1 || Y > Field.SIZE;
            }
        }

        public Point Random
        {
            get
            {
                return new Point( new Random().Next(1, Field.SIZE + 1), new Random().Next(1, Field.SIZE + 1) );
            }
        }

        public Point() : this(0, 0) {}

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool Equals(Point point)
        {
            return X == point.X && Y == point.Y;
        }

        public override int GetHashCode()
        {
            return Y + 10 * (X - 1);      
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Point p = (Point)obj;
                return (X == p.X) && (Y == p.Y);
            }
        }


    }
}
