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
    public class ClimberTakingPlayerViewModel : ObservableRecipient
    {
        public GameLogic Logic { get; set; }
        public Display Display { get; set; }
        public ObservableCollection<Player> AvailablePlayers { get; set; } //in case there are other players on the tile
        public BoardWindow boardWindow { get; set; }
        ClimberTakingPlayerWindow window;
        public int X { get; set; }
        public int Y { get; set; }
        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                SetProperty(ref selectedPlayer, value);
                (TogetherCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public void SetupLogic(GameLogic logic, Display display, BoardWindow boardWindow)
        {
            this.Logic = logic;
            this.Display = display;
            this.boardWindow = boardWindow;
        }
        public ICommand TogetherCommand { get; set; }
        public ICommand AloneCommand { get; set; }
        public void GoTogether()
        {
            try
            {
                bool invalidate = false;
                if (SelectedPlayer != null)
                {
                    invalidate = Display.MoveMultiPlayer(X, Y, SelectedPlayer);
                    if (invalidate)
                    {
                        boardWindow.UpdateBoardViewModel();
                        boardWindow.UpdateItemCardDisplay();
                        Display.InvalidateVisual();
                    }
                }
                else throw new Exception("You selected nobody to come with you. Please try again.");

            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.DialogResult = true;
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public void GoAlone()
        {
            bool invalidate = false;
            try
            {
                invalidate = Display.MoveThePlayer(X, Y);
                if (invalidate)
                {
                    boardWindow.UpdateBoardViewModel();
                    boardWindow.UpdateItemCardDisplay();
                    Display.InvalidateVisual();
                }
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.DialogResult = true;
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public bool ShowWindow()
        {
            try
            {
                window = new ClimberTakingPlayerWindow(this);
                window.ShowDialog();
                if (window.DialogResult == true)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            return false;
        }

        public ClimberTakingPlayerViewModel()
        {
            TogetherCommand = new RelayCommand(
                () => GoTogether(),
                () => SelectedPlayer != null
                );
            AloneCommand = new RelayCommand(
                () => GoAlone()
                );
        }

        public void ConvertListToObservable(List<Player> players)
        {
            AvailablePlayers = new ObservableCollection<Player>(players);
            SelectedPlayer = null; //Because of avoiding memory leakages, we reset the value.
        }
    }
}
