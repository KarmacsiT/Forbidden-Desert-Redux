using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public enum ShelterVariations { Empty, Friendly, FriendlyQuest, FriendlyWater, Hostile }
    public class Board
    {
        TunnelTile[] TunnelTiles;
        AirShipClueTile[] AirShipClueTiles;
        OasisMirageTile[] OasisMirageTiles;
        LaunchPadTile LaunchPadTile;
        List<StormCard> DiscardedStormCards;
        List<ItemCard> DiscardedItemCards;
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
    public class ShelterTile : ITile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDiscovered { get; set; }
        public ShelterVariations ShelterType { get; }
    }

}
