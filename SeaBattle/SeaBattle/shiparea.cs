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
        public int bitShips {get; private set;}

        public bool GameOver
        {
            get
            {
                return bitShips == AllShips.Count;
            }
        }

        public int GetbitShipStatus { get; private set; }
        
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
            bitShips = 0;

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

        internal int ChekShoot(String spnt)
        {
            return CheckShoot(Func.GetPoint(spnt));
        }

        internal int CheckShoot(int pnt) // -1 - неправильный ход, 0 - не попал (переход хода), 1 - попал (ход сохраняется).
        {
            if (pnt < 1 || pnt > 100) return -1;

            if (printarea[pnt] > 1) return -1;

            if (printarea[pnt] == 0)
            {
                printarea[pnt] = 2;
                return 0;
            }

            for(int i = 0; i < AllShips.Count; i++)
            {
                if (AllShips[i].shiparea.Contains(pnt))
                {
                    if (AllShips[i].SetShipStatus(pnt) )
                    {
                        foreach (int ind in AllShips[i].freeField)
                        {
                            printarea[ind] = 2;
                        }
                        bitShips++;
                        GetbitShipStatus = 2;
                        break;
                    }
                    GetbitShipStatus = 1;
                    break;
                }
            }

            printarea[pnt] = 3;

            return 1;
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
