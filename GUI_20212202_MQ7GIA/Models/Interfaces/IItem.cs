using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Models
{
    public interface IItem // To be continued
    {
        //This is a Gadget which you can use anytime (jetpack)
        string Name { get; set; }
        void Ability(); 
    }
}
