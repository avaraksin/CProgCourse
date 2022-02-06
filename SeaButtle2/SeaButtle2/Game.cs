using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    
    public class Game
    {
        public bool isGameOn;

        public Game()
        {
            isGameOn = false;
        }

        public void StartGame()
        {
            isGameOn = true;
            Player hum = new Player();
            BilPlayer bil = new BilPlayer();
        }


    }
}
