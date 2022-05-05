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
    /// Interaction logic for PauseWindow.xaml
    /// </summary>
    public partial class PauseWindow : Window
    {
        public Sound Sound { get; set; }
        public PauseWindow(Sound sound)
        {
            InitializeComponent();
            Sound = sound;
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Sound.PlaySound("Scarface - Bolivia Theme.mp3");
        }

        private void Options(object sender, RoutedEventArgs e)
        {
            OptionsWindow window = new OptionsWindow(Sound);
            window.ShowDialog();
        }
    }
}
