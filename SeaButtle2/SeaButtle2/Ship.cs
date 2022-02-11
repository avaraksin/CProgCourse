using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public enum Orientation
    {
        HORIZONTAL = 0,
        VERTICAL   = 1
    }
    public class Ship
    {
        public readonly int decks;
        public readonly Point coordinates;
        public readonly Orientation orientation;
        public int decksuck;
        
        public Ship(int decks, Orientation orientation)
        {
            this.decks = decks;
            this.orientation = orientation;
            decksuck = 0;

            do
            {
                coordinates = new Point().Random;

            } while (!CheckShip()); // вторая проверка, убрать
        }

        public List<Point> ShipArea()
        {
            List<Point> shiparea = new List<Point>();
            for(int i = 0; i < decks; i++)
            {
                Point point = new Point();
                point.X = coordinates.X + (1 - (int)orientation) * i;
                point.Y = coordinates.Y + (int)orientation * i;

                shiparea.Add(point);
            }
            return shiparea;
        }

        private bool CheckShip()
        {
            return !coordinates.isInvPoint && !ShipArea()[decks - 1].isInvPoint;
        }
    }
}
