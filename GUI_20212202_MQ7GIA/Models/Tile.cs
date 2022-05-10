using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public class Tile : ITile //General implementation of tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDiscovered { get; set; }
    }
}
