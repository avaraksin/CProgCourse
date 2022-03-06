using System;

namespace SeaButtle2
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public bool isLegal
        {
            get
            {
                return X > 0 && Y > 0 && X <= Field.SIZE && Y <= Field.SIZE;
            }
        }

        /// <summary>
        /// Конструктор класса Point по умолчанию возвращает точку со случайными координатами
        /// </summary>
        public Point()
        {
            X = new Random().Next(1, Field.SIZE + 1);
            Y = new Random().Next(1, Field.SIZE + 1);
        }

        /// <summary>
        /// Конструктор класса Point
        /// </summary>
        /// <param name="X">Координата по оси ox</param>
        /// <param name="Y">Координата по оси oy</param>
        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
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
