using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.UI;
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
        public BoardWindow(GameLogic logic, Sound sound, GameSetupWindow setupWindow)
        {
            InitializeComponent();
            display.SetupLogic(logic);
            display.SetupGameSetup(setupWindow);
            Sound = sound;
            partsCollected.SetupModel(logic);
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
            else if (e.Key == Key.E) // E
            {
                invalidate = display.Excavate();
            }

            if(invalidate == true)
            {
                display.InvalidateVisual();
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
