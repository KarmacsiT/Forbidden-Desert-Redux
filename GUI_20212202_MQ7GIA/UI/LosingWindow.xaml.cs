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
    /// Interaction logic for LosingWindow.xaml
    /// </summary>
    public partial class LosingWindow : Window
    {
        public Sound Sound { get; set; }
        public LosingWindow(Sound sound)
        {
            InitializeComponent();
            Sound = sound;
            Sound.PlaySound("zapsplat_multimedia_game_sound_negative_big_lose_buzz_with_descending_fail_tone_78340.mp3");
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Sound.PlayMusic("Scarface - Bolivia Theme.mp3");
        }
    }
}
