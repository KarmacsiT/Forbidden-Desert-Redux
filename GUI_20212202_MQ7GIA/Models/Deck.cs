using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public class Deck
    {
        public List<ItemCard> AvailableItemCards { get; set; }
        public List<StormCard> AvailableStormCards { get; set; }
    }
    public class ItemCard
    {
        public ItemCard(string name, bool isDiscarded, bool inPlayerHand, string display)
        {
            Name = name;
            IsDiscarded = isDiscarded;
            InPlayerHand = inPlayerHand;
            Display = display;
        }

        public string Name { get; set; }
        public bool IsDiscarded { get; set; }
        public bool InPlayerHand { get; set; }        
        public string Display { get; set; }
    }

    public class StormCard
    {
        public StormCard(string name, bool isDiscarded, int xMove, int yMove)
        {
            Name = name;
            IsDiscarded = isDiscarded;
            XMove = xMove;
            YMove = yMove;
        }

        public string Name { get; set; }
        public bool IsDiscarded { get; set; }
        public int XMove { get; set; } // Where the storm brings the storm in X direction (-1 or 1)
        public int YMove { get; set; } // Where the storm brings the storm in Y direction (-1 or 1)
    }
}
