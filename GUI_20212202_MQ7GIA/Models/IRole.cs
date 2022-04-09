using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public enum RoleName { Archeologist, Climber, Explorer, Meteorologist, Navigator, WaterCarrier }
    public interface IRole
    {
        RoleName PlayerRoleName { get; set; }
        int WaterLevel { get; set; }
        string AbilityDescription { get; set; }
        bool[] AbilityList { get; set; }
        string ActionDescription { get; set; }
    }
}
