using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public interface IRole
    {
        string RoleName { get; set; }
        int WaterLevel { get; set; }
        string AbilityDescription { get; set; }
        bool[] AbilityList { get; set; }
        string ActionDescription { get; set; }
    }
}
