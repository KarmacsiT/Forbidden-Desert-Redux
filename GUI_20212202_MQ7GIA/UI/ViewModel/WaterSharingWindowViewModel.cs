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
            this.boardWindow = boardWindow;
        }
        public ICommand ShareCommand { get; set; }
        public void ConfirmWaterSharing()
        {
            // implement water change
            try
            {
                Logic.WaterSharing(Logic.Players, selectedPlayer);
                boardWindow.UpdateBoardViewModel();
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

        public void RefreshPlayers(GameLogic logic)
        {
            PlayersWithoutFirst = null; // avoiding memory leak
            RoleName role = logic.players.Where(p => p.TurnOrder == 1).SingleOrDefault().PlayerRoleName;
            if (role == RoleName.WaterCarrier)
            {
                this.PlayersWithoutFirst = new ObservableCollection<Player>(logic.GetPlayersOnAdjacentAndYourTile(1));
            }
            else this.PlayersWithoutFirst = new ObservableCollection<Player>(logic.GetPlayersOnSameTile(1));
        }
    }
}
