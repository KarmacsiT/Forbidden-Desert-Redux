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
    /// Interaction logic for StormTrackerWindow.xaml
    /// </summary>
    public partial class StormTrackerWindow : Window
    {
        StormTrackerWindowViewModel VM { get; set; }
        public StormTrackerWindow(StormTrackerWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            VM = vm;
            StormCard1Display.Source = vm.SC1;
            StormCard2Display.Source = vm.SC2;
            StormCard3Display.Source = vm.SC3;
            StormCard4Display.Source = vm.SC4;
            StormCard5Display.Source = vm.SC5;
            StormCard6Display.Source = vm.SC6;
        }

        private void ClickCard1(object sender, MouseButtonEventArgs e)
        {
            if (VM.SC1 != null)
            {
                VM.PutStormCard1();
            }
        }
        private void ClickCard2(object sender, MouseButtonEventArgs e)
        {
            if (VM.SC2 != null)
            {
                VM.PutStormCard2();
            }
        }
        private void ClickCard3(object sender, MouseButtonEventArgs e)
        {
            if (VM.SC3 != null)
            {
                VM.PutStormCard3();
            }
        }
        private void ClickCard4(object sender, MouseButtonEventArgs e)
        {
            if (VM.SC4 != null)
            {
                VM.PutStormCard4();
            }
        }
        private void ClickCard5(object sender, MouseButtonEventArgs e)
        {
            if (VM.SC5 != null)
            {
                VM.PutStormCard5();
            }
        }
        private void ClickCard6(object sender, MouseButtonEventArgs e)
        {
            if (VM.SC6 != null)
            {
                VM.PutStormCard6();
            }
        }
    }
}
