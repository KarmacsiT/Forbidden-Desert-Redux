using GUI_20212202_MQ7GIA.Logic;
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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private Sound Sound{get;set;}
        public OptionsWindow(Sound sound)
        {
            InitializeComponent();
            OptionsWindowViewModel vm = new OptionsWindowViewModel();
            Sound = sound;
            vm.SetupSound(sound);
            DataContext = vm;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ChangeVolume(object sender, RoutedEventArgs e)
        {
            Sound.PlaySound("Chimes.wav");
        }
        
    }
}
