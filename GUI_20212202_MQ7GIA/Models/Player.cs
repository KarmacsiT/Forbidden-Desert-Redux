using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public class Player : IRole
    {
        public string PlayerName { get; set; } // name of the player
        public int X { get; set; }
        public int Y { get; set; }
        public List<ItemCard> Cards { get; set; }
        public int NumberOfActions { get; set; }
        public int TurnOrder { get; set; }
        public RoleName PlayerRoleName { get; set; }
        public int WaterLevel { get; set; }
        public string AbilityDescription { get; set; }
        public bool[] AbilityList { get; set; }
        public string ActionDescription { get; set; }
    }
}
