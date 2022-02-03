using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal static class Func
    {
        internal static string[] literals = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        public static string GetAddress(int point)
        {
            if (point % 10 == 0)
            {
                return "j" + (point / 10).ToString();
            }
            return literals[point % 10 - 1] + (point / 10 + 1).ToString(); 
        }

        public static int GetPoint(string addr)
        {
            string lit = addr.Substring(0, 1);
            int i;
            for(i = 0; i < literals.Length; i++)
            {
                if (literals[i] == lit.ToLower()) break;
            }
            if (i == literals.Length) return 0;

            string num = addr.Substring(1, addr.Length - 1);
            int numaddr;

            if (!int.TryParse(num, out numaddr)) return 0;

            if (numaddr <=0 || numaddr > 10) return 0;

            return 10 * (numaddr - 1) + i + 1;

        }
    }
}
