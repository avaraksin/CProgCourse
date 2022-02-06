using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public class BilPlayer : Player
    {
        private bool isDeckkSuck;

        private bool[] nextDirection;

        public BilPlayer()
        {
            nextDirection = new bool[4] { true, true, true, true };
        }


    }
}
