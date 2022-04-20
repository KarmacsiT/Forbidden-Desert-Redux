using GUI_20212202_MQ7GIA.Logic;
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
        public BoardWindow(GameLogic logic, Sound sound)
        {
            InitializeComponent();
            display.SetupModel(logic);
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
    }
}
