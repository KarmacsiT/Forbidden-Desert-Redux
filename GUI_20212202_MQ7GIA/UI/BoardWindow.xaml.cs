using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using GUI_20212202_MQ7GIA.UI;
using GUI_20212202_MQ7GIA.UI.Renderer;
using GUI_20212202_MQ7GIA.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI_20212202_MQ7GIA
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        GameLogic logic;
        public Sound Sound { get; set; }
        List<string> colors = new List<string>();
        #region ViewModels
        BoardWindowViewModel boardWindowViewModel;
        WaterSharingWindowViewModel waterSharingWindowVM;
        TunnelTeleportWindowViewModel tunnelTeleportWindowVM;
        //Gadget card viewModels
        DuneBlasterWindowViewModel duneBlasterWindowViewModel;
        JetPackWindowViewModel jetPackWindowViewModel;
        TerrascopeSelectWindowViewModel terrascopeSelectWVM;
        StormTrackerWindowViewModel stormTrackerWVM;
        public TerraScopeRenderer terraScopeRenderer;
        #endregion
        CardInspector cardInspector = new CardInspector();
        StormCardDisplay stormCardDisplay = new StormCardDisplay();
        DispatcherTimer timer = new DispatcherTimer();

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
            terraScopeRenderer = new TerraScopeRenderer();
            terraScopeRenderer.SetupLogic(logic);
            Sound = sound;
            Sound.PlayMusic("RPG DD Ambience Windy Desert Immersive Realistic Relaxing Heat Sand Calm.mp3");

            partsCollected.SetupModel(logic, logic.Players);
            stormMeter.SetupModel(logic);
            waterSharingWindowVM = new WaterSharingWindowViewModel();
            waterSharingWindowVM.SetupLogic(logic, this);
            tunnelTeleportWindowVM = new TunnelTeleportWindowViewModel();
            tunnelTeleportWindowVM.SetupLogic(logic, display, this);
            duneBlasterWindowViewModel = new DuneBlasterWindowViewModel();
            duneBlasterWindowViewModel.SetupLogic(logic, this);
            jetPackWindowViewModel = new JetPackWindowViewModel();
            jetPackWindowViewModel.SetupLogic(logic, display, this);
            terrascopeSelectWVM = new TerrascopeSelectWindowViewModel();
            terrascopeSelectWVM.SetupLogic(logic, terraScopeRenderer, this);
            stormTrackerWVM = new StormTrackerWindowViewModel();
            stormTrackerWVM.SetupLogic(logic, this);
            boardWindowViewModel = new BoardWindowViewModel(logic.Players);
            this.DataContext = boardWindowViewModel;
            logic.CardsMovingOnBoard += CardsChanging;
        }
        public BoardWindow(GameLogic logic, Sound sound)       //continue game
        {
            InitializeComponent();
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
            Sound.PlayMusic("RPG DD Ambience Windy Desert Immersive Realistic Relaxing Heat Sand Calm.mp3");

            partsCollected.SetupModel(logic, logic.Players);
            stormMeter.SetupModel(logic);
            waterSharingWindowVM = new WaterSharingWindowViewModel();
            waterSharingWindowVM.SetupLogic(logic, this);
            tunnelTeleportWindowVM = new TunnelTeleportWindowViewModel();
            tunnelTeleportWindowVM.SetupLogic(logic, display, this);
            duneBlasterWindowViewModel = new DuneBlasterWindowViewModel();
            duneBlasterWindowViewModel.SetupLogic(logic, this);
            jetPackWindowViewModel = new JetPackWindowViewModel();
            jetPackWindowViewModel.SetupLogic(logic, display, this);
            terrascopeSelectWVM = new TerrascopeSelectWindowViewModel();
            terrascopeSelectWVM.SetupLogic(logic, terraScopeRenderer, this);
            stormTrackerWVM = new StormTrackerWindowViewModel();
            stormTrackerWVM.SetupLogic(logic, this);
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
            if (pauseWindow.Save == true)
            {
                display.SaveGame();
            }
            if (pauseWindow.DialogResult == true)
            {
                this.Close();
                cardInspector.Close();
                stormCardDisplay.Close();
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
            else if (e.Key == Key.T)
            {
                // Teleport
                logic = display.GetLogic();
                tunnelTeleportWindowVM.RefreshTunnels(new List<TunnelTile>(logic.board.TunnelTiles));
                invalidate = tunnelTeleportWindowVM.ShowWindow();
            }
            else if (e.Key == Key.D0)
            {
                // Remove by Sand
                invalidate = display.RemoveSandByCoordinates(-1, -1);
            }
            else if (e.Key == Key.D1)
            {
                // Remove by Sand
                invalidate = display.RemoveSandByCoordinates(-1, 0);
            }
            else if (e.Key == Key.D2)
            {
                // Remove by Sand
                invalidate = display.RemoveSandByCoordinates(-1, 1);

            }
            else if (e.Key == Key.D3)
            {
                // Remove by Sand
                invalidate = display.RemoveSandByCoordinates(0, -1);
            }
            else if (e.Key == Key.D4)
            {
                // Remove by Sand
                invalidate = display.RemoveSandByCoordinates(0, 1);
            }
            else if (e.Key == Key.D5)
            {
                // Remove by Sand
                invalidate = display.RemoveSandByCoordinates(1, -1);
            }
            else if (e.Key == Key.D6)
            {
                // Remove by Sand
                invalidate = display.RemoveSandByCoordinates(1, 0);
            }
            else if (e.Key == Key.D7)
            {
                // Remove by Sand
                invalidate = display.RemoveSandByCoordinates(1, 1);
            }           
            //testing purpose
            else if (e.Key == Key.Scroll)
            {
                logic = display.GetLogic();
                List<ITile> undiscoveredTiles = logic.UndiscoveredTiles();
                terrascopeSelectWVM.ConvertListToObservable(undiscoveredTiles);
                terrascopeSelectWVM.ShowWindow();
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

        public void CatchException(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        private void EndTurn(object sender, RoutedEventArgs e) //MoveTheStorm now returns storm cards you can use that to display the StormCard on the UI
        {
            List<Image> stormCardDisplayElements = new Image[] { stormCardDisplay.StormCard1Display, stormCardDisplay.StormCard2Display, stormCardDisplay.StormCard3Display, stormCardDisplay.StormCard4Display, stormCardDisplay.StormCard5Display }.ToList();
            List<string> possibleStormMovingCardNames = new string[] { "oneDown", "oneUp", "oneLeft", "oneRight", "twoDown", "twoUp", "twoLeft", "twoRight", "threeDown", "threeUp", "threeLeft", "threeRight" }.ToList();
            string MoveStormMessage = string.Empty;
            string stormCardImagePath = string.Empty;


            foreach (var initialElement in stormCardDisplayElements)
            {
                initialElement.Source = null;
            }

            int iterations = display.NumberOfStormCardsActivated();
            for (int i = 0; i < iterations; i++)
            {
                display.NeedsShufflingStormcards();  //if yes, it automatically shuffles the stormcards

                MoveStormMessage = display.MoveTheStorm().Name;

                if (possibleStormMovingCardNames.Any(sc => sc == MoveStormMessage))
                {
                    stormCardImagePath = $"/ImageAssets/Storm Cards/{MoveStormMessage}.png";

                    foreach (var element in stormCardDisplayElements)
                    {
                        if (element.Source is null)
                        {
                            element.Source = new BitmapImage(new Uri(stormCardImagePath, UriKind.RelativeOrAbsolute));
                            break;
                        }
                    }

                    display.InvalidateVisual();
                    StormDiscardDisplay.Source = new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm Card Backside.png", UriKind.RelativeOrAbsolute));
                }
                else if (MoveStormMessage == "Storm Picks Up")
                {

                    foreach (var element in stormCardDisplayElements)
                    {
                        if (element.Source is null)
                        {
                            element.Source = new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm Picks Up.png", UriKind.RelativeOrAbsolute));
                            break;
                        }
                    }

                    stormMeter.InvalidateVisual();

                }

                else if (MoveStormMessage == "Sun Beats Down")
                {
                    foreach (var element in stormCardDisplayElements)
                    {
                        if (element.Source is null)
                        {
                            element.Source = new BitmapImage(new Uri("/ImageAssets/Storm Cards/Sun Beats Down.png", UriKind.RelativeOrAbsolute));
                            break;
                        }
                    }

                }
                display.MoveStormCardToDiscarded();
                if (display.LoseOrNot())
                {
                    LoseGame();
                    break;
                }
            }
            stormCardDisplay.Show();

            if (display.LoseOrNot() == false)
            {
                display.EndTurn();
                Sound.PlaySound("326478__byseb__automatic-wrist-watch-ticking.wav");
                UpdateBoardViewModel();
                if (stormCardDisplay.IsVisible is true)
                {
                    timer.Tick += new EventHandler(NextPlayer);
                    if (iterations <= 3)
                    {
                        timer.Interval = new TimeSpan(0, 0, 0, 4);
                    }
                    if (iterations >= 4)
                    {
                        timer.Interval = new TimeSpan(0, 0, 0, 5);
                    }
                    timer.Start();
                }
                else
                {
                    stormCardDisplay.Hide(); //Just in case to cover a rare corner case
                    MessageBox.Show($"{boardWindowViewModel.FirstPlayerName} you're up!");
                }
            }
        }


        private void NextPlayer(object sender, EventArgs e)
        {
            stormCardDisplay.Hide(); //Just in case to cover a rare corner case
            MessageBox.Show($"{boardWindowViewModel.FirstPlayerName} you're up!");
            timer.Stop();
            timer.Tick -= NextPlayer;
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
                Card1.Source = logic.CurrentPlayerCard1Display;
            }
            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 2 && logic.CurrentPlayerCard2Display is not null)
            {
                Card2.Source = logic.CurrentPlayerCard2Display;
            }
            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 3 && logic.CurrentPlayerCard3Display is not null)
            {
                Card3.Source = logic.CurrentPlayerCard3Display;
            }
            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 4 && logic.CurrentPlayerCard4Display is not null)
            {
                Card4.Source = logic.CurrentPlayerCard4Display;
            }
            if (logic.Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Count() is 5 && logic.CurrentPlayerCard5Display is not null)
            {
                Card5.Source = logic.CurrentPlayerCard5Display;
            }
        }
        private void CardsChanging(object sender, EventArgs e)
        {
            logic = display.GetLogic();

            if (P2Card1.Source is not null)
            {
                logic.CurrentPlayerCard1Display = P2Card1.Source;
            }
            else
            {
                logic.CurrentPlayerCard1Display = null;
            }
            if (P2Card2.Source is not null)
            {
                logic.CurrentPlayerCard2Display = P2Card2.Source;
            }
            else
            {
                logic.CurrentPlayerCard2Display = null;
            }
            if (P2Card3.Source is not null)
            {
                logic.CurrentPlayerCard3Display = P2Card3.Source;
            }
            else
            {
                logic.CurrentPlayerCard3Display = null;
            }
            if (P2Card4.Source is not null)
            {
                logic.CurrentPlayerCard4Display = P2Card4.Source;
            }
            else
            {
                logic.CurrentPlayerCard4Display = null;
            }
            if (P2Card5.Source is not null)
            {
                logic.CurrentPlayerCard5Display = P2Card5.Source;
            }
            else
            {
                logic.CurrentPlayerCard5Display = null;
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
        private void LoseGame()
        {
            LosingWindow window = new LosingWindow(Sound);
            stormCardDisplay.Hide();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                this.Close();
            }
        }

        #region MouseMove Functions
        private void CurrentPlayer_Card1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(Card1, new DataObject(typeof(Image), Card1), DragDropEffects.Copy);

            }
        }

        private void CurrentPlayer_Card2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(Card2, new DataObject(typeof(Image), Card2), DragDropEffects.Copy);

            }
        }

        private void CurrentPlayer_Card3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(Card3, new DataObject(typeof(Image), Card3), DragDropEffects.Copy);

            }
        }

        private void CurrentPlayer_Card4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(Card4, new DataObject(typeof(Image), Card4), DragDropEffects.Copy);

            }
        }

        private void CurrentPlayer_Card5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(Card5, new DataObject(typeof(Image), Card5), DragDropEffects.Copy);

            }
        }

        private void Player2_Card1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(P2Card1, new DataObject(typeof(Image), P2Card1), DragDropEffects.Copy);

            }
        }

        private void Player2_Card2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(P2Card2, new DataObject(typeof(Image), P2Card2), DragDropEffects.Copy);

            }
        }

        private void Player2_Card3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(P2Card3, new DataObject(typeof(Image), P2Card3), DragDropEffects.Copy);

            }
        }

        private void Player2_Card4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(P2Card4, new DataObject(typeof(Image), P2Card4), DragDropEffects.Copy);

            }
        }

        private void Player2_Card5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(P2Card5, new DataObject(typeof(Image), P2Card5), DragDropEffects.Copy);

            }
        }
        #endregion
        private void Board_Drop(object sender, DragEventArgs e)
        {
            object dragedObject = e.Data.GetData(typeof(Image)) as Image;

            if (dragedObject is not null)
            {
                logic = display.GetLogic();
                string dragedCardName = (dragedObject as Image).Name;

                string draggedCardGadgetType = (dragedObject as Image).Source.ToString().Split('/')[5].Split('.')[0]; //We can use this to trigger gadget effects
                bool invalidate = false;
                ItemDiscardDisplay.Source = new BitmapImage(new Uri("/ImageAssets/Gadget Cards/Gadget Backside.png", UriKind.RelativeOrAbsolute));
                switch (dragedCardName) //null out the dropped cards source and the logic card property
                {
                    case "Card1":
                        Card1.Source = null;
                        logic.CurrentPlayerCard1Display = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "Card2":
                        Card2.Source = null;
                        logic.CurrentPlayerCard2Display = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "Card3":
                        Card3.Source = null;
                        logic.CurrentPlayerCard3Display = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "Card4":
                        Card4.Source = null;
                        logic.CurrentPlayerCard4Display = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "Card5":
                        Card5.Source = null;
                        logic.CurrentPlayerCard5Display = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 1).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "P2Card1":
                        P2Card1.Source = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "P2Card2":
                        P2Card2.Source = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "P2Card3":
                        P2Card3.Source = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "P2Card4":
                        P2Card4.Source = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                    case "P2Card5":
                        P2Card5.Source = null;
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().IsDiscarded = true; //Mathcing card type that is in a players hand is now discarded upon drop
                        logic.Deck.AvailableItemCards.Where(c => c.Name == draggedCardGadgetType && c.InPlayerHand is true).FirstOrDefault().InPlayerHand = false; //After discarding the card, it is no longer in a players hand
                        logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Remove(logic.Players.Where(p => p.TurnOrder is 2).FirstOrDefault().Cards.Where(c => c.Name == draggedCardGadgetType).FirstOrDefault()); //Removes the specified card from the players "hand"
                        break;
                }
                //decide which player's card was pulled
                int turnOrder = 1;
                if (dragedCardName.StartsWith("P2"))
                {
                    turnOrder = 2;
                }
                else if (dragedCardName.StartsWith("P3"))
                {
                    turnOrder = 3;
                }
                switch (draggedCardGadgetType)
                {
                    case "Dune Blaster":
                        logic.RefreshAdjacentSandTilesForPlayer(turnOrder);
                        duneBlasterWindowViewModel.ConvertListToObservable(logic.adjacentSandedTilesFromPlayer);
                        invalidate = duneBlasterWindowViewModel.ShowWindow();
                        break;
                    case "Jet Pack":
                        List<Player> onSameTile = logic.GetPlayersOnSameTile(turnOrder);
                        List<Tile> unblockedTiles = logic.GetUnblockedTiles();
                        jetPackWindowViewModel.ConvertListToObservable(unblockedTiles,onSameTile);
                        jetPackWindowViewModel.TurnOrder = turnOrder;
                        invalidate = jetPackWindowViewModel.ShowWindow();
                        break;
                    case "Terrascope":
                        List<ITile> undiscoveredTiles = logic.UndiscoveredTiles();
                        terrascopeSelectWVM.ConvertListToObservable(undiscoveredTiles);
                        terrascopeSelectWVM.ShowWindow();
                        break;
                    case "Storm Tracker":
                        logic = display.GetLogic();
                        List<StormCard> stormcards = logic.CollectStormCardsForTracking();
                        stormTrackerWVM.ConvertListToObservable(stormcards);
                        stormTrackerWVM.ShowWindow();
                        break;
                    case "Time Throttle":
                        logic.Players.Where(p => p.TurnOrder == turnOrder).SingleOrDefault().NumberOfActions += 2;
                        UpdateBoardViewModel();
                        break;
                    case "Secret Water Reserve":
                        logic.SecretWaterReserve(turnOrder);
                        break;
                    default:
                        break;
                }
                if (invalidate)
                {
                    UpdateBoardViewModel();
                    UpdateItemCardDisplay();
                    display.InvalidateVisual();
                }
               //

            }
        }

        #region MouseEnter and MouseLeave Funtions
        private void CurrentPlayer_Card1_MouseEnter(object sender, MouseEventArgs e) //Somehow close the window once the player goes back to the main menu
        {
            if (Card1.Source is not null)
            {
                cardInspector.InspectedCard.Source = Card1.Source;
                cardInspector.Show();
            }
        }

        private void CurrentPlayer_Card1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Card1.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void CurrentPlayer_Card2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Card2.Source is not null)
            {
                cardInspector.InspectedCard.Source = Card2.Source;
                cardInspector.Show();
            }
        }

        private void CurrentPlayer_Card2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Card2.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void CurrentPlayer_Card3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Card3.Source is not null)
            {
                cardInspector.InspectedCard.Source = Card3.Source;
                cardInspector.Show();
            }
        }

        private void CurrentPlayer_Card3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Card3.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void CurrentPlayer_Card4_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Card4.Source is not null)
            {
                cardInspector.InspectedCard.Source = Card4.Source;
                cardInspector.Show();
            }
        }

        private void CurrentPlayer_Card4_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Card4.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void CurrentPlayer_Card5_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Card5.Source is not null)
            {
                cardInspector.InspectedCard.Source = Card5.Source;
                cardInspector.Show();
            }
        }

        private void CurrentPlayer_Card5_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Card5.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void Player2_Card1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (P2Card1.Source is not null)
            {
                cardInspector.InspectedCard.Source = P2Card1.Source;
                cardInspector.Show();
            }
        }

        private void Player2_Card1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (P2Card1.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void Player2_Card2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (P2Card2.Source is not null)
            {
                cardInspector.InspectedCard.Source = P2Card2.Source;
                cardInspector.Show();
            }
        }

        private void Player2_Card2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (P2Card2.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void Player2_Card3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (P2Card3.Source is not null)
            {
                cardInspector.InspectedCard.Source = P2Card3.Source;
                cardInspector.Show();
            }
        }

        private void Player2_Card3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (P2Card3.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void Player2_Card4_MouseEnter(object sender, MouseEventArgs e)
        {
            if (P2Card4.Source is not null)
            {
                cardInspector.InspectedCard.Source = P2Card4.Source;
                cardInspector.Show();
            }
        }

        private void Player2_Card4_MouseLeave(object sender, MouseEventArgs e)
        {
            if (P2Card4.Source is not null)
            {
                cardInspector.Hide();
            }
        }

        private void Player2_Card5_MouseEnter(object sender, MouseEventArgs e)
        {
            if (P2Card5.Source is not null)
            {
                cardInspector.InspectedCard.Source = P2Card5.Source;
                cardInspector.Show();
            }
        }

        private void Player2_Card5_MouseLeave(object sender, MouseEventArgs e)
        {
            if (P2Card5.Source is not null)
            {
                cardInspector.Hide();
            }
        }
        #endregion

    }
}
