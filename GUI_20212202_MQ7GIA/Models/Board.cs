using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public class Board
    {
        TunnelTile[] TunnelTiles;
        enum ShelterType { Empty, Friendly, FriendlyQuest, Hostile }
        AirShipClueTile[] AirShipClueTiles;
        OasisMirageTile[] OasisMirageTiles;
        LaunchPadTile LaunchPadTile;
        List<DiscardedStormCard> DiscardedStormCards;
        List<DiscardedItemCard> DiscardedItemCards;

    }

    public class LaunchPadTile : ITile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDiscovered { get; set; }
    }

    public class OasisMirageTile : ITile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDiscovered { get; set; }
    }

    public class AirShipClueTile : ITile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDiscovered { get; set; }
    }

    public class TunnelTile : ITile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDiscovered { get; set; }
    }
    
}
