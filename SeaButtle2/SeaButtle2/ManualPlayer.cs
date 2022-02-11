using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    internal class ManualPlayer : Player
    {
        public override PointStatus DoMove(Player player, Point point)
        {
            Console.WriteLine("MANUAL");
            //return GamePresenter.GetStep();
            return PointStatus.DECKSUCK;
        }
    }
}
