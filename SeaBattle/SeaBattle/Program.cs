using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //Console.BackgroundColor = ConsoleColor.White;
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //Console.WriteLine("\u2592"); //Поле
            //Console.WriteLine("\u2588"); // Корабль
            //Console.WriteLine("\u25CF"); // мимо

            //Console.WriteLine(" ABCDEFGHIJ");
            //Console.WriteLine("1\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592");
            //Console.WriteLine("2\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592");
            //Console.WriteLine("3\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592");
            //Console.WriteLine("4\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592");
            //Console.WriteLine("5\u2592\u2592\u25CF\u25CF\u2592\u2592\u2592\u2592\u2592\u2592");
            //Console.WriteLine("6\u2588\u2588XX\u2592\u2592\u2592\u2592\u2592\u2592");
            //Console.WriteLine("7\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592\u2592");

            Shiparea shiparea = new Shiparea();
            shiparea.CreateShipArea();

            shiparea.PrintArea(true);



            Console.ReadKey();
        }
    }
}
