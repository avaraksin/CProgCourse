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
        EMPTY = 0,
        AWAY = 2,
        DECK = 1,
        SUNK = 3
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
                for (int k = 1; k <= i; k++)
                {
                    do
                    {
                        ship = new Ship(k, (Orientation)new Random().Next(0, 2));
                        if (IsLegalShip(ship))
                        {
                            ships.Add(ship);
                            foreach (Point point in ship.ShipArea())
                            {
                                field[point.X, point.Y] = (int)FIELDSTATUS.DECK;
                            }
                        }
                    } while (!IsLegalShip(ship));
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

        public void PrintArea()
        {
            string[] fieldsp = new string[] { "\u2592", "\u2588", "\u25CF", "X" };
            string[] fieldsb = new string[] { "\u2592", "\u2592", "\u25CF", "X" };

            Console.WriteLine("  ABCDEFGHIJ");

            string row;

            for (int x = 1; x <= field.GetUpperBound(0); x++)
            {
                row = x.ToString();
                if (x < 10) row += " ";
                for (int y = 1; y <= field.GetUpperBound(1); y++)
                {
                    row += fieldsp[field[x, y]];
                }
                Console.WriteLine(row);
            }
        }
    }
}
