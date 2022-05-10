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
    public class DuneBlasterWindowViewModel : ObservableRecipient
    {
        public GameLogic Logic { get; set; }
        public ObservableCollection<AdjacentSandedTileFromPlayer> AdjacentTilesFromPlayers { get; set; } //okay this also contains the tile which the player is on
        public BoardWindow boardWindow { get; set; }
        DuneBlasterWindow window;
        private AdjacentSandedTileFromPlayer selectedTile;
        public AdjacentSandedTileFromPlayer SelectedTile
        {
            get { return selectedTile; }
            set
            {
                SetProperty(ref selectedTile, value);
                (RemoveCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public void SetupLogic(GameLogic logic, BoardWindow boardWindow)
        {
            this.Logic = logic;
            this.boardWindow = boardWindow;
        }
        public ICommand RemoveCommand { get; set; }
        public void ConfirmRemoveSand()
        {
            try
            {
                Logic.RemoveSandByCoordinateNoAction(SelectedTile.X, SelectedTile.Y, Logic.Players);
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
        public bool ShowWindow()
        {
            try
            {
                if (AdjacentTilesFromPlayers.Count > 0)
                {
                    window = new DuneBlasterWindow(this);
                    window.ShowDialog();
                    return true;
                }
                else
                {
                    //this has to be implemented here, otherwise the window would open unnecessarily
                    throw new Exception("There is no sand around you in 1 radius.");
                    
                }
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            return false;
        }

        public DuneBlasterWindowViewModel()
        {
            RemoveCommand = new RelayCommand(
                () => ConfirmRemoveSand(),
                () => SelectedTile != null
                );
        }
        public void ConvertListToObservable(List<AdjacentSandedTileFromPlayer> adjacentSandedTileFromPlayers)
        {
            AdjacentTilesFromPlayers = new ObservableCollection<AdjacentSandedTileFromPlayer>(adjacentSandedTileFromPlayers);
            SelectedTile = null; // this is also done because otherwise there may be memory leakages from previous dune blaster card actions
        }
    }
}
