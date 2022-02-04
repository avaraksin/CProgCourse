using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal static class Strat
    {
        private static bool StratOn;

        public static int lastpnt { get; set; }

        private static bool top, button, left, right;

        private static int OldPnt;

        private static Shiparea shiparea;

        private static int priordir;

        public static int SetNewPoint(int pnt)
        {
            int answ;
            if (StratOn)
            {
                int newpnt = 0;
                if (pnt <= 10) top = true;
                if (pnt / 10 >= 9) button = true;
                if (pnt % 10 == 1) left = true;
                if (pnt % 10 == 0) right = true;
                if (OldPnt == 0) OldPnt = pnt;
                for(int i = 1; i <=4; i++)
                {
                    if (i == 1 && !top) newpnt = OldPnt - 10;
                    if (i == 2 && !button) newpnt = OldPnt + 10;
                    if (i == 3 && !left) newpnt = OldPnt - 1;
                    if (i == 4 && !right) newpnt = OldPnt + 1;

                    answ = shiparea.CheckShootNoSet(newpnt);
                    if (answ == 0)
                    {
                        if (i == 1) top = true;
                        if (i == 2) button = true;
                        if (i == 3) left = true;
                        if (i == 4) right = true;
                        return newpnt;
                    }
                    if (answ == 1)
                    {
                        OldPnt = newpnt;
                        priordir = 1;
                        return newpnt;
                    }
                    if (answ == -1 && i == 1) top = true;
                    if (answ == -1 && i == 2) button = true;
                    if (answ == -1 && i == 3) left = true;
                    if (answ == -1 && i == 4) right = true;
                }
                
            }
            return new Random().Next(1, 101);
        }

        public static void SetStratOff()
        {
            StratOn = false;
            top = button = left = right = false;
            OldPnt = 0;
        }

        public static void SetStratOn(Shiparea sa)
        {
            if (!StratOn)
            {
                shiparea = sa;
                StratOn = true;
                priordir = 0;
            }
        }
    }
}
