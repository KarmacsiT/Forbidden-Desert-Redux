﻿using GUI_20212202_MQ7GIA.Logic;
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
        JetPackWindow window;
        public int TurnOrder { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        private Tile selectedTile;
        public Tile SelectedTile
        {
            get { return selectedTile; }
            set
            {
                SetProperty(ref selectedTile, value);
                (TeleportCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                SetProperty(ref selectedPlayer, value);
                //This doesn't depend on if the button can be pushed for teleporting
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
            // implement water change
            try
            {
                Logic.MovePlayer(X, Y, Logic.Players, SelectedPlayer);
                Display.InvalidateVisual();
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
        public void GoAlone()
        {
            // implement water change
            try
            {
                Logic.MovePlayer(X, Y, Logic.Players, null);
                Display.InvalidateVisual();
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
                if (AvailableTiles.Count > 0)
                {
                    window = new JetPackWindow(this);
                    window.ShowDialog();
                    return true;
                }
                else
                {
                    //this has to be implemented here, otherwise the window would open unnecessarily
                    throw new Exception("There is no unblocked tile around you.");

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
                () => SelectedTile != null
                );
            AloneCommand = new RelayCommand(
                () => GoAlone(),
                () => SelectedTile != null
                );
        }

        public void ConvertListToObservable(List<Tile> tiles, List<Player> players)
        {
            AvailableTiles = new ObservableCollection<Tile>(tiles);
            AvailablePlayers = new ObservableCollection<Player>(players);
            SelectedTile = null; // this is also done because otherwise there may be memory leakages from previous dune blaster card actions
            SelectedPlayer = null;
        }
    }
}
