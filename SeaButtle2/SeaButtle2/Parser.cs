using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public class Parser
    {
        internal List<string> literals = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        public Parser()
        {
            
        }
        public Point GetPoint(string address)
        {
            Point point = new Point();

            if (address == null || address.Length < 2 || address.Length > 3) return point;

            string letter = address.Substring(0, 1).ToLower();
            string number = address.Substring(1, address.Length - 1);

            if (!literals.Contains(letter)) return point;
            int inumber;

            if (!int.TryParse(number, out inumber)) return point;
            if (inumber <= 0 || inumber > 10) return point;

            point.X = literals.IndexOf(letter) + 1;
            point.Y = inumber;

            return point;
        }

        public string GetAddress(Point point)
        {
            if (point == null || point.isNullPoint) return null;

            try
            {
                return literals[point.X - 1] + point.Y.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
