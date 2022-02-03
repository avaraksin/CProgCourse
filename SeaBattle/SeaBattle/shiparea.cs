using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal class Shiparea
    {
        private List<Ship> AllShips;
        private int[] printarea;
        
        internal Shiparea()
        {
            AllShips = new List<Ship>();
            printarea = new int[101]; // 0 не учитываем
        }

        public void CreateShipArea()
        {
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Ship ship = new Ship(4 - i + 1);
                    bool isOn;
                    do
                    {
                        int orent = new Random().Next(0, 2);
                        int pnt = new Random().Next(101);
                        ship.orientation = orent;
                        isOn = ship.SetShipAddr(pnt);
                        if (isOn)
                        {
                            foreach (Ship ships in AllShips)
                            {
                                foreach (int deck in ship.shiparea)
                                {
                                    isOn = isOn && !ships.shiparea.Contains(deck);
                                    isOn = isOn && !ships.freeField.Contains(deck);
                                }
                            }
                        }
                    } while (isOn == false);
                    AllShips.Add(ship);
                    SetShipInArea(ship);
                }
            }

        }

        public void PrintArea(bool shipshow = false)
        {
            string[] fieldsp = new string[] { "\u2592", "\u2588", "\u25CF", "X" };
            string[] fieldsb = new string[] { "\u2592", "\u2592", "\u25CF", "X" };

            Console.WriteLine("  ABCDEFGHIJ");
            for(int i = 1; i <= 10; i++)
            {
                string row = i.ToString();
                if (i < 10) row += " ";
                for(int j = 1; j <= 10; j++)
                {
                    if (shipshow)
                        row += fieldsp[printarea[j + (i - 1) * 10]];
                    else
                        row += fieldsb[printarea[j + (i - 1) * 10]];
                }
                Console.WriteLine(row);
            }

        }

        private void SetShipInArea(Ship ship)
        {
            for(int i = 0; i < ship.decksnum; i++)
            {
                printarea[ship.shiparea[i]] = 1;
            }
        }
    }
}
