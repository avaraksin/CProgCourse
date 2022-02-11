using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public class BilPlayer : Player
    {
        private bool isDeckSuck;

        private bool[] nextDirection;

        private Point pnt, OldPnt, newpnt;
        public bool StratOn;

        public void newStrategy()
        {
            nextDirection = new bool[4] { false, false, false, false };
            OldPnt = new Point();
            StratOn = true;
        }

        public override PointStatus DoMove(Point point)
        {
            Console.WriteLine("Comp");
            return PointStatus.ILLEGAL;
            //PointStatus answ;
            //if (StratOn)
            //{
            //    if (pnt.X == 1) nextDirection[0] = true;
            //    if (pnt.X == 10) nextDirection[1] = true;
            //    if (pnt.Y == 1) nextDirection[2] = true;
            //    if (pnt.Y == 10) nextDirection[3] = true;
            //    if (OldPnt.X == 0 && OldPnt.Y == 0) OldPnt = pnt;
            //    for (int i = 1; i <= 4; i++)
            //    {
            //        newpnt = new Point();
            //        if (i == 1 && !nextDirection[i - 1]) newpnt = new Point(OldPnt.X - 1, OldPnt.Y);
            //        if (i == 2 && !nextDirection[i - 1]) newpnt = new Point(OldPnt.X + 1, OldPnt.Y);
            //        if (i == 3 && !nextDirection[i - 1]) newpnt = new Point(OldPnt.X, OldPnt.Y - 1);
            //        if (i == 4 && !nextDirection[i - 1]) newpnt = new Point(OldPnt.X, OldPnt.Y + 1); 

            //        answ = game.GetPointStatus(newpnt);
            //        if (answ == PointStatus.AWAY)
            //        {
            //            nextDirection[i - 1] = true;
            //            return newpnt;
            //        }
            //        else if (answ == PointStatus.DECKSUCK || answ == PointStatus.SHIPSUCK)
            //        {
            //            OldPnt = newpnt;
            //            return newpnt;
            //        }
            //        else
            //        {
            //            nextDirection[i - 1] = true;
            //        }
            //    }

            //}
            //pnt = new Point().Random;
            //return pnt;
        }


    }
}
