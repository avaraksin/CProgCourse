using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Game game = new Game();
            //game.StartGame();
            //game.PlayGame();
            //Console.ReadKey();


            // все зависимости создать здесь!
            var ai = new BilPlayer();
            var human = new ManualPlayer();

            new Game(ai, human);
            Console.ReadKey();
        }
    }
}
