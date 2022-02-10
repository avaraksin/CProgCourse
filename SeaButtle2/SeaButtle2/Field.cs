using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public enum ShootResult
    {
        MISS  = 0,
        HIT   = 1,
        SUNK   = 2
    }

    public enum DeckStatus
    {
        OK = 0,
        SUNK = 1,
    }

    public enum Orientation
    {
        HORIZONTAL = 0,
        VERTICAL = 1,
    }

    /// <summary>
    /// Cell ячейка поля, может содержать ссылку на корабль
    /// </summary>
    public class Cell
    {
        public Ship ship { get; }

        public Cell(Ship ship)
        {
            this.ship = ship;
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
        public List<Ship> ships;

        public Field()
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4 - i; j++)
                {
                    var fits = false;
                    while (!fits)
                    {
                        var ship = new Ship();

                        foreach (var point in ship.GetCoordinates())
                        {
                            var (x, y) = (point.X, point.Y);
                            if (field[x, y].NearShip || field[x, y].ship != null)
                            {
                                continue;
                            }

                            fits = true;
                        }
                    }

                }
            }
        }

        public void Shot(Point point)
        {
            var cell = field[point.X, point.Y];
            // уже стреляли
            if (cell.Shot) {
                // todo
                
            }

            if (cell.ship == null)
            {
                cell.Shot = true;
                
            }

            // в случае если корабль есть, подбиваем его
            cell.ship.Shot();
        }
    }
}
