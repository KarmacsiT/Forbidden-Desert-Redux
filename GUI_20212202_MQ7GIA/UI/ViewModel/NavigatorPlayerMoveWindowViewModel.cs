using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using GUI_20212202_MQ7GIA.UI.Renderer;
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
    public class NavigatorPlayerMoveWindowViewModel : ObservableRecipient
    {
        public GameLogic Logic { get; set; }
        public Display Display{ get; set; }
        public ObservableCollection<NamedTile> AvailableTiles { get; set; } //of course we exclude the storm out of this.
        public ObservableCollection<Player> AvailablePlayers { get; set; } //in case there are other players on the tile
        public BoardWindow boardWindow { get; set; }
        NavigatorPlayerMoveWindow window;
        public int TurnOrder { get; set; }
        private Sound sound = new Sound();
        private NamedTile selectedTile;
        public NamedTile SelectedTile
        {
            get { return selectedTile; }
            set
            {
                SetProperty(ref selectedTile, value);
                (MoveCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                SetProperty(ref selectedPlayer, value);
                (MoveCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public void SetupLogic(GameLogic logic, Display display, BoardWindow boardWindow)
        {
            this.Logic = logic;
            this.Display = display;
            this.boardWindow = boardWindow;
        }
        public ICommand MoveCommand { get; set; }
        public void ConfirmMove()
        {
            try
            {
                string validationMessage = Logic.NavigatorMovingPlayer(SelectedTile.X, SelectedTile.Y,SelectedPlayer);
                if (validationMessage == "validMove")
                {
                    boardWindow.UpdateBoardViewModel();
                    boardWindow.UpdateItemCardDisplay();
                    Display.InvalidateVisual();
                }
                else if (validationMessage is "blocked")
                {
                    throw new Exception("Hint: You can't move to this tile because it is blocked.");
                }
                else if (validationMessage is "currentBlocked")
                {
                    throw new Exception("Hint: You can't move from this tile because it is blocked. Remove the double sand first.");
                }
                else if (validationMessage is "outOfActions")
                {
                    throw new Exception("Hint: You can't move anymore because you are out of actions.");
                }
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public void ShowWindow()
        {
            try
            {
                if (AvailablePlayers.Count > 0 && AvailableTiles.Count > 0)
                {
                    window = new NavigatorPlayerMoveWindow(this);
                    window.ShowDialog();
                }
                else if (AvailableTiles.Count == 0 && AvailablePlayers.Count > 0)
                {
                    throw new Exception("There are no available tiles for the players to go move them to.");
                }
                else
                {
                    //this has to be implemented here, otherwise the window would open unnecessarily
                    throw new Exception("There are no available players around you in 3 radius.");

                }
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
        }

        public NavigatorPlayerMoveWindowViewModel()
        {
            MoveCommand = new RelayCommand(
                () => ConfirmMove(),
                () => SelectedTile != null && SelectedPlayer != null
                );
        }
        public void ConvertListToObservable(List<NamedTile> availableTiles, List<Player> availablePlayers)
        {
            AvailableTiles = new ObservableCollection<NamedTile>(availableTiles);
            AvailablePlayers = new ObservableCollection<Player>(availablePlayers);
            SelectedTile = null; 
            SelectedPlayer = null;
        }
    }
}
