using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public class GamePresenter
    {
            private string[] fieldsp = new string[] { "\u2592", "\u2588", "\u25CF", "X" };
            private string[] fieldsb = new string[] { "\u2592", "\u2592", "\u25CF", "X" };

        public GamePresenter()
        {
        }

        public string GetStep()
        {
            Console.Write("Ваш ход: ");
            return Console.ReadLine();
        }

        public void PrintAreaWithShips(int[,] field)
        {
            PrintArea(field, fieldsp);
        }
        public void PrintAreaWithoutShips(int[,] field)
        {
            PrintArea(field, fieldsb);
        }

        private void PrintArea(int[,] field, string[] mask)
        {
            Console.WriteLine("  ABCDEFGHIJ");

            string row;

            for (int x = 1; x <= field.GetUpperBound(0); x++)
            {
                row = x.ToString();
                if (x < 10) row += " ";
                for (int y = 1; y <= field.GetUpperBound(1); y++)
                {
                    row += mask[field[x, y]];
                }
                Console.WriteLine(row);
            }
        }
    }
}
