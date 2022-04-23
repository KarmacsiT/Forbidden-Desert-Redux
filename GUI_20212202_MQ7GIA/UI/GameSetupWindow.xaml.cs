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

namespace GUI_20212202_MQ7GIA.UI
{
    /// <summary>
    /// Interaction logic for GameSetupWindow.xaml
    /// </summary>
    public partial class GameSetupWindow : Window
    {
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }
        public string PlayerThreeName { get; set; }
        public GameSetupWindow()
        {
            InitializeComponent();
        }

        private void TwoPlayerBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TwoPlayerStartButtonClick(object sender, RoutedEventArgs e) //Needs additional checking whether difficulty was inputed
        {
            if (TwoPlayerModePlayerOneTextBox.Text is not "" || TwoPlayerModePlayerTwoTextBox.Text is not "")
            {
                PlayerOneName = TwoPlayerModePlayerOneTextBox.Text;
                PlayerTwoName = TwoPlayerModePlayerTwoTextBox.Text;
            }
            else
            {
                MessageBox.Show("A Player's name can't be blank.");
                return;
            }
        }

        private void ThreePlayerBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ThreePlayerStartButtonClick(object sender, RoutedEventArgs e) //Needs additional checking whether difficulty was inputed
        {
            if (ThreePlayerModePlayerOneTextBox.Text is not "" || ThreePlayerModePlayerTwoTextBox.Text is not "" || ThreePlayerModePlayerThreeTextBox.Text is not "")
            {
                PlayerOneName = ThreePlayerModePlayerOneTextBox.Text;
                PlayerTwoName = ThreePlayerModePlayerTwoTextBox.Text;
                PlayerThreeName = ThreePlayerModePlayerThreeTextBox.Text;
            }
            else
            {
                MessageBox.Show("A Player's name can't be blank.");
                return;
            }
        }
    }
}
