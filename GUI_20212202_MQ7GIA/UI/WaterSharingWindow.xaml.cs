using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using GUI_20212202_MQ7GIA.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for WaterSharingWindow.xaml
    /// </summary>
    public partial class WaterSharingWindow : Window
    {
        public WaterSharingWindow(WaterSharingWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
