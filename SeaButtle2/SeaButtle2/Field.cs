using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public enum FIELDSTATUS
    {
        EMPTY  = 0,
        AWAY   = 2,
        DECK   = 1,
        SUNK   = 3
    }
    public enum PointStatus
    {
        ILLEGAL = -1,
        AWAY = 0,
        DECKSUCK = 1,
        SHIPSUCK = 2
    }
    public class Field
    {
        public int[,] field;
        public List<Ship> ships;

        public Field()
        {
            ships = new List<Ship>();
            field = new int[11, 11];
            Ship ship;

            for (int i = 4; i >= 1; i--)
            {
                for (int k = 4; k >= i; k--)
                {
                    do
                    {
                        ship = new Ship(i, (Orientation)new Random().Next(0, 2));
                        if (IsLegalShip(ship))
                        {
                            ships.Add(ship);
                            foreach (Point point in ship.ShipArea())
                            {
                                field[point.X, point.Y] = (int)FIELDSTATUS.DECK;
                            }
                            break;
                        }
                    } while (true);
                }
            }

        }

        public List<Point> FreeField(Ship ship)
        {
            List<Point> freeField = new List<Point>();

            foreach (Point point in ship.ShipArea())
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        Point pnt = new Point(point.X + x, point.Y + y);
                        if ( !freeField.Contains(pnt) && !pnt.isInvPoint && !ship.ShipArea().Contains(pnt))
                        {
                            freeField.Add(pnt);
                        }
                    }
                }
            }
            return freeField;
        }

        public bool IsLegalShip(Ship ship)
        {
            foreach (Point point in ship.ShipArea())
            {
                foreach (Ship fieldship in ships)
                {
                    if (fieldship.ShipArea().Contains(point) || FreeField(fieldship).Contains(point)) return false;
                }
            }
            return true;
        }

        private Ship GetShip(Point point)
        {
            foreach (Ship ship in ships)
            {
                if (ship.ShipArea().Contains(point)) return ship;
            }
            return null;
        }

        public PointStatus GetPointStatus(Point point)
        {
            if (point.isInvPoint || field[point.X, point.Y] == (int)FIELDSTATUS.AWAY || field[point.X, point.Y] == (int)FIELDSTATUS.SUNK)
            {
                return PointStatus.ILLEGAL;
            }
            else if (field[point.X, point.Y] == (int)FIELDSTATUS.EMPTY)
            {
                return PointStatus.AWAY;
            }
            else
            {
                Ship ship = GetShip(point);
                if (ship == null) return PointStatus.ILLEGAL;

                if (ship.decks == ship.decksuck + 1) return PointStatus.SHIPSUCK;
            }

            return PointStatus.DECKSUCK;
        }

        public PointStatus SetShoot(Point point)
        {
            PointStatus pointStatus = GetPointStatus(point);

            switch (pointStatus)
            {
                case PointStatus.AWAY:
                    field[point.X, point.Y] = (int)FIELDSTATUS.AWAY;
                    break;

                case PointStatus.ILLEGAL:
                    break;

                default:
                    field[point.X, point.Y] = (int)FIELDSTATUS.SUNK;
                    break;
            }

            return pointStatus;
        }

    }
}
