using System;
using System.Windows;
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
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool GameOn = true;

            while (GameOn)
            {
                Shiparea hum = new Shiparea();
                Shiparea bil = new Shiparea();
                hum.CreateShipArea();
                bil.CreateShipArea();
                int retanswer = 0;
                Strat.SetStratOff();
                int pnt = 0;

                while (GameOn && !bil.GameOver && !hum.GameOver)
                {
                    do
                    {
                        Console.WriteLine();
                        Console.Write("Ваш ход: ");
                        string answ = Console.ReadLine();
                        if (answ.ToLower() == "status")
                        {
                            Console.WriteLine("Ваша карта:");
                            hum.PrintArea(true); 
                            retanswer = 1;
                            continue;
                        }
                        else if (answ.ToLower() == "exit")
                        {
                            GameOn = false;
                            Console.WriteLine("Ваша карта: ");
                            hum.PrintArea(true);
                            Console.WriteLine("Карта компьютера: ");
                            bil.PrintArea(true);
                            retanswer = 0;
                            break;
                        }
                        else
                        {
                            retanswer = bil.CheckShoot(answ);
                            if (retanswer == -1)
                            {
                                Console.WriteLine("Неправильный ввод!");
                                retanswer = 1;
                            }
                            else if (retanswer == 0)
                            {
                                Console.WriteLine("Мимо!");
                                bil.PrintArea();
                            }
                            else
                            {
                                if (bil.GetbitShipStatus == 1)
                                    Console.WriteLine("Ранен!");
                                else
                                    Console.WriteLine("Потоплен!");

                                bil.PrintArea();
                            }

                        }
                    } while (retanswer > 0);

                    if (GameOn)
                    {
                        do
                        {
                            Console.WriteLine();
                            do
                            {
                                pnt = Strat.SetNewPoint(pnt);
                                //pnt = new Random().Next(1, 101);
                                retanswer = hum.CheckShoot(pnt);
                            } while (retanswer == -1);
                            Console.WriteLine("Ход компьютера: " + Func.GetAddress(pnt));
                            if (retanswer == 0)
                            {
                                Console.WriteLine("Мимо!");
                            }
                            else
                            {
                                if (hum.GetbitShipStatus == 1)
                                {
                                    Console.WriteLine("Ранен!");
                                    Strat.SetStratOn(hum);
                                }
                                else
                                {
                                    Console.WriteLine("Потоплен!");
                                    Strat.SetStratOff();
                                }
                            }

                        } while (retanswer > 0);
                    }
                }

               if (GameOn)
                {
                    if (bil.GameOver)
                        Console.WriteLine("Вы выграли! Поздравляю!\n");
                    else
                        Console.WriteLine("Компьтер выграл.\nВам нужно еще потренироваться!\n");

                    Console.Write("Играем еще (Y/N): ");
                    String answ = Console.ReadLine();
                    if (answ.ToLower() != "y") GameOn = false;
                }
            }

            Console.ReadKey();
        }
    }
}
