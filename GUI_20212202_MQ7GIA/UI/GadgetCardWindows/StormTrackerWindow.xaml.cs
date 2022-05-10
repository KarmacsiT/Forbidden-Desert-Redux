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
        public StormTrackerWindow(StormTrackerWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
