using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public interface ITile
    {
        int X { get; set; }
        int Y { get; set; }
        bool IsDiscovered { get; set; }
    }
}
