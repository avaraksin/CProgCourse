using System.Collections.Generic;

namespace SeaButtle2
{
    public enum ShootResult
    {
        SHOOTIN = -1,
        MISS    = 0,
        HIT     = 1,
        SUNK    = 2
    }
    public enum DeckStatus
    {
        OK = 0,
        SUNK = 1
    }
    public enum Orientation
    {
        HORIZONTAL = 0,
        VERTICAL = 1
    }
    /// <summary>
    /// Cell ячейка поля, может содержать ссылку на корабль
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Ссылка на корабль в ячейке, если null значит корабля тут нет
        /// </summary>
        public Ship ship { get; set; }

        public Point point { get; }

        public Cell(Point point)
        {
            this.point = point;
        }

        /// <summary>
        /// Уже стреляли
        /// </summary>
        public bool Shot { get; set; }

        /// <summary>
        /// Находится рядом с кораблём - не стоит тут размещать другой корабль
        /// </summary>
        public bool NearShip { get; set; }
    }

    /// <summary>
    /// Field игровое поле
    /// Состоит из ячеек типа Cell, это сделано специально потому что в ячейках может быть ссылка на корабль, а может какое-то значение, например признак нахождения рядом с кораблём
    /// </summary>
    public class Field
    {
        public Cell[,] field;

        public const int SIZE = 10; 

        public Field()
        {
            field = new Cell[SIZE + 1, SIZE + 1];

            for (int i = 1; i <= SIZE; i++)
            {
                for (int j = 1; j <= SIZE; j++)
                {
                    field[i, j] = new Cell(new Point(i, j));
                }
            }

            // todo это набросок цикла, доработать
            for (var i = 0; i < Ship.DECKSNUM; i++)
            {
                for (var j = 0; j < Ship.DECKSNUM - i; j++)
                {
                    var fits = false;
                    while (!fits)
                    {
                        var ship = new TypicalShip(i);

                        foreach (var point in ship.GetCoordinates)
                        {
                            var (x, y) = (point.X, point.Y);
                            if (!PointInField(point) || field[x, y].NearShip || field[x, y].ship != null)
                            {
                                continue;
                            }

                            fits = true;
                            SetShipInField(ship);
                        }
                    }
                }
            }
        }

        private bool PointInField(Point point)
        {
            return (point.X >= 1 && point.X <= SIZE && point.Y >= 1 && point.Y <= SIZE);
        }

        private void SetShipInField(Ship ship)
        {
            foreach (var point in ship.GetCoordinates)
            {
                var (x, y) = (point.X, point.Y);
                field[x, y].ship = ship;

                for (var i = -1; i <= 1; i++)
                {
                    for (var k = -1; k <= 1; k++)
                    {
                        Point newpoint = new Point(x + i, y + k);
                        if (PointInField(newpoint) && field[newpoint.X, newpoint.Y].ship == null)
                        {
                            field[newpoint.X, newpoint.Y].NearShip = true;
                        }
                    }
                }

            }
        }

        public ShootResult Shot(Point point)
        {
            var cell = field[point.X, point.Y];
            // уже стреляли
            if (cell.Shot)
            {
                return ShootResult.SHOOTIN;
            }

            cell.Shot = true;
            if (cell.ship == null)
            {
                return ShootResult.MISS;
            }
            
            return cell.ship.Shot(point);            
        }
    }
}
