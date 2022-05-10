using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.UI.Renderer;
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
    /// Interaction logic for TerrascopeWindow.xaml
    /// </summary>
    public partial class TerrascopeWindow : Window
    {
        public TerraScopeRenderer Renderer { get; set; }
        public GameLogic Logic { get; set; }
        public TerrascopeWindow(TerraScopeRenderer renderer, GameLogic logic)
        {
            this.Renderer = renderer;
            this.Logic = logic;
            Renderer.SetupLogic(Logic);
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Renderer.Resize(new Size(boardDisplay.ActualWidth, boardDisplay.ActualHeight));
            Renderer.InvalidateVisual();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
