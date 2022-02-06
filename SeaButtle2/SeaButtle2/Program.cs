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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Field field = new Field();
            new GamePresenter().PrintAreaWithShips(field.field);
            Console.ReadKey();
        }
    }
}
