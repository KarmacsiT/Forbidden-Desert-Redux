using GUI_20212202_MQ7GIA.Models;
using GUI_20212202_MQ7GIA.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GUI_20212202_MQ7GIA.Logic
{
    public class GameLogic
    {
        public BoardWindowViewModel BoardWindowViewModel { get; set; } = new BoardWindowViewModel();
        public Board board { get; set; }
        public Deck Deck { get { return DeckGeneration(); } set { Deck = value; } } //Setting might be incorrect check back later

        public GameStatus Status { get; set; }
        private List<Player> players = new List<Player>();
        public List<Player> Players { get { return players; } set { players = value; } }
        public int CurrentPlayer { get; set; } = 1;
        public ShipParts[] shipParts { get; set; }
        public Sound Sound { get; set; }
        public string[,] TileNames { get; set; }
        public string[,] PartTiles { get; set; }
        public string DifficultyLevel { get; set; }
        public int NumberOfPlayers { get; set; } // this is a very efficient way to store the # of players, because there are cases when we don't want the entire object (eg. Storm Meter)
        public double StormProgress { get; set; }
        public int StormProgressNumberOfCards { get; set; }  // from 2 to 6, it gives us the number of stormcards to be drawn

        public string CurrentPlayerCard1Display { get; set; }
        public string CurrentPlayerCard2Display { get; set; }
        public string CurrentPlayerCard3Display { get; set; }
        public string CurrentPLayerCard4Display { get; set; }
        public string CurrentPlayerCard5Display { get; set; }


        public EventHandler CardsMovingOnBoard;
        protected virtual void OnCardsMovingOnBoard(EventArgs e)
        {
            CardsMovingOnBoard?.Invoke(this, e);
        }

        Random random = new Random();
        public GameLogic(Sound sound)
        {
            bool[,] isTaken = new bool[5, 5];
            TileNames = new string[5, 5];
            Sound = sound;
            board = new Board
            {
                TunnelTiles = new TunnelTile[3], // Okay, we have 3 tunnel tiles, not 2
                AirShipClueTiles = new AirShipClueTile[8],
                LaunchPadTile = new LaunchPadTile(),
                OasisMirageTiles = new OasisMirageTile[3],
                ShelterTiles = new ShelterTile[9],
                SandTiles = new int[5, 5],
                storm = new Storm()
            };
            board.storm.X = 2;
            board.storm.Y = 2;
            TileNames[board.storm.X, board.storm.Y] = "Storm";
            isTaken[board.storm.X, board.storm.Y] = true;

            shipParts = new ShipParts[4];
            shipParts[0] = new ShipParts { Name = "Crystal" };
            shipParts[1] = new ShipParts { Name = "Engine" };
            shipParts[2] = new ShipParts { Name = "Compass" };
            shipParts[3] = new ShipParts { Name = "Propeller" };
            int cardCounter = 0;

            //We need this to avoid card generation conflicts when X = 0 and Y = 0
            for (int i = 0; i < 8; i++)
            {
                board.AirShipClueTiles[i] = new AirShipClueTile();
                board.AirShipClueTiles[i].X = -1;
                board.AirShipClueTiles[i].Y = -1;
            }
            //AirShipClueTile generation


            foreach (AirShipClueTile tile in board.AirShipClueTiles)
            {
                int X = random.Next(0, 5);
                int Y = random.Next(0, 5);

                //Avoid number generation for 2 and check if card is already generated (and also avoid generation when the x and ys are the same
                while (isTaken[X, Y] || IsItDuplicate(X, Y, board.AirShipClueTiles))
                {
                    X = random.Next(0, 5);
                    Y = random.Next(0, 5);
                }

                tile.X = X;
                tile.Y = Y;
                isTaken[X, Y] = true;
                TileNames[X, Y] = "AirShipClueTile";

                //Put direction
                if (cardCounter % 2 == 0)
                {
                    tile.Direction = 'X';
                }
                else
                {
                    tile.Direction = 'Y';
                }
                if (cardCounter < 2) tile.PartName = shipParts[0].Name;
                else if (cardCounter < 4) tile.PartName = shipParts[1].Name;
                else if (cardCounter < 6) tile.PartName = shipParts[2].Name;
                else tile.PartName = shipParts[3].Name;
                cardCounter++;
            }

            PartTiles = new string[5, 5];

            // function, since every time the storm moves, it has to be called
            PartTileCoordinateGiver();

            // LaunchPadTile 
            int[] coordinates = CoordinateGiver(isTaken);
            board.LaunchPadTile = new LaunchPadTile();
            board.LaunchPadTile.X = coordinates[0];
            board.LaunchPadTile.Y = coordinates[1];
            isTaken[board.LaunchPadTile.X, board.LaunchPadTile.Y] = true;
            TileNames[board.LaunchPadTile.X, board.LaunchPadTile.Y] = "LaunchPadTile";

            // CrashStartTile 
            coordinates = CoordinateGiver(isTaken);
            board.CrashStartTile = new CrashStartTile();
            board.CrashStartTile.X = coordinates[0];
            board.CrashStartTile.Y = coordinates[1];
            board.CrashStartTile.IsDiscovered = true; //It's already discovered
            isTaken[board.CrashStartTile.X, board.CrashStartTile.Y] = true;
            TileNames[board.CrashStartTile.X, board.CrashStartTile.Y] = "CrashStartTile";

            // TunnelTiles && OasisMirageTiles

            for (int i = 0; i < 3; i++)
            {
                board.TunnelTiles[i] = new TunnelTile();

                coordinates = CoordinateGiver(isTaken);
                board.TunnelTiles[i].X = coordinates[0];
                board.TunnelTiles[i].Y = coordinates[1];
                isTaken[board.TunnelTiles[i].X, board.TunnelTiles[i].Y] = true;
                TileNames[board.TunnelTiles[i].X, board.TunnelTiles[i].Y] = "TunnelTile";

                coordinates = CoordinateGiver(isTaken);
                board.OasisMirageTiles[i] = new OasisMirageTile();
                board.OasisMirageTiles[i].X = coordinates[0];
                board.OasisMirageTiles[i].Y = coordinates[1];
                isTaken[board.OasisMirageTiles[i].X, board.OasisMirageTiles[i].Y] = true;

                if (i is 0)
                {
                    board.OasisMirageTiles[i].IsDried = true; //Generate Mirage
                    TileNames[board.OasisMirageTiles[i].X, board.OasisMirageTiles[i].Y] = "Mirage";
                }

                else
                {
                    board.OasisMirageTiles[i].IsDried = false; //Generate Oasis
                    TileNames[board.OasisMirageTiles[i].X, board.OasisMirageTiles[i].Y] = "Oasis";
                }
            }

            // ShelterTiles
            for (int i = 0; i < board.ShelterTiles.Length; i++)
            {
                board.ShelterTiles[i] = new ShelterTile();
            }

            foreach (ShelterTile tile in board.ShelterTiles)
            {
                int x = 0;
                bool breakLoops = false;

                while (x < isTaken.GetLength(0) && !breakLoops)
                {
                    int y = 0;

                    while (y < isTaken.GetLength(1) && !breakLoops)
                    {
                        if (isTaken[x, y] is false)
                        {
                            tile.X = x;
                            tile.Y = y;
                            isTaken[tile.X, tile.Y] = true;

                            int chance = random.Next(0, 101);

                            if (chance <= 50)
                            {
                                tile.ShelterType = ShelterVariations.Empty;
                                TileNames[tile.X, tile.Y] = "EmptyShelter";
                            }
                            else if (chance > 50 && chance <= 65)
                            {
                                tile.ShelterType = ShelterVariations.FriendlyWater;
                                TileNames[tile.X, tile.Y] = "FriendlyWater";
                            }
                            else if (chance > 65 && chance <= 80)
                            {
                                tile.ShelterType = ShelterVariations.FriendlyQuest;
                                TileNames[tile.X, tile.Y] = "FriendlyQuest";
                            }
                            else
                            {
                                tile.ShelterType = ShelterVariations.Hostile;
                                TileNames[tile.X, tile.Y] = "Hostile";
                            }

                            breakLoops = true;
                        }
                        y++;
                    }
                    x++;
                }
            }
            //SandTiles (These are fixed at the beginning)
            board.SandTiles[2, 0] += 1;
            board.SandTiles[1, 1] += 1;
            board.SandTiles[3, 1] += 1;
            board.SandTiles[0, 2] += 1;
            board.SandTiles[4, 2] += 1;
            board.SandTiles[1, 3] += 1;
            board.SandTiles[3, 3] += 1;
            board.SandTiles[2, 4] += 1;

        }
        public bool IsItDuplicate(int X, int Y, AirShipClueTile[] tiles)
        {
            for (int i = 0; i < tiles.Length; i += 2)
            {
                if (tiles[i].X == X && tiles[i + 1].Y == Y)
                {
                    return true;
                }
            }
            return false;
        }
        public bool SandTileChecker(int X, int Y)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (x == X && y == Y && board.SandTiles[x, y] > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DoubleSandChecker(int X, int Y)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (x == X && y == Y && board.SandTiles[x, y] > 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool PartPickedChecker(int X, int Y)
        {
            return shipParts.Where(x => x.Name == PartTiles[X, Y]).Select(x => x.IsPickedUp).SingleOrDefault();
        }
        private int[] CoordinateGiver(bool[,] isTaken)
        {
            int[] result = new int[2];   // 0--X , 1 ---Y
            int x = random.Next(0, 5);
            int y = random.Next(0, 5);

            while (isTaken[x, y])  //The coordinate can't be the coordinate of the storm start coordinate which is [2,2] or taken
            {
                x = random.Next(0, 5);
                y = random.Next(0, 5);
            }

            result[0] = x;
            result[1] = y;
            return result;
        }
        private void PartTileCoordinateGiver()
        {
            int cardCounter = 0;
            foreach (ShipParts part in shipParts)
            {
                part.X = board.AirShipClueTiles[cardCounter].X;
                part.Y = board.AirShipClueTiles[cardCounter + 1].Y;
                PartTiles[part.X, part.Y] = board.AirShipClueTiles[cardCounter].PartName;
                cardCounter += 2;
            }
        }
        private void PlayersOnStorm(int PositionX, int PositionY, int x, int y, List<Player> players)
        {
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                if (players[i].X == PositionX && players[i].Y == PositionY)
                {
                    if (x != -100)
                    {
                        players[i].X = x;
                    }
                    else if (y != -100)
                    {
                        players[i].Y = y;
                    }
                }
            }
        }
        private bool MoveStorm(int x, int y, List<Player> players)
        {
            bool changed = false;
            if (x != 0)  //&& StormProgress < 0.93
            {
                for (int i = 0; i < Math.Abs(x); i++)
                {
                    if (x > 0)
                    {
                        if (board.storm.X + 1 == 5)
                        {
                            //nothing happens, this is the case when the storm is on the edge
                        }
                        else
                        {
                            changed = true;
                            board.SandTiles[board.storm.X, board.storm.Y] += 1;
                            string tempname = TileNames[board.storm.X + 1, board.storm.Y];
                            TileNames[board.storm.X + 1, board.storm.Y] = "Storm";
                            TileNames[board.storm.X, board.storm.Y] = tempname;
                            PlayersOnStorm(board.storm.X + 1, board.storm.Y, board.storm.X, -100, players);      // -100 means that the value should not change                          
                            switch (tempname)
                            {
                                case "AirShipClueTile":
                                    {
                                        board.AirShipClueTiles.First(x => x.X == board.storm.X + 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                                case "LaunchPadTile":
                                    {
                                        board.LaunchPadTile.X = board.storm.X;
                                    }
                                    break;
                                case "TunnelTile":
                                    {
                                        board.TunnelTiles.First(x => x.X == board.storm.X + 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                                case "Mirage":
                                    {
                                        board.OasisMirageTiles.First(x => x.X == board.storm.X + 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                                case "Oasis":
                                    {
                                        board.OasisMirageTiles.First(x => x.X == board.storm.X + 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                                case "CrashStartTile":
                                    {
                                        board.CrashStartTile.X = board.storm.X;
                                    }
                                    break;
                                default:      //Shelters left
                                    {
                                        board.ShelterTiles.First(x => x.X == board.storm.X + 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                            }
                            board.storm.X += 1;
                            //StormProgress += 1.0 / 15.0;
                            Sound.PlaySound("360372__chancemedia__20160724-loud-cloud.mp3");
                        }
                    }
                    else
                    {
                        if (board.storm.X - 1 == -1)
                        {
                            //nothing happens, this is the case when the storm is on the edge
                        }
                        else
                        {
                            changed = true;
                            board.SandTiles[board.storm.X, board.storm.Y] += 1;
                            string tempname = TileNames[board.storm.X - 1, board.storm.Y];
                            TileNames[board.storm.X - 1, board.storm.Y] = "Storm";
                            TileNames[board.storm.X, board.storm.Y] = tempname;
                            PlayersOnStorm(board.storm.X - 1, board.storm.Y, board.storm.X, -100, players);      // -100 means that the value should not change                          

                            switch (tempname)
                            {
                                case "AirShipClueTile":
                                    {
                                        board.AirShipClueTiles.First(x => x.X == board.storm.X - 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                                case "LaunchPadTile":
                                    {
                                        board.LaunchPadTile.X = board.storm.X;
                                    }
                                    break;
                                case "TunnelTile":
                                    {
                                        board.TunnelTiles.First(x => x.X == board.storm.X - 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                                case "Mirage":
                                    {
                                        board.OasisMirageTiles.First(x => x.X == board.storm.X - 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                                case "Oasis":
                                    {
                                        board.OasisMirageTiles.First(x => x.X == board.storm.X - 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                                case "CrashStartTile":
                                    {
                                        board.CrashStartTile.X = board.storm.X;
                                    }
                                    break;
                                default:      //Shelters left
                                    {
                                        board.ShelterTiles.First(x => x.X == board.storm.X - 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                            }
                            board.storm.X -= 1;
                            //StormProgress += 1.0 / 15.0;
                            Sound.PlaySound("360372__chancemedia__20160724-loud-cloud.mp3");
                        }
                    }
                    PartTileCoordinateGiver();
                }
            }
            else      //StormProgress < 0.93
            {
                for (int i = 0; i < Math.Abs(y); i++)
                {
                    if (y > 0)
                    {
                        if (board.storm.Y + 1 == 5)
                        {
                            //nothing happens, this is the case when the storm is on the edge
                        }
                        else
                        {
                            changed = true;
                            board.SandTiles[board.storm.X, board.storm.Y] += 1;
                            string tempname = TileNames[board.storm.X, board.storm.Y + 1];
                            TileNames[board.storm.X, board.storm.Y + 1] = "Storm";
                            TileNames[board.storm.X, board.storm.Y] = tempname;
                            PlayersOnStorm(board.storm.X, board.storm.Y + 1, -100, board.storm.Y, players);      // -100 means that the value should not change                          


                            switch (tempname)
                            {
                                case "AirShipClueTile":
                                    {
                                        board.AirShipClueTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y + 1).Y = board.storm.Y;
                                    }
                                    break;
                                case "LaunchPadTile":
                                    {
                                        board.LaunchPadTile.Y = board.storm.Y;
                                    }
                                    break;
                                case "CrashStartTile":
                                    {
                                        board.CrashStartTile.Y = board.storm.Y;
                                    }
                                    break;
                                case "TunnelTile":
                                    {
                                        board.TunnelTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y + 1).Y = board.storm.Y;
                                    }
                                    break;
                                case "Mirage":
                                    {
                                        board.OasisMirageTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y + 1).Y = board.storm.Y;
                                    }
                                    break;
                                case "Oasis":
                                    {
                                        board.OasisMirageTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y + 1).Y = board.storm.Y;
                                    }
                                    break;

                                default:      //Shelters left
                                    {
                                        board.ShelterTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y + 1).Y = board.storm.Y;
                                    }
                                    break;
                            }
                            board.storm.Y += 1;
                            //StormProgress += 1.0 / 15.0;
                            Sound.PlaySound("360372__chancemedia__20160724-loud-cloud.mp3");
                        }
                    }
                    else
                    {
                        if (board.storm.Y - 1 == -1)
                        {
                            //nothing happens, this is the case when the storm is on the edge
                        }
                        else
                        {
                            changed = true;
                            board.SandTiles[board.storm.X, board.storm.Y] += 1;
                            string tempname = TileNames[board.storm.X, board.storm.Y - 1];
                            TileNames[board.storm.X, board.storm.Y - 1] = "Storm";
                            TileNames[board.storm.X, board.storm.Y] = tempname;
                            PlayersOnStorm(board.storm.X, board.storm.Y - 1, -100, board.storm.Y, players);      // -100 means that the value should not change                          

                            switch (tempname)
                            {
                                case "AirShipClueTile":
                                    {
                                        board.AirShipClueTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y - 1).Y = board.storm.Y;
                                    }
                                    break;
                                case "LaunchPadTile":
                                    {
                                        board.LaunchPadTile.Y = board.storm.Y;
                                    }
                                    break;
                                case "CrashStartTile":
                                    {
                                        board.CrashStartTile.Y = board.storm.Y;
                                    }
                                    break;
                                case "TunnelTile":
                                    {
                                        board.TunnelTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y - 1).Y = board.storm.Y;
                                    }
                                    break;
                                case "Mirage":
                                    {
                                        board.OasisMirageTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y - 1).Y = board.storm.Y;
                                    }
                                    break;
                                case "Oasis":
                                    {
                                        board.OasisMirageTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y - 1).Y = board.storm.Y;
                                    }
                                    break;
                                default:      //Shelters left
                                    {
                                        board.ShelterTiles.First(x => x.X == board.storm.X && x.Y == board.storm.Y - 1).Y = board.storm.Y;
                                    }
                                    break;
                            }
                            board.storm.Y -= 1;
                            Sound.PlaySound("360372__chancemedia__20160724-loud-cloud.mp3");
                        }
                    }
                    PartTileCoordinateGiver();
                }
            }
            return changed;
        }

        public bool StormCardAction(StormCard currentCard)          //only 1 card is played in the Logic, this is the function, that is called muliple times if needed   
        {
            return MoveStorm(currentCard.XMove, currentCard.YMove, Players);
        }
        public void StormMeterUp()
        {
            StormProgress += 1.0 / 15.0;
        }
        public int CalculateNumberOfStormCards()
        {
            if (NumberOfPlayers == 2)
            {
                if (StormProgress < (1.0 / 15.0) * 2)
                {
                    StormProgressNumberOfCards = 2;
                }
                else if (StormProgress >= (1.0 / 15.0) * 2 && StormProgress < (1.0 / 15.0) * 5)
                {
                    StormProgressNumberOfCards = 3;
                }
                else if (StormProgress >= (1.0 / 15.0) * 5 && StormProgress < (1.0 / 15.0) * 9)
                {
                    StormProgressNumberOfCards = 4;
                }
                else if (StormProgress >= (1.0 / 15.0) * 9 && StormProgress < 0.79)   //double accuracy stops here :)
                {
                    StormProgressNumberOfCards = 5;
                }
                else if (StormProgress >= 0.79 && StormProgress < 0.87)
                {
                    StormProgressNumberOfCards = 6;
                }
                else
                {
                    StormProgressNumberOfCards = 144; //theoretically, this part should never be accessed
                }
            }
            else if (NumberOfPlayers == 3)
            {
                if (StormProgress < 1.0 / 15.0)
                {
                    StormProgressNumberOfCards = 2;
                }
                else if (StormProgress >= 1.0 / 15.0 && StormProgress < (1.0 / 15.0) * 5)
                {
                    StormProgressNumberOfCards = 3;
                }
                else if (StormProgress >= (1.0 / 15.0) * 5 && StormProgress < (1.0 / 15.0) * 9)
                {
                    StormProgressNumberOfCards = 4;
                }
                else if (StormProgress >= (1.0 / 15.0) * 9 && StormProgress < 0.79)  //double accuracy stops here :)
                {
                    StormProgressNumberOfCards = 5;
                }
                else if (StormProgress >= 0.79 && StormProgress < 0.87)
                {
                    StormProgressNumberOfCards = 6;
                }
                else
                {
                    StormProgressNumberOfCards = 144; //theoretically, this part should never be accessed
                }

            }
            return StormProgressNumberOfCards;
        }
        private Deck DeckGeneration()
        {

            Deck GameDeck = new Deck();

            GameDeck.AvailableItemCards = new List<ItemCard>();
            GameDeck.AvailableStormCards = new List<StormCard>();
            //GameDeck.DiscardedItemCards = new List<ItemCard>();         we might not need this
            //GameDeck.DiscardedStormCards = new List<StormCard>();



            for (int i = 0; i < 3; i++)
            {
                GameDeck.AvailableItemCards.Add(new ItemCard("Dune Blaster", false, false, "/ImageAssets/Gadget Cards/Dune Blaster.png"));
                GameDeck.AvailableItemCards.Add(new ItemCard("Jet Pack", false, false, "/ImageAssets/Gadget Cards/Jet Pack.png"));

                GameDeck.AvailableStormCards.Add(new StormCard("oneDown", false, 0, -1));
                GameDeck.AvailableStormCards.Add(new StormCard("oneUp", false, 0, 1));
                GameDeck.AvailableStormCards.Add(new StormCard("oneLeft", false, -1, 0));
                GameDeck.AvailableStormCards.Add(new StormCard("oneRight", false, 1, 0));
                GameDeck.AvailableStormCards.Add(new StormCard("SunBeatsdown", false, -404, -404));
                GameDeck.AvailableStormCards.Add(new StormCard("StormPicksUp", false, -303, -303));
            }

            GameDeck.AvailableStormCards.Add(new StormCard("SunBeatsdown", false, -404, -404));

            for (int i = 0; i < 2; i++)
            {
                GameDeck.AvailableItemCards.Add(new ItemCard("Solar Shield", false, false, "/ImageAssets/Gadget Cards/Solar Shield.png"));
                GameDeck.AvailableItemCards.Add(new ItemCard("Terrascope", false, false, "/ImageAssets/Gadget Cards/Terrascope.png"));

                GameDeck.AvailableStormCards.Add(new StormCard("twoDown", false, 0, -2));
                GameDeck.AvailableStormCards.Add(new StormCard("twoUp", false, 0, 2));
                GameDeck.AvailableStormCards.Add(new StormCard("twoLeft", false, -2, 0));
                GameDeck.AvailableStormCards.Add(new StormCard("twoRight", false, 2, 0));
            }

            GameDeck.AvailableItemCards.Add(new ItemCard("Secret Water Reserve", false, false, "/ImageAssets/Gadget Cards/Secret Water Reserve.png"));
            GameDeck.AvailableItemCards.Add(new ItemCard("Storm Tracker", false, false, "/ImageAssets/Gadget Cards/Storm Tracker.png"));
            GameDeck.AvailableItemCards.Add(new ItemCard("Time Throttle", false, false, "/ImageAssets/Gadget Cards/Time Throttler.png"));

            GameDeck.AvailableStormCards.Add(new StormCard("threeDown", false, 0, -3));
            GameDeck.AvailableStormCards.Add(new StormCard("threeUp", false, 0, 3));
            GameDeck.AvailableStormCards.Add(new StormCard("threeLeft", false, -3, 0));
            GameDeck.AvailableStormCards.Add(new StormCard("threeRight", false, 3, 0));

            GameDeck = Shuffle(GameDeck, true, true);
            return GameDeck;
        }


        private Deck Shuffle(Deck deckToSuffle, bool stormshuffle, bool itemcardshuffle)
        {
            if (itemcardshuffle == true)
            {
                int n = deckToSuffle.AvailableItemCards.Count;
                while (n > 1) //Shuffling ItemCards
                {
                    n--;
                    int k = random.Next(n + 1);
                    ItemCard itemCardValue = deckToSuffle.AvailableItemCards[k];
                    deckToSuffle.AvailableItemCards[k] = deckToSuffle.AvailableItemCards[n];
                    deckToSuffle.AvailableItemCards[n] = itemCardValue;
                }
            }
            if (stormshuffle == true)
            {
                int n = deckToSuffle.AvailableStormCards.Count;
                while (n > 1) //Shuffling StormCards
                {
                    n--;
                    int k = random.Next(n + 1);
                    StormCard stormCardValue = deckToSuffle.AvailableStormCards[k];
                    deckToSuffle.AvailableStormCards[k] = deckToSuffle.AvailableStormCards[n];
                    deckToSuffle.AvailableStormCards[n] = stormCardValue;
                }
            }
            return deckToSuffle;
        }
        public bool GameWon(List<Player> players)
        {
            //Defining condition for the winning. If all players are on the launchpad tile which is discovered and you have all the shipparts then the game is won.
            if (players.All(x => x.X == board.LaunchPadTile.X) && players.All(x => x.Y == board.LaunchPadTile.Y) && board.LaunchPadTile.IsDiscovered == true && shipParts.All(x => x.IsPickedUp == true) == true)
            {
                return true;
            }
            return false;
        }
        public Player PlayerInit(string playerName, int turnOrder, List<Player> players)  //Roles are random now, needs testing tho
        {
            Random rng = new Random();
            int roleNumber = rng.Next(0, 6);

            Player newPlayer = new Player()
            {
                X = board.CrashStartTile.X,
                Y = board.CrashStartTile.Y, //The launchpad tile would be the finishing tile, so we need the CrashStartTile as starting position
                NumberOfActions = 4,
                ActionDescription = "ActionDescriptionLongString",    //needs to be finished
                Cards = new List<ItemCard>(),
                PlayerName = playerName,
                TurnOrder = turnOrder,
            };

            while (players.Any(x => (int)x.PlayerRoleName == roleNumber))
            {
                roleNumber = rng.Next(0, 6);
            }

            switch (roleNumber)     //Abilitylist not finished
            {
                case 0:
                    {
                        newPlayer.PlayerRoleName = RoleName.Archeologist;
                        newPlayer.WaterLevel = 3;
                        newPlayer.MaxWaterLevel = 3;
                        newPlayer.AbilityDescription = "You can remove 2 Sand markers from any single tile for 1 action";
                        //newPlayer.AbilityList
                    }
                    break;
                case 1:
                    {
                        newPlayer.PlayerRoleName = RoleName.Climber;
                        newPlayer.WaterLevel = 3;
                        newPlayer.MaxWaterLevel = 3;
                        newPlayer.AbilityDescription = "You can move to blocked tiles (tiles with 2 or more Sand markers on them). You may also take one other player with you whenever you move. Pawns on the your tile are never buried and can leave the tile containing you even if there are 2 or more Sand markers on it";
                        //newPlayer.AbilityList
                    }
                    break;
                case 2:
                    {
                        newPlayer.PlayerRoleName = RoleName.Explorer;
                        newPlayer.WaterLevel = 4;
                        newPlayer.MaxWaterLevel = 4;
                        newPlayer.AbilityDescription = "You can move, remove sand, and may use Dune Blasters diagonally";
                        //newPlayer.AbilityList
                    }
                    break;
                case 3:
                    {
                        newPlayer.PlayerRoleName = RoleName.Meteorologist;
                        newPlayer.WaterLevel = 4;
                        newPlayer.MaxWaterLevel = 4;
                        newPlayer.AbilityDescription = "You may spend actions to draw fewer Storm cards (1 card per action) at the end of your turn. You may also choose to spend 1 action to look at the top Storm cards, equal to the Storm level, and may place one at the bottom of the deck.";
                        //newPlayer.AbilityList
                    }
                    break;
                case 4:
                    {
                        newPlayer.PlayerRoleName = RoleName.Navigator;
                        newPlayer.WaterLevel = 4;
                        newPlayer.MaxWaterLevel = 4;
                        newPlayer.AbilityDescription = "You may move another player up to 3 unblocked tiles per action, including tunnels. You can move the Explorer diagonally and can move the Climber through blocked tiles. When moved in this way, the Climber can also use his power to take along 1 other player-including you!";
                        //newPlayer.AbilityList
                    }
                    break;
                case 5:
                    {
                        newPlayer.PlayerRoleName = RoleName.WaterCarrier;
                        newPlayer.WaterLevel = 5;
                        newPlayer.MaxWaterLevel = 5;
                        newPlayer.AbilityDescription = "You can take 2 water from already excavated wells for 1 action. You may also give water to players on adjacent tiles for free at any time.";
                        //newPlayer.AbilityList
                    }
                    break;
                default:
                    break;
            }

            return newPlayer;
        }
        public string MovePlayer(int newX, int newY, List<Player> players) // returns true if the player moves ---> so render only rerenders in this case
        {
            int X = players.Where(p => p.TurnOrder == 1).SingleOrDefault().X;
            int Y = players.Where(p => p.TurnOrder == 1).SingleOrDefault().Y;

            if (board.storm.X == newX && board.storm.Y == newY)
            {
                return "invalidMove";
            }
            if (DoubleSandChecker(newX, newY) && players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerRoleName != RoleName.Climber)
            {
                return "blocked";
            }
            if (DoubleSandChecker(X, Y) && players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerRoleName != RoleName.Climber)
            {
                //Making sure you can't move out of the tile when you're on a sand tile. You have to dig that first out
                return "currentBlocked";
            }
            else
            {
                if (newX > -1 && newY > -1 && newX < 5 && newY < 5)
                {
                    if (players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions > 0) // >0, because if the # of moves is < 0, you can move
                    {
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().X = newX;
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y = newY;
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("game_monopoly_game_metal_playing_piece_drop_onto_playing_board.mp3");
                        return "validMove";
                    }
                    else return "outOfActions";
                }
                else return "invalidMove";
            }
        }
        public void Endturn(List<Player> players)
        {
            players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions = 4;
            players.Where(p => p.TurnOrder == 1).FirstOrDefault().TurnOrder = 6;   // We are using maxsearch-like pattern we are copying 1 to 6 then later on 6 to 3, because the other players will have 2 or 1.
            players.Where(p => p.TurnOrder == 2).FirstOrDefault().TurnOrder -= 1;
            if (players.Count == 3)
            {
                players.Where(p => p.TurnOrder == 3).FirstOrDefault().TurnOrder -= 1;
            }
            players.Where(p => p.TurnOrder == 6).FirstOrDefault().TurnOrder = players.Count;      // either 2 or 3

            CurrentPlayer++;

            if (players.Count is 3 && CurrentPlayer is 4)
            {
                CurrentPlayer = 1;
            }
            if (players.Count is 2 && CurrentPlayer is 3)
            {
                CurrentPlayer = 1;
            }
            OnCardsMovingOnBoard(EventArgs.Empty);
        }
        public string RemoveSand(List<Player> players) // We need bool because of invalidatevisual
        {
            int x = players.Where(p => p.TurnOrder == 1).FirstOrDefault().X;
            int y = players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y;
            bool sand = SandTileChecker(x, y);
            if (sand && players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions > 0) //if we are on a sand tile and we can do an action
            {
                board.SandTiles[x, y] -= 1; //we excavate the sand
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                Sound.PlaySound("441824__jjdg__shovel-digging-sound.mp3");
                return "validMove";
            }
            else if (!sand)
            {
                return "notSand";
            }
            else return "outOfActions";
        }
        public string RemoveSandByCoordinate(int x, int y, List<Player> players)
        {
            bool sand = SandTileChecker(x, y);
            if (sand && players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions > 0) //if we are on a sand tile and we can do an action
            {
                board.SandTiles[x, y] -= 1; //we excavate the sand
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                Sound.PlaySound("441824__jjdg__shovel-digging-sound.mp3");
                return "validMove";
            }
            else if (!sand)
            {
                return "notSand";
            }
            else return "outOfActions";
        }
        public string Excavate(List<Player> players)
        {
            int x = players.Where(p => p.TurnOrder == 1).FirstOrDefault().X;
            int y = players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y;
            bool sand = SandTileChecker(x, y);
            string typeOfCard = TileNames[x, y];
            int pos = -1;
            bool cardDiscovered = IsCardDiscovered(typeOfCard, x, y);
            if (sand == false && cardDiscovered == false && players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions > 0)
            {
                switch (typeOfCard)
                {
                    case "AirShipClueTile":
                        pos = CardFinder(board.AirShipClueTiles, x, y);
                        board.AirShipClueTiles[pos].IsDiscovered = true;
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "LaunchPadTile":
                        board.LaunchPadTile.IsDiscovered = true;
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "CrashStartTile":
                        board.CrashStartTile.IsDiscovered = true;
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "TunnelTile":
                        pos = CardFinder(board.TunnelTiles, x, y);
                        board.TunnelTiles[pos].IsDiscovered = true;
                        AddGadgetCardOnExcavate();
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "Mirage":
                        pos = CardFinder(board.OasisMirageTiles, x, y);
                        board.OasisMirageTiles[pos].IsDiscovered = true;
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "Oasis":
                        pos = CardFinder(board.OasisMirageTiles, x, y);
                        board.OasisMirageTiles[pos].IsDiscovered = true;
                        players.Where(p => p.X == board.OasisMirageTiles[pos].X && p.Y == board.OasisMirageTiles[pos].Y).ToList().ForEach(x => x.WaterLevel = x.WaterLevel + 2);
                        //checking if the players have over the maximum water level
                        players.Where(p => p.WaterLevel > p.MaxWaterLevel).ToList().ForEach(x => x.WaterLevel = x.MaxWaterLevel);
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "EmptyShelter":
                        pos = CardFinder(board.ShelterTiles, x, y);
                        board.ShelterTiles[pos].IsDiscovered = true;
                        AddGadgetCardOnExcavate();
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "FriendlyWater":
                        pos = CardFinder(board.ShelterTiles, x, y);
                        board.ShelterTiles[pos].IsDiscovered = true;
                        AddGadgetCardOnExcavate();
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "FriendlyQuest":
                        pos = CardFinder(board.ShelterTiles, x, y);
                        board.ShelterTiles[pos].IsDiscovered = true;
                        AddGadgetCardOnExcavate();
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    case "Hostile":
                        pos = CardFinder(board.ShelterTiles, x, y);
                        board.ShelterTiles[pos].IsDiscovered = true;
                        AddGadgetCardOnExcavate();
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        Sound.PlaySound("zapsplat_leisure_trading_card_or_playing_card_single_turn_over_on_table_001_68328.mp3");
                        return "validMove";
                    default:
                        break;
                }
            }
            else if (cardDiscovered == true)
            {
                return "alreadyDiscovered";
            }
            else if (sand == true)
            {
                return "sandTile";
            }
            return "outOfActions";
        }
        public void AddGadgetCardOnExcavate()
        {
            //Placeholder card with a true InPlayerHand property in order the while loop can generate an actual card
            ItemCard currentItemCard = new ItemCard("Placeholder", false, true, "Long live the while loop");

            while (currentItemCard.InPlayerHand)
            {
                currentItemCard = Deck.AvailableItemCards.ElementAt(random.Next(0, Deck.AvailableItemCards.Count));
            }

            currentItemCard.InPlayerHand = true;

            if (CurrentPlayerCard1Display == null)
            {
                Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Add(currentItemCard);
                CurrentPlayerCard1Display = currentItemCard.Display;
            }
            else if (CurrentPlayerCard2Display == null)
            {
                Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Add(currentItemCard);
                CurrentPlayerCard2Display = currentItemCard.Display;
            }
            else if (CurrentPlayerCard3Display == null)
            {
                Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Add(currentItemCard);
                CurrentPlayerCard3Display = currentItemCard.Display;
            }
            else if (CurrentPLayerCard4Display == null)
            {
                Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Add(currentItemCard);
                CurrentPLayerCard4Display = currentItemCard.Display;
            }
            else if (CurrentPlayerCard5Display == null)
            {
                Players.Where(p => p.TurnOrder == 1).FirstOrDefault().Cards.Add(currentItemCard);
                CurrentPlayerCard5Display = currentItemCard.Display;
            }
            //implement something when all player card slots is filled
        }

        private bool IsCardDiscovered(string typeOfCard, int x, int y)
        {
            int pos = -1;
            bool isDiscovered = false;
            switch (typeOfCard)
            {
                case "AirShipClueTile":
                    pos = CardFinder(board.AirShipClueTiles, x, y);
                    if (board.AirShipClueTiles[pos].IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "LaunchPadTile":
                    if (board.LaunchPadTile.IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "CrashStartTile":
                    if (board.CrashStartTile.IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "TunnelTile":
                    pos = CardFinder(board.TunnelTiles, x, y);
                    if (board.TunnelTiles[pos].IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "Mirage":
                    pos = CardFinder(board.OasisMirageTiles, x, y);
                    if (board.OasisMirageTiles[pos].IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "Oasis":
                    pos = CardFinder(board.OasisMirageTiles, x, y);
                    if (board.OasisMirageTiles[pos].IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "EmptyShelter":
                    pos = CardFinder(board.ShelterTiles, x, y);
                    if (board.ShelterTiles[pos].IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "FriendlyWater":
                    pos = CardFinder(board.ShelterTiles, x, y);
                    if (board.ShelterTiles[pos].IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "FriendlyQuest":
                    pos = CardFinder(board.ShelterTiles, x, y);
                    if (board.ShelterTiles[pos].IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                case "Hostile":
                    pos = CardFinder(board.ShelterTiles, x, y);
                    if (board.ShelterTiles[pos].IsDiscovered == true)
                    {
                        isDiscovered = true;
                    }
                    break;
                default:
                    break;
            }
            return isDiscovered;
        }
        public int CardFinder(ITile[] cards, int x, int y) //here Linq doesn't work because we have to return the index of where we found the card in the given card's list
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].X == x && cards[i].Y == y)
                {
                    return i;
                }
            }
            return 0; // -1 or lower values would break the program, also the next thing is that this should always return a value in the for loop since if we caught a certain card, it certainly exists
        }
        public string ItemPickUp(List<Player> players)
        {
            int x = players.Where(p => p.TurnOrder == 1).FirstOrDefault().X;
            int y = players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y;
            bool sand = SandTileChecker(x, y);
            string typeOfItem = PartTiles[x, y];
            bool isPickedUp = false;
            if (typeOfItem == "Crystal" || typeOfItem == "Compass" || typeOfItem == "Engine" || typeOfItem == "Propeller")
            {
                isPickedUp = PartPickedChecker(x, y);
                if (sand == false && isPickedUp == false && players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions > 0)
                {
                    shipParts.Where(x => x.Name == typeOfItem).FirstOrDefault().IsPickedUp = true;
                    players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                    Sound.PlaySound("422709__niamhd00145229__inspect-item.wav");
                    return "validMove";
                }
                else if (isPickedUp == true)
                {
                    return "alreadyPickedUp";
                }
                return "outOfActions";
            }
            else
            {
                return "notAnItem";
            }
        }
        public void WaterSharing(List<Player> players, Player selectedPlayer)
        {
            int playerX = players.Where(p => p.TurnOrder == 1).FirstOrDefault().X;
            int playerY = players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y;
            int waterLevel = players.Where(p => p == selectedPlayer).SingleOrDefault().WaterLevel;
            int playerWaterLevel = players.Where(p => p.TurnOrder == 1).SingleOrDefault().WaterLevel;
            int maxWaterLevel = players.Where(p => p == selectedPlayer).SingleOrDefault().MaxWaterLevel;
            bool isWaterCarrier = players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerRoleName == RoleName.WaterCarrier;
            bool isLeft = selectedPlayer.X == playerX - 1 && selectedPlayer.Y == playerY;
            bool isRight = selectedPlayer.X == playerX + 1 && selectedPlayer.Y == playerY;
            bool isUp = selectedPlayer.Y == playerY + 1 && selectedPlayer.X == playerX;
            bool isDown = selectedPlayer.Y == playerY - 1 && selectedPlayer.X == playerX;

            if (selectedPlayer == players.Where(p => p.TurnOrder == 1).FirstOrDefault())
            {
                throw new Exception("You can't give water to yourself. Try another player.");
            }
            else if (isWaterCarrier && playerWaterLevel > 0
                 && (isLeft || isRight || isUp || isDown) && waterLevel < maxWaterLevel)
            {
                players.Where(p => p == selectedPlayer).SingleOrDefault().WaterLevel += 1;
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().WaterLevel -= 1;
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                return;
            }
            else if (isWaterCarrier == false && (isLeft || isRight || isUp || isDown))
            {
                throw new Exception("Hint: You can't give water to the selected player. You're not a water carrier. Go to their tile first.");
            }

            else if (selectedPlayer.WaterLevel == selectedPlayer.MaxWaterLevel)
            {
                throw new Exception("Hint: You can't give water to the selected player. Their water level is max.");
            }
            else if (isWaterCarrier == true && selectedPlayer.X - playerX > 1 || selectedPlayer.X - playerX < -1 || selectedPlayer.Y - playerY > 1 || selectedPlayer.Y - playerY < -1)
            {
                throw new Exception("Hint: You can't give water to the selected player as a Water Carrier. You're out of reach.");
            }

            else if (playerWaterLevel == 0)
            {
                throw new Exception("Hint: You can't give water to the selected player. You're almost dead because of thirstiness.");
            }

            else if (selectedPlayer == players.Where(p => p.TurnOrder == 1).FirstOrDefault())
            {
                throw new Exception("Hint: You can't give water to yourself. Try another player.");
            }

            else if (players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions > 0)
            {
                players.Where(p => p == selectedPlayer).SingleOrDefault().WaterLevel += 1;
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().WaterLevel -= 1;
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                return;
            }
            throw new Exception("Hint: You are out of actions.");
        }
        public string WaterCarrierRefill(List<Player> players)
        {
            //Conditions: the player is on the same coordinates, their waterlevel isn't higher than max, they're not on a sandtile, not on a mirage tile, and it's discovered,
            // and of course, the player is a water carrier, then we're all good
            int X = players.Where(p => p.TurnOrder == 1).SingleOrDefault().X;
            int Y = players.Where(p => p.TurnOrder == 1).SingleOrDefault().Y;
            int waterLevel = players.Where(p => p.TurnOrder == 1).FirstOrDefault().WaterLevel;
            int maxWaterLevel = players.Where(p => p.TurnOrder == 1).FirstOrDefault().MaxWaterLevel;
            bool sand = SandTileChecker(X, Y);
            bool isDiscovered = board.OasisMirageTiles.Any(x => x.X == X && x.Y == Y && x.IsDiscovered == true);
            bool isMirage = board.OasisMirageTiles.Any(x => x.X == X && x.Y == Y && x.IsDried == true);
            //We also have to check if it's a mirage or not
            bool sameCoordinate = board.OasisMirageTiles.Any(x => x.X == X && x.Y == Y);
            RoleName roleName = players.Where(p => p.TurnOrder == 1).SingleOrDefault().PlayerRoleName;
            if (!sand && !isMirage && isDiscovered && sameCoordinate && roleName == RoleName.WaterCarrier 
                && waterLevel < maxWaterLevel && players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions > 0)
            {
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().WaterLevel += 2;
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                //Of course we decrease the water level to max
                players.Where(p => p.WaterLevel > p.MaxWaterLevel).ToList().ForEach(x => x.WaterLevel = x.MaxWaterLevel);
                return "validMove";
            }
            else if (roleName != RoleName.WaterCarrier)
            {
                return "notWaterCarrier";
            }
            else if (sameCoordinate != true)
            {
                return "notInReach";
            }
            else if (sand == true)
            {
                return "sandTile";
            }
            else if (isDiscovered != true)
            {
                return "notDiscovered";
            }
            else if (isMirage == true)
            {
                return "mirage";
            }
            else if (waterLevel == maxWaterLevel)
            {
                return "maxWater";
            }
            else if (players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions <= 0)
            {
                return "outOfActions";
            }
            else return "";
        }
        private bool SandTilesOnBoardMaximum()   //returns true if no more sand can be placed on the board --> 48
        {
            int count = 0;
            for (int i = 0; i < board.SandTiles.GetLength(0); i++)
            {
                for (int j = 0; j < board.SandTiles.GetLength(1); j++)
                {
                    count += board.SandTiles[i, j];
                }
            }
            if (count >= 48)
            {
                return true;
            }
            return false;
        }
        public bool LoseCondition()
        {
            //lose if:
            if (StormProgress > 0.87 || SandTilesOnBoardMaximum() || players.Any(x=>x.WaterLevel < 0))
            {
                return true;
            }
            return false;
        }
        public void ReEnableDiscardedPropertyStorm()

        {
            foreach (StormCard card in Deck.AvailableStormCards)
            {
                card.IsDiscarded = false;
            }
            Deck = Shuffle(Deck, true, false);
        }
        public void TunnelTeleport(List<Player> players, TunnelTile selectedTunnelTile)
        {
            int playerX = players.Where(x => x.TurnOrder == 1).SingleOrDefault().X;
            int playerY = players.Where(x => x.TurnOrder == 1).SingleOrDefault().Y;
            int currentActions = players.Where(x => x.TurnOrder == 1).SingleOrDefault().NumberOfActions;
            int discoveredTunnels = board.TunnelTiles.Count(x=>x.IsDiscovered == true);
            bool sand = SandTileChecker(selectedTunnelTile.X, selectedTunnelTile.Y);
            if (!sand && selectedTunnelTile.IsDiscovered == true && PlayerOnTunnelTile(playerX,playerY) && discoveredTunnels > 1 && currentActions > 0)
            {
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().X = selectedTunnelTile.X;
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y = selectedTunnelTile.Y;
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                return;
            }
            else if (discoveredTunnels == 1)
            {
                throw new Exception("Hint: Discover more tunnel tiles in order to teleport.");
            }
            else if (sand)
            {
                throw new Exception("Hint: The tunnel tile is covered by sand. Remove it first.");
            }

            throw new Exception("Hint: You are out of actions.");
        }
        public bool PlayerOnTunnelTile(int X, int Y)
        {
            if (board.TunnelTiles.Any(x => x.X == X && x.Y == Y))
            {
                return true;
            }
            else return false;
        }
    }
}
