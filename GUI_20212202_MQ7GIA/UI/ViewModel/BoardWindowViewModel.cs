using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.UI.Renderer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.UI.ViewModel
{
    public class BoardWindowViewModel: ObservableRecipient
    {
        public Display Display { get; set; }
        public int TurnsCounter { 
            get { return Display.players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions; }
        }
        public BoardWindowViewModel(Display display)
        {
            Display = display;
        }
    }
}
