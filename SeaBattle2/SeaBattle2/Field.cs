using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle2
{
    internal class Field
    {
        List<Ship> _ships;
        int[,] _cells;

        public Field(int size)
        {
            // создаём корабли и заполняем поле
            var ship = new Ship(size);

        }

    }
}
