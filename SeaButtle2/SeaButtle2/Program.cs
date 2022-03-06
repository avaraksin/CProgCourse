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
            // todo
            var player = new ManualPlayer(new Field());
            var aiPlayer = new EasyBot(new Field());
            var game = new Game(player, aiPlayer);
            while(!game.Over) {
                var input = Console.ReadLine();
                string result;
                try
                {
                    result = game.ParseAndExecuteCommand(input);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                
                Console.WriteLine(result);
            }
        }
    }
}
