using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_20212202_MQ7GIA.UI.ViewModel
{
    public class WaterSharingWindowViewModel : ObservableRecipient
    {
        public GameLogic Logic { get; set; }
        public ObservableCollection<Player> PlayersWithoutFirst { get; set; }
        public BoardWindow boardWindow { get; set; }
        WaterSharingWindow window;
        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set 
            { 
                SetProperty(ref selectedPlayer, value);
                (ShareCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public void SetupLogic(GameLogic logic, BoardWindow boardWindow)
        {
            this.Logic = logic;
            this.PlayersWithoutFirst = new ObservableCollection<Player>(logic.Players);
            PlayersWithoutFirst.Remove(PlayersWithoutFirst.Where(x=>x.TurnOrder == 1).SingleOrDefault());
            this.boardWindow = boardWindow;
        }
        public ICommand ShareCommand { get; set; }
        public void ConfirmWaterSharing()
        {
            // implement water change
            try
            {
                Logic.WaterSharing(Logic.Players, selectedPlayer);
                window.Close();
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            
        }
        public void ShowWindow()
        {
            try
            {
                window = new WaterSharingWindow(this);
                window.ShowDialog();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public WaterSharingWindowViewModel()
        {
            ShareCommand = new RelayCommand(
                () => ConfirmWaterSharing(),
                () => SelectedPlayer != null
                );
        }

        public void RefreshPlayers(List<Player> players)
        {
            this.PlayersWithoutFirst = new ObservableCollection<Player>(players);
            PlayersWithoutFirst.Remove(PlayersWithoutFirst.Where(x => x.TurnOrder == 1).SingleOrDefault());
        }
    }
}
