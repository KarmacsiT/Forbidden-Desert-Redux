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
        public Sound Sound { get; set; }
        public int NumberOfActions { get; set; }

        private void StartGameSequence()
        {
            GameLogic logic = new GameLogic(Sound);
            GameSetupWindow gameSetup = new GameSetupWindow(logic,Sound);
            gameSetup.ShowDialog();
        }
        private void OptionsWindow(Sound sound)
        {
            OptionsWindow window = new OptionsWindow(sound);
            window.ShowDialog();
        }

        public MainWindowViewModel()
        {
            Sound = new Sound();
            Sound.PlayMusic("Scarface - Bolivia Theme.mp3");
            StartGame = new RelayCommand(StartGameSequence);
            OpenOptions = new RelayCommand(() => OptionsWindow(Sound));
        }
    }
}
