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
        private Sound Sound { get; set; }
        private GameLogic Logic { get; set; }

        public GameSetupWindow(GameLogic logic, Sound sound)
        {
            InitializeComponent();
            Sound = sound;
            Logic = logic;
        }

        private void TwoPlayerBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TwoPlayerStartButtonClick(object sender, RoutedEventArgs e) //Needs additional checking whether difficulty was inputed
        {
            if (TwoPlayerModePlayerOneTextBox.Text is not "" && TwoPlayerModePlayerTwoTextBox.Text is not "" && TwoPlayerDiffLevel.Text is not "")
            {
                PlayerOneName = TwoPlayerModePlayerOneTextBox.Text;
                PlayerTwoName = TwoPlayerModePlayerTwoTextBox.Text;
                Logic.DifficultyLevel = TwoPlayerDiffLevel.Text;
                Logic.NumberOfPlayers = 2;
                Logic.StormProgress += 1.0/15.0;
                if (Logic.DifficultyLevel == "Normal")
                {
                    Logic.StormProgress += 1.0 / 15.0;
                }
                else if (Logic.DifficultyLevel == "Elite")
                {
                    Logic.StormProgress += 2.0 / 15.0;
                }
                else if (Logic.DifficultyLevel == "Legendary")
                {
                    Logic.StormProgress += 3.0 / 15.0;
                }
                this.Close();
                BoardWindow board = new BoardWindow(Logic, Sound, this);
                board.Show();
            }
            else if (TwoPlayerModePlayerOneTextBox.Text is "" || TwoPlayerModePlayerTwoTextBox.Text is "")
            {
                MessageBox.Show("A Player's name can't be blank.");
                return;
            }
            else
            {
                MessageBox.Show("Please choose a difficulty level in order to start a game!");
            }
        }

        private void ThreePlayerBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ThreePlayerStartButtonClick(object sender, RoutedEventArgs e) //Needs additional checking whether difficulty was inputed
        {
            if (ThreePlayerModePlayerOneTextBox.Text is not "" && ThreePlayerModePlayerTwoTextBox.Text is not "" && ThreePlayerModePlayerThreeTextBox.Text is not "" && ThreePlayerDiffLevel.Text is not "")
            {
                PlayerOneName = ThreePlayerModePlayerOneTextBox.Text;
                PlayerTwoName = ThreePlayerModePlayerTwoTextBox.Text;
                PlayerThreeName = ThreePlayerModePlayerThreeTextBox.Text;
                Logic.DifficultyLevel = ThreePlayerDiffLevel.Text;
                Logic.NumberOfPlayers = 3;
                if (Logic.DifficultyLevel == "Normal")
                {
                    Logic.StormProgress += 1.0 / 15.0;
                }
                else if (Logic.DifficultyLevel == "Elite")
                {
                    Logic.StormProgress += 2.0 / 15.0;
                }
                else if (Logic.DifficultyLevel == "Legendary")
                {
                    Logic.StormProgress += 3.0 / 15.0;
                }
                this.Close();
                BoardWindow board = new BoardWindow(Logic, Sound, this);
                board.Show();

            }
            else if (ThreePlayerModePlayerOneTextBox.Text is "" || ThreePlayerModePlayerTwoTextBox.Text is "" || ThreePlayerModePlayerThreeTextBox.Text is "")
            {
                MessageBox.Show("A Player's name can't be blank.");
                return;
            }
            else
            {
                MessageBox.Show("Please choose a difficulty level in order to start a game!");
                return;
            }
        }
    }
}
