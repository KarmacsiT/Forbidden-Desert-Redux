using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public enum ShelterVariations { Empty, FriendlyQuest, FriendlyWater, Hostile }
    public class Board
    {
        public TunnelTile[] TunnelTiles { get; set; } // 2 tiles
        public AirShipClueTile[] AirShipClueTiles { get; set; } // 8 tiles
        public OasisMirageTile[] OasisMirageTiles { get; set; } // 3 tiles
        public LaunchPadTile LaunchPadTile { get; set; } // 1 tiles
        public ShelterTile[] ShelterTiles { get; set; } //10 tiles
        public List<SandTile> SandTiles { get; set; } //8 tiles initially but can be 24+24 in the end
        public Storm storm { get; set; }
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
        public bool IsDried { get; set; }
    }

    public class AirShipClueTile : ITile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDiscovered { get; set; }
        public char Direction { get; set; }
        public string PartName { get; set; }
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
        public ShelterVariations ShelterType { get; set; }
    }
    public class SandTile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool DoubleSand { get; set; } //this marks if the card has two sand cards
    }

}
