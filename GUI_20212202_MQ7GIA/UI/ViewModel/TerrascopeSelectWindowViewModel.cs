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
    public class TerrascopeSelectWindowViewModel : ObservableRecipient
    {
        public GameLogic Logic { get; set; }
        public TerraScopeRenderer Renderer { get; set; }
        public ObservableCollection<ITile> UndiscoveredTiles { get; set; }
        public BoardWindow boardWindow { get; set; }
        TerrascopeWindow window;
        TerrascopeSelectorWindow selectorWindow;
        private ITile selectedTile;
        public ITile SelectedTile
        {
            get { return selectedTile; }
            set
            {
                SetProperty(ref selectedTile, value);
                (PeekCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public void SetupLogic(GameLogic logic, TerraScopeRenderer renderer, BoardWindow boardWindow)
        {
            this.Logic = logic;
            this.Renderer = renderer;
            this.boardWindow = boardWindow;
        }
        public ICommand PeekCommand { get; set; }
        public void ConfirmPeek()
        {
            try
            {
                Logic.SetPeekTile(SelectedTile);
                selectorWindow.Close();
                window = new TerrascopeWindow(Renderer,Logic);
                window.Show();
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }

        }
        public bool ShowWindow()
        {
            try
            {
                if (UndiscoveredTiles.Count > 0)
                {
                    selectorWindow = new TerrascopeSelectorWindow(this);
                    selectorWindow.ShowDialog();
                }
                else
                {
                    //this has to be implemented here, otherwise the window would open unnecessarily
                    throw new Exception("All the tiles are discovered.");

                }
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            return false;
        }

        public TerrascopeSelectWindowViewModel()
        {
            PeekCommand = new RelayCommand(
                () => ConfirmPeek(),
                () => SelectedTile != null
                );
        }

        public void ConvertListToObservable(List<ITile> undiscoveredTiles)
        {
            UndiscoveredTiles = new ObservableCollection<ITile>(undiscoveredTiles);
            SelectedTile = null;
        }
    }
}
