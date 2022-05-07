using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using GUI_20212202_MQ7GIA.UI;
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
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        GameLogic logic;
        public Sound Sound { get; set; }
        //List<Player> Players = new List<Player>();
        List<string> colors = new List<string>();
        BoardWindowViewModel boardWindowViewModel;
        WaterSharingWindowViewModel waterSharingWindowVM;

        public BoardWindow(GameLogic logic, Sound sound, GameSetupWindow setupWindow)
        {
            InitializeComponent();
            //playerGeneration
            if (setupWindow.PlayerThreeName is null && logic.Players.Count == 0)
            {
                logic.Players.Add(logic.PlayerInit(setupWindow.PlayerOneName, 1, logic.Players));
                logic.Players.Add(logic.PlayerInit(setupWindow.PlayerTwoName, 2, logic.Players));
            }

            else if (logic.Players.Count == 0)
            {
                logic.Players.Add(logic.PlayerInit(setupWindow.PlayerOneName, 1, logic.Players));
                logic.Players.Add(logic.PlayerInit(setupWindow.PlayerTwoName, 2, logic.Players));
                logic.Players.Add(logic.PlayerInit(setupWindow.PlayerThreeName, 3, logic.Players));
            }

            //Create some logic that matches the role to the piece color

            foreach (Player player in logic.Players)
            {
                switch (player.PlayerRoleName)
                {
                    case RoleName.Archeologist:
                        colors.Add("red_piece.png");
                        break;
                    case RoleName.Climber:
                        colors.Add("black_piece.png");
                        break;
                    case RoleName.Explorer:
                        colors.Add("green_piece.png");
                        break;
                    case RoleName.Meteorologist:
                        colors.Add("white_piece.png");
                        break;
                    case RoleName.Navigator:
                        colors.Add("yellow_piece.png");
                        break;
                    case RoleName.WaterCarrier:
                        colors.Add("blue_piece.png");
                        break;
                    default: break;
                }


            }
            display.SetupLogic(logic, colors);
            Sound = sound;

            partsCollected.SetupModel(logic, logic.Players);
            stormMeter.SetupModel(logic);
            waterSharingWindowVM = new WaterSharingWindowViewModel();
            waterSharingWindowVM.SetupLogic(logic, logic.Players, this);
            boardWindowViewModel = new BoardWindowViewModel(logic.Players);
            this.DataContext = boardWindowViewModel;
            logic.CardsMovingOnBoard += CardsChanging;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            display.Resize(new Size(boardDisplay.ActualWidth, boardDisplay.ActualHeight));
            display.InvalidateVisual();
            partsCollected.Resize(new Size(partsCollectedDisplay.ActualWidth, partsCollectedDisplay.ActualHeight));
            partsCollected.InvalidateVisual();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            PauseWindow pauseWindow = new PauseWindow(Sound);
            pauseWindow.ShowDialog();
            if (pauseWindow.DialogResult == true)
            {
                this.Close();
            }
        }
        private void StormMove(object sender, RoutedEventArgs e)  //only for testing
        {
            if (display.MoveTheStorm(0, 1))
            {
                display.InvalidateVisual();
                stormMeter.InvalidateVisual();
            }
            else
            {
                //implement some proper game over screen
                stormMeter.InvalidateVisual();
                LosingWindow window = new LosingWindow(Sound);
                window.ShowDialog();
                if (window.DialogResult == true)
                {
                    this.Close();
                }
            }

        }

        private void KeyBoardUsed(object sender, KeyEventArgs e)
        {
            bool invalidate = false;
            bool partInvalidate = false;
            bool gameWon = false;
            if (e.Key == Key.NumPad7)    // left and up
            {
                invalidate = display.MoveThePlayer(-1, -1);
                gameWon = display.GameWon();
            }
            else if (e.Key == Key.NumPad9)    // right and up
            {
                invalidate = display.MoveThePlayer(1, -1);
                gameWon = display.GameWon();
            }
            else if (e.Key == Key.NumPad1)    // left and down
            {
                invalidate = display.MoveThePlayer(-1, 1);
                gameWon = display.GameWon();
            }
            else if (e.Key == Key.NumPad3)    // right and down
            {
                invalidate = display.MoveThePlayer(1, 1);
                gameWon = display.GameWon();
            }
            else if (e.Key == Key.Up || e.Key == Key.NumPad8)      // up
            {
                invalidate = display.MoveThePlayer(0, -1);
                gameWon = display.GameWon();
            }
            else if (e.Key == Key.Left || e.Key == Key.NumPad4)     // left
            {
                invalidate = display.MoveThePlayer(-1, 0);
                gameWon = display.GameWon();
            }
            else if (e.Key == Key.Down || e.Key == Key.NumPad2)      // down
            {
                invalidate = display.MoveThePlayer(0, 1);
                gameWon = display.GameWon();
            }
            else if (e.Key == Key.Right || e.Key == Key.NumPad6)    // right
            {
                invalidate = display.MoveThePlayer(1, 0);
                gameWon = display.GameWon();
            }
            else if (e.Key == Key.R) // R
            {
                invalidate = display.RemoveSand();
            }
            else if (e.Key == Key.E)
            {
                invalidate = display.Excavate();
            }
            else if (e.Key == Key.P)
            {
                partInvalidate = partsCollected.ItemPickUp();
            }
            else if (e.Key == Key.S)
            {
                //implement waterSharing
                logic = display.GetLogic();
                waterSharingWindowVM.RefreshPlayers(logic.Players); //Because in the VM the first element is deleted
                waterSharingWindowVM.ShowWindow();
            }
            else if (e.Key == Key.W)
            {
                //implement DisplayWaterLevel
                logic = display.GetLogic();
                WaterLevelWindow waterLevelWindow = new WaterLevelWindow(logic.Players.Where(x => x.TurnOrder == 1).SingleOrDefault());
                waterLevelWindow.Show();
            }
            else if (e.Key == Key.C)
            {
                // Refill
                invalidate = display.WaterCarrierRefill();
            }

            if (invalidate == true)
            {
                UpdateBoardViewModel();
                UpdateItemCardDisplay();
                display.InvalidateVisual();
            }
            if (partInvalidate)
            {
                UpdateBoardViewModel();
                UpdateItemCardDisplay();
                partsCollected.InvalidateVisual();
            }
            if (gameWon)
            {
                WinningWindow window = new WinningWindow(Sound);
                window.ShowDialog();
                if (window.DialogResult == true)
                {
                    this.Close();
                }
            }
        }

        private void ExcavateClick(object sender, KeyEventArgs e)
        {

        }
        public void CatchException(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        private void EndTurn(object sender, RoutedEventArgs e)
        {
            display.EndTurn();
            Sound.PlaySound("411749__natty23__bell-ding.wav");
            UpdateBoardViewModel();
            MessageBox.Show($"{boardWindowViewModel.FirstPlayerName} you're up!");
            // draw cards, (and move storm, ...) 
        }
        private void UpdateBoardViewModel()
        {
            GameLogic logic = display.GetLogic();
            boardWindowViewModel.SetPlayers(logic.Players);

        }


        private void UpdateItemCardDisplay()
        {
            GameLogic logic = display.GetLogic();


            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 1 && logic.CurrentPlayerCard1Display is not null)
            {
                Card1.Source = new BitmapImage(new Uri(logic.CurrentPlayerCard1Display, UriKind.RelativeOrAbsolute));
            }
            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 2 && logic.CurrentPlayerCard2Display is not null)
            {
                Card2.Source = new BitmapImage(new Uri(logic.CurrentPlayerCard2Display, UriKind.RelativeOrAbsolute));
            }
            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 3 && logic.CurrentPlayerCard3Display is not null)
            {
                Card3.Source = new BitmapImage(new Uri(logic.CurrentPlayerCard3Display, UriKind.RelativeOrAbsolute));
            }
            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 4 && logic.CurrentPLayerCard4Display is not null)
            {
                Card4.Source = new BitmapImage(new Uri(logic.CurrentPLayerCard4Display, UriKind.RelativeOrAbsolute));
            }
            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 5 && logic.CurrentPlayerCard5Display is not null)
            {
                Card5.Source = new BitmapImage(new Uri(logic.CurrentPlayerCard5Display, UriKind.RelativeOrAbsolute));
            }
        }
        private void CardsChanging(object sender, EventArgs e)
        {
            logic = display.GetLogic();

            if (P2Card1.Source is not null)
            {
                logic.CurrentPlayerCard1Display = P2Card1.Source.ToString();
            }
            if (P2Card2.Source is not null)
            {
                logic.CurrentPlayerCard2Display = P2Card2.Source.ToString();
            }
            if (P2Card3.Source is not null)
            {
                logic.CurrentPlayerCard3Display = P2Card3.Source.ToString();
            }
            if (P2Card4.Source is not null)
            {
                logic.CurrentPLayerCard4Display = P2Card4.Source.ToString();
            }
            if (P2Card5.Source is not null)
            {
                logic.CurrentPlayerCard5Display = P2Card5.Source.ToString();
            }


            ImageSource temp1 = P2Card1.Source;
            ImageSource temp2 = P2Card2.Source;
            ImageSource temp3 = P2Card3.Source;
            ImageSource temp4 = P2Card4.Source;
            ImageSource temp5 = P2Card5.Source;


            P2Card1.Source = Card1.Source;
            P2Card2.Source = Card2.Source;
            P2Card3.Source = Card3.Source;
            P2Card4.Source = Card4.Source;
            P2Card5.Source = Card5.Source;
            Card1.Source = temp1;
            Card2.Source = temp2;
            Card3.Source = temp3;
            Card4.Source = temp4;
            Card5.Source = temp5;
        }
    }
}
