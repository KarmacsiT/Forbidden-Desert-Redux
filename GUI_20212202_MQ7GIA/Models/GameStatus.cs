using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public enum Difficulty { Novice, Normal, Elite, Legendary }
    public class GameStatus
    {
        public int NumberOfSandTiles { get; set; }
        public Difficulty GameDifficulty { get; }
        public int StormMeter { get; set; }
        public int NumberOfStormCardsDrawn { get; set; }
    }
}
