using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using GUI_20212202_MQ7GIA.UI;
using GUI_20212202_MQ7GIA.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_20212202_MQ7GIA
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        public Sound Sound { get; set; }
        List<Player> players = new List<Player>();
        List<string> colors = new List<string>();
        public BoardWindow(GameLogic logic, Sound sound, GameSetupWindow setupWindow)
        {
            InitializeComponent();
            //playerGeneration
            if (setupWindow.PlayerThreeName is null && players.Count == 0)
            {
                players.Add(logic.PlayerInit(setupWindow.PlayerOneName, 1, players));
                players.Add(logic.PlayerInit(setupWindow.PlayerTwoName, 2, players));
            }

            else if (players.Count == 0)
            {
                players.Add(logic.PlayerInit(setupWindow.PlayerOneName, 1, players));
                players.Add(logic.PlayerInit(setupWindow.PlayerTwoName, 2, players));
                players.Add(logic.PlayerInit(setupWindow.PlayerThreeName, 3, players));
            }

            //Create some logic that matches the role to the piece color

            foreach (Player player in players)
            {
                switch (player.PlayerRoleName)
                {
                    case RoleName.Archeologist:
                        colors.Add("red_piece.png");
                        break;
                    case RoleName.Climber:
                        colors.Add("black_piece.png");
                        break;
                    case RoleName.Explorer:
                        colors.Add("green_piece.png");
                        break;
                    case RoleName.Meteorologist:
                        colors.Add("white_piece.png");
                        break;
                    case RoleName.Navigator:
                        colors.Add("yellow_piece.png");
                        break;
                    case RoleName.WaterCarrier:
                        colors.Add("blue_piece.png");
                        break;
                    default: break;
                }
            }
            display.SetupLogic(logic, players, colors);
            Sound = sound;
            partsCollected.SetupModel(logic, players);

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            display.Resize(new Size(boardDisplay.ActualWidth, boardDisplay.ActualHeight));
            display.InvalidateVisual();
            partsCollected.Resize(new Size(partsCollectedDisplay.ActualWidth, partsCollectedDisplay.ActualHeight));
            partsCollected.InvalidateVisual();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            PauseWindow pauseWindow = new PauseWindow(Sound);
            pauseWindow.ShowDialog();
            if (pauseWindow.DialogResult == true)
            {
                this.Close();
            }
        }
        private void StormMove(object sender, RoutedEventArgs e)  //only for testing
        {
            display.MoveTheStorm(0, 1);
            display.InvalidateVisual();
        }

        private void KeyBoardUsed(object sender, KeyEventArgs e)
        {
            bool invalidate = false;
            bool partInvalidate = false;
            if (e.Key == Key.NumPad7)    // left and up
            {
                invalidate = display.MoveThePlayer(-1, -1);
            }       
            else if (e.Key == Key.NumPad9)    // right and up
            {
                invalidate = display.MoveThePlayer(1, -1);
            }
            else if (e.Key == Key.NumPad1)    // left and down
            {
                invalidate = display.MoveThePlayer(-1, 1);
            }
            else if (e.Key == Key.NumPad3)    // right and down
            {
                invalidate = display.MoveThePlayer(1, 1);
            }
            else if (e.Key == Key.Up || e.Key == Key.NumPad8)      // up
            {
                invalidate = display.MoveThePlayer(0, -1);
            }
            else if (e.Key == Key.Left || e.Key == Key.NumPad4)     // left
            {
                invalidate = display.MoveThePlayer(-1, 0);
            }
            else if (e.Key == Key.Down || e.Key == Key.NumPad2)      // down
            {
                invalidate = display.MoveThePlayer(0, 1);
            }
            else if (e.Key == Key.Right || e.Key == Key.NumPad6)    // right
            {
                invalidate = display.MoveThePlayer(1, 0);
            }
            else if (e.Key == Key.R) // R
            {
                invalidate = display.RemoveSand();
            }
            else if(e.Key == Key.E)
            {
                invalidate = display.Excavate();
            }
            else if(e.Key == Key.P)
            {
                partInvalidate = partsCollected.ItemPickUp();
            }

            if(invalidate == true)
            {
                display.InvalidateVisual();
            }
            if (partInvalidate)
            {
                partsCollected.InvalidateVisual();
            }
        }

        private void ExcavateClick(object sender, KeyEventArgs e)
        {

        }

        private void EndTurn(object sender, RoutedEventArgs e)
        {
            display.EndTurn();
            // draw cards, (and move storm, ...) 
        }
    }
}
