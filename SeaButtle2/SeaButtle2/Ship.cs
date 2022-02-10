using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    public abstract class Ship
    {
        public Point startPoint { get; set; }
        public Orientation orientation { get; set; }
        public string Name { get; set; }
        public int decksNum { get; set; }
        public bool Sunk { get; set; }
        public DeckStatus[] Decks { get; set; }

        public Ship(Point point, Orientation orientation, string name, int decksNum) {
        
        }

        /// <summary>
        /// Shot принять выстрел
        /// </summary>
        public void Shot()
        {

        }
    }

    /// <summary>
    /// TypicalShip - корабль как корабль
    /// </summary>
    public class TypicalShip : Ship
    {
        // TODO
    }
}
