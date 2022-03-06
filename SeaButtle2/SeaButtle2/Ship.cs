using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public abstract class Ship
    {
        public const int DECKSNUM = 4;
        public Point startPoint { get; set; }
        public Orientation orientation { get; set; }
        public string Name { get; set; }
        public int decksNum { get; set; }
        public bool Sunk { get; set; }
        public DeckStatus[] Decks { get; set; }

        public List<Point> GetCoordinates
        {
            get
            {
                List<Point> coordinates = new List<Point>();
                for (var i = 0; i < decksNum; i++)
                {
                    if (orientation == Orientation.HORIZONTAL)
                    {
                        coordinates.Add(new Point(startPoint.X + i, startPoint.Y));
                    }
                    else if (orientation == Orientation.VERTICAL)
                    {
                        coordinates.Add(new Point(startPoint.X, startPoint.Y + 1));
                    }

                }
                return coordinates;
            }
        }
        public Ship(Point point, Orientation orientation, string name, int decksNum) 
        {
            startPoint = point;
            this.orientation = orientation;
            Name = name;
            this.decksNum = decksNum;
            Decks = new DeckStatus[decksNum + 1];
            for (int i = 1; i <= decksNum; i++)
            {
                Decks[i] = DeckStatus.OK;
            }
        }

        public Ship(int decksNum)
        {
            startPoint = new Point();
            orientation = (Orientation)new Random().Next(0, 2);
            this.decksNum = decksNum;
            Decks = new DeckStatus[decksNum + 1];
            for (int i = 1; i <= decksNum; i++)
            {
                Decks[i] = DeckStatus.OK;
            }
            Name = decksNum.ToString() + "s DECKS";
        }

        /// <summary>
        /// Shot принять выстрел
        /// </summary>
        public virtual ShootResult Shot(Point point)
        {
            bool hit = false;
            if (orientation == Orientation.HORIZONTAL)
            {
                if (startPoint.Y == point.Y && point.X >= startPoint.X && point.X <= startPoint.X + decksNum - 1)
                {
                    hit = true;
                    Decks[point.X - startPoint.X + 1] = DeckStatus.SUNK;
                }
            }
            else if (orientation == Orientation.VERTICAL)
            {
                if (startPoint.X == point.X && point.Y >= startPoint.Y && point.Y <= startPoint.Y + decksNum - 1)
                {
                    hit = true;
                    Decks[point.Y - startPoint.Y + 1] = DeckStatus.SUNK;
                }
            }
            
            if (hit)
            {
                for (var i = 1; i <= decksNum;i++)
                {
                    if (DeckStatus.OK == Decks[i]) return ShootResult.HIT;
                }
                Sunk = true;
                return ShootResult.SUNK;
            }

            return ShootResult.MISS;
        }
    }

    /// <summary>
    /// TypicalShip - корабль как корабль
    /// </summary>
    public class TypicalShip : Ship
    {
        public TypicalShip(Point point, Orientation orientation, string name, int decksNum) 
            : base(point, orientation, name, decksNum) {}

        public TypicalShip(int decksNum) : base(decksNum) {}
    }
}
