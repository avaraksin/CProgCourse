using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public abstract class Player
    {
        public int move;
        public Field field;
        public Player()
        {
            field = new Field();
            move = 0;
        }

        public abstract PointStatus DoMove(Player player, Point point);
    }
}
