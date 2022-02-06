using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public class Player
    {
        public int move;
        public Field field;
        public Player()
        {
            if (field == null)
            {
                field = new Field();
            }
            move = 0;
        }
    }
}
