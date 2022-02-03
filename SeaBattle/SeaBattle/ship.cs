using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal class Ship
    {
        public int decksnum { get; private set; }
        public int addr { get; private set; }
        internal int orientation { get; set; }  // 0 - по горизонтали, 1 - по вертикали 
        public List<int> shiparea { get; private set; }
        
        public List<int> freeField { get; private set; }            // Свободное поле вокруг корабля

        private int bitdecks; 

        public Ship(int decksnum)
        {
            this.decksnum = decksnum;
        }

        internal bool CheckShip(int shipaddr)
        {
            if (orientation == 0)
            {
                if (shipaddr % 10 == 0)
                {
                    if (decksnum > 1) return false;
                }
                else
                {
                    if (shipaddr % 10 + decksnum - 1 > 10) return false;
                }
            }
            else
            {
                if (shipaddr % 10 == 0)
                {
                    if (shipaddr / 10 + decksnum - 1 > 10) return false;
                }
                else
                {
                    if (shipaddr / 10 + decksnum > 10) return false;
                }
            }

            return true;
        }

        internal bool SetShipAddr(int shipaddr)
        {
            if (CheckShip(shipaddr))
            {
                addr = shipaddr;
                shiparea = new List<int>();
                freeField = new List<int>();
                int index;
                for(int i = 1; i <= decksnum; i++)
                {
                    index = orientation * 10 * (i - 1) + addr + (1 - orientation) * (i - 1);
                    shiparea.Add(index);
                }

                if (true)
                {
                    for(int i = 0; i < decksnum; i++)
                    {
                        int j = shiparea[i];
                        if (j % 10 != 1)
                        {
                            for(index = j - 11; index <= j + 11; index += 10)
                            {
                                if ( !freeField.Contains(index) && !shiparea.Contains(index) && index > 0 && index <= 100 ) freeField.Add(index);
                            }
                        }
                        if (j % 10 != 0)
                        {
                            for(index = j - 9; index <= j + 11; index += 10)
                            {
                                if (!freeField.Contains(index) && !shiparea.Contains(index) && index > 0 && index <= 100) freeField.Add(index);
                            }
                        }
                        for(index = j - 10; index <= j + 10; index += 10)
                        {
                            if (!freeField.Contains(index) && !shiparea.Contains(index) && index > 0 && index <= 100) freeField.Add(index);
                        }
                    }
                }

                bitdecks = 0;
                return true;
            }
            return false;
        }

        internal bool SetShipStatus(int pnt)
        {
            bitdecks++;
            if (bitdecks == decksnum) return true;
            return false;
        }

    }
}
