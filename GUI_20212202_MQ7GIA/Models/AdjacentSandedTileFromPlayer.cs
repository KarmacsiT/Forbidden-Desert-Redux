using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public class AdjacentSandedTileFromPlayer
    {
        public string Name { get; set; } //This means the position of the player (left, right, up, down... and the tile you're standing on)
        public int X { get; set; }
        public int Y { get; set; }
    }
}
