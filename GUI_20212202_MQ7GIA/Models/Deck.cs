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
        public List<ItemCard> DiscardedItemCards { get; set; }
        public List<StormCard> AvailableStormCards { get; set; }
        public List<StormCard> DiscardedStormCards { get; set; }
    }
    public class ItemCard
    {
        public bool IsDiscarded { get; set; }
        public string Description { get; set; }
        public Action Ability { get; set; }
    }

    public class StormCard
    {
        public bool IsDiscarded { get; set; }
        public int XMove { get; set; } // Where the storm brings the storm in X direction (-1 or 1)
        public int YMove { get; set; } // Where the storm brings the storm in Y direction (-1 or 1)
    }
}
