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
    public class MainWindowViewModel : ObservableRecipient
    {
        public RelayCommand StartGame { get; set; }
        public RelayCommand OpenOptions { get; set; }
        public static Sound Sound { get; set; }

        private static void StartGameSequence()
        {
            GameLogic logic = new GameLogic(Sound);
            new BoardWindow(logic, Sound).ShowDialog();
        }
        private void OptionsWindow(Sound sound)
        {
            OptionsWindow window = new OptionsWindow(sound);
            window.ShowDialog();
        }

        public MainWindowViewModel()
        {
            Sound = new Sound();
            StartGame = new RelayCommand(StartGameSequence);
            OpenOptions = new RelayCommand(() => OptionsWindow(Sound));
        }
    }
}
