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
    public class TunnelTeleportWindowViewModel : ObservableRecipient
    {
        public GameLogic Logic { get; set; }
        public Display Display { get; set; }
        public ObservableCollection<TunnelTile> TunnelsWithoutFirst { get; set; }
        public BoardWindow boardWindow { get; set; }
        TunnelTeleportWindow window;
        private TunnelTile selectedTunnel;
        public TunnelTile SelectedTunnel
        {
            get { return selectedTunnel; }
            set
            {
                SetProperty(ref selectedTunnel, value);
                (TeleportCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public void SetupLogic(GameLogic logic, Display display, BoardWindow boardWindow)
        {
            this.Logic = logic;
            this.Display = display;
            this.boardWindow = boardWindow;
        }
        public ICommand TeleportCommand { get; set; }
        public void ConfirmTeleport()
        {
            // implement water change
            try
            {
                Logic.TunnelTeleport(Logic.Players, selectedTunnel);
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
                int playerX = Logic.Players.Where(x => x.TurnOrder == 1).SingleOrDefault().X;
                int playerY = Logic.Players.Where(x => x.TurnOrder == 1).SingleOrDefault().Y;
                if (Logic.PlayerOnTunnelTile(playerX,playerY))
                {
                    window = new TunnelTeleportWindow(this);
                    window.ShowDialog();
                    return true;
                }
                else
                {
                    //this has to be implemented here, otherwise the window would open unnecessarily
                    throw new Exception("You're not on a Tunnel tile.");
                    
                }
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            return false;
        }

        public TunnelTeleportWindowViewModel()
        {
            TeleportCommand = new RelayCommand(
                () => ConfirmTeleport(),
                () => SelectedTunnel != null
                );
        }

        public void RefreshTunnels(List<TunnelTile> tunnels)
        {
            int playerX = Logic.Players.Where(x => x.TurnOrder == 1).FirstOrDefault().X;
            int playerY = Logic.Players.Where(x => x.TurnOrder == 1).FirstOrDefault().Y;
            List<TunnelTile> temp = new List<TunnelTile>(tunnels);
            //Remove that one which is on the player
            temp.Remove(temp.Where(x => x.X == playerX && x.Y == playerY).SingleOrDefault());
            //I also remove the ones which are not discovered yet
            temp = temp.Where(x => x.IsDiscovered == true).ToList();
            TunnelsWithoutFirst = new ObservableCollection<TunnelTile>(temp);
            SelectedTunnel = null;
        }
    }
}
