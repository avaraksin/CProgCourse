using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public class Point
    {
        public int X;
        public int Y;

        public bool isNullPoint { get { return (X == 0 && Y == 0); } }

        public bool isInvPoint
        {
            get
            {
                return X < 1 || X > 10 || Y < 1 || Y > 10;
            }
        }

        public Point Random
        {
            get
            {
                return new Point( new Random().Next(1, 11), new Random().Next(1, 11) );
            }
        }

        public Point() 
        {
            X = Y = 0;
        }

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }


    }
}
