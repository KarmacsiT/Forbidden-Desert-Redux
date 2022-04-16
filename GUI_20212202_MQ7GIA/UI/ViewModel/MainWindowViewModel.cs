using GUI_20212202_MQ7GIA.Logic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.UI.ViewModel
{
    class MainWindowViewModel : ObservableRecipient
    {
        public RelayCommand StartGame { get; set; }

        private static void StartGameSequence()
        {
            GameLogic logic = new GameLogic();
            new BoardWindow(logic).ShowDialog();
        }

        public MainWindowViewModel()
        {
            StartGame = new RelayCommand(StartGameSequence);
        }
    }
}
