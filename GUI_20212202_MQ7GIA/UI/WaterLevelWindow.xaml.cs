using GUI_20212202_MQ7GIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_20212202_MQ7GIA
{
    /// <summary>
    /// Interaction logic for WaterLevelWindow.xaml
    /// </summary>
    public partial class WaterLevelWindow : Window
    {
        public WaterLevelWindow(Player player)
        {
            InitializeComponent();
            WaterLevelNeedle.SetupPlayer(player);
            switch (player.PlayerRoleName)
            {
                case RoleName.Archeologist:
                    Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Charachter Cards", "Archeologist.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case RoleName.Climber:
                    Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Charachter Cards", "Climber.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case RoleName.Explorer:
                    Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Charachter Cards", "Explorer.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case RoleName.Meteorologist:
                    Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Charachter Cards", "Meteorologist.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case RoleName.Navigator:
                    Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Charachter Cards", "Navigator.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case RoleName.WaterCarrier:
                    Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Charachter Cards", "Water Carrier.png"), UriKind.RelativeOrAbsolute)));
                    break;
                default: break;
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //Makes the window dragable
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
