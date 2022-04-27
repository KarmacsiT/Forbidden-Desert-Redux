using GUI_20212202_MQ7GIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Logic
{
    public class GameLogic
    {
        public Board board { get; set; }
        public Deck Deck { get; set; }
        public GameStatus Status { get; set; }
       // public List<Player> Players { get; set; }   we may not need this, since in the display we store them, and here so far it was not needed
        public ShipParts[] shipParts { get; set; }
        public Sound Sound { get; set; }
        public string[,] TileNames { get; set; }
        public string[,] PartTiles { get; set; }
        public string DifficultyLevel { get; set; }

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

                //Avoid number generation for 2 and check if card is already generated
                while (isTaken[X, Y])
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
        public void MoveStorm(int x, int y)
        {
            if (x != 0)
            {
                for (int i = 0; i < Math.Abs(x); i++)
                {
                    if (board.storm.X + 1 == 5 || board.storm.X - 1 == -1)
                    {
                        //nothing happens, this is the case when the storm is on the edge
                    }
                    else
                    {
                        board.SandTiles[board.storm.X, board.storm.Y] += 1;
                        if (x > 0)
                        {
                            string tempname = TileNames[board.storm.X + 1, board.storm.Y];
                            TileNames[board.storm.X + 1, board.storm.Y] = "Storm";
                            TileNames[board.storm.X, board.storm.Y] = tempname;

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
                                default:      //Shelters left
                                    {
                                        board.ShelterTiles.First(x => x.X == board.storm.X + 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                            }
                            board.storm.X += 1;
                            board.SandTiles[board.storm.X, board.storm.Y] = 0;
                        }
                        else
                        {
                            string tempname = TileNames[board.storm.X - 1, board.storm.Y];
                            TileNames[board.storm.X - 1, board.storm.Y] = "Storm";
                            TileNames[board.storm.X, board.storm.Y] = tempname;

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
                                default:      //Shelters left
                                    {
                                        board.ShelterTiles.First(x => x.X == board.storm.X - 1 && x.Y == board.storm.Y).X = board.storm.X;
                                    }
                                    break;
                            }
                            board.storm.X -= 1;
                            board.SandTiles[board.storm.X, board.storm.Y] = 0;
                        }
                        PartTileCoordinateGiver();
                    }
                }
            }
            else
            {
                Sound.PlaySound("CHIMES.WAV");
                for (int i = 0; i < Math.Abs(y); i++)
                {
                    if (board.storm.Y + 1 == 5 || board.storm.Y - 1 == -1)
                    {
                        //nothing happens, this is the case when the storm is on the edge
                    }
                    else
                    {
                        board.SandTiles[board.storm.X, board.storm.Y] += 1;
                        if (y > 0)
                        {
                            string tempname = TileNames[board.storm.X, board.storm.Y + 1];
                            TileNames[board.storm.X, board.storm.Y + 1] = "Storm";
                            TileNames[board.storm.X, board.storm.Y] = tempname;

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
                                        board.CrashStartTile.Y = board.LaunchPadTile.Y = board.storm.Y;
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
                            board.SandTiles[board.storm.X, board.storm.Y] = 0;
                        }
                        else
                        {
                            string tempname = TileNames[board.storm.X, board.storm.Y - 1];
                            TileNames[board.storm.X, board.storm.Y - 1] = "Storm";
                            TileNames[board.storm.X, board.storm.Y] = tempname;
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
                                        board.CrashStartTile.Y = board.LaunchPadTile.Y = board.storm.Y;
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
                            board.SandTiles[board.storm.X, board.storm.Y] = 0;
                        }
                        PartTileCoordinateGiver();
                    }
                }
            }
        }

        private Deck DeckGeneration()
        {
            Random rng = new Random();
            Deck GameDeck = new Deck();

            ItemCard duneBlaster = new ItemCard("Dune Blaster", false, false);
            ItemCard jetPack = new ItemCard("Jet Pack", false, false);
            ItemCard secretWaterReserve = new ItemCard("Secret Water Reserve", false, false);
            ItemCard solarShield = new ItemCard("Solar Shield", false, false);
            ItemCard stormTracker = new ItemCard("Storm Tracker", false, false);
            ItemCard terraScope = new ItemCard("Terrascope", false, false);
            ItemCard timeThrottle = new ItemCard("Time Throttle", false, false);

            StormCard oneDown = new StormCard("oneDown", false, 0, -1);
            StormCard oneLeft = new StormCard("oneLeft", false, -1, 0);
            StormCard oneRight = new StormCard("oneRight", false, 1, 0);
            StormCard oneUp = new StormCard("oneUp", false, 0, 1);

            StormCard twoDown = new StormCard("twoDown", false, 0, -2);
            StormCard twoLeft = new StormCard("twoLeft", false, -2, 0);
            StormCard twoRight = new StormCard("twoRight", false, 2, 0);
            StormCard twoUp = new StormCard("twoUp", false, 0, 2);

            StormCard threeDown = new StormCard("threeDown", false, 0, -3);
            StormCard threeLeft = new StormCard("threeLeft", false, -3, 0);
            StormCard threeRight = new StormCard("threeRight", false, 3, 0);
            StormCard threeUp = new StormCard("threeUp", false, 0, 3);

            for (int i = 0; i < 3; i++)
            {
                GameDeck.AvailableItemCards.Add(duneBlaster);
                GameDeck.AvailableItemCards.Add(jetPack);

                GameDeck.AvailableStormCards.Add(oneDown);
                GameDeck.AvailableStormCards.Add(oneUp);
                GameDeck.AvailableStormCards.Add(oneLeft);
                GameDeck.AvailableStormCards.Add(oneRight);
            }

            for (int i = 0; i < 2; i++)
            {
                GameDeck.AvailableItemCards.Add(solarShield);
                GameDeck.AvailableItemCards.Add(terraScope);

                GameDeck.AvailableStormCards.Add(twoDown);
                GameDeck.AvailableStormCards.Add(twoUp);
                GameDeck.AvailableStormCards.Add(twoLeft);
                GameDeck.AvailableStormCards.Add(twoRight);
            }

            GameDeck.AvailableItemCards.Add(secretWaterReserve);
            GameDeck.AvailableItemCards.Add(stormTracker);
            GameDeck.AvailableItemCards.Add(timeThrottle);

            GameDeck.AvailableStormCards.Add(threeDown);
            GameDeck.AvailableStormCards.Add(threeUp);
            GameDeck.AvailableStormCards.Add(threeLeft);
            GameDeck.AvailableStormCards.Add(threeRight);


            int n = GameDeck.AvailableItemCards.Count;
            while (n > 1) //Shuffling ItemCards
            {
                n--;
                int k = rng.Next(n + 1);
                ItemCard itemCardValue = GameDeck.AvailableItemCards[k];
                GameDeck.AvailableItemCards[k] = GameDeck.AvailableItemCards[n];
                GameDeck.AvailableItemCards[n] = itemCardValue;
            }

            n = GameDeck.AvailableStormCards.Count;
            while (n > 1) //Shuffling StormCards
            {
                n--;
                int k = rng.Next(n + 1);
                StormCard stormCardValue = GameDeck.AvailableStormCards[k];
                GameDeck.AvailableStormCards[k] = GameDeck.AvailableStormCards[n];
                GameDeck.AvailableStormCards[n] = stormCardValue;
            }

            return GameDeck;
        }
        public Player PlayerInit(string playerName, int turnOrder, List<Player> players)  //Roles are random now, needs testing tho
        {
            Random rng = new Random();
            int roleNumber = rng.Next(0, 6); // Do we always need an archeologist? (roleNumber 0)

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
                        newPlayer.AbilityDescription = "You can remove 2 Sand markers from any single tile for 1 action";
                        //newPlayer.AbilityList
                    }
                    break;
                case 1:
                    {
                        newPlayer.PlayerRoleName = RoleName.Climber;
                        newPlayer.WaterLevel = 3;
                        newPlayer.AbilityDescription = "You can move to blocked tiles (tiles with 2 or more Sand markers on them). You may also take one other player with you whenever you move. Pawns on the your tile are never buried and can leave the tile containing you even if there are 2 or more Sand markers on it";
                        //newPlayer.AbilityList
                    }
                    break;
                case 2:
                    {
                        newPlayer.PlayerRoleName = RoleName.Explorer;
                        newPlayer.WaterLevel = 4;
                        newPlayer.AbilityDescription = "You can move, remove sand, and may use Dune Blasters diagonally";
                        //newPlayer.AbilityList
                    }
                    break;
                case 3:
                    {
                        newPlayer.PlayerRoleName = RoleName.Meteorologist;
                        newPlayer.WaterLevel = 4;
                        newPlayer.AbilityDescription = "You may spend actions to draw fewer Storm cards (1 card per action) at the end of your turn. You may also choose to spend 1 action to look at the top Storm cards, equal to the Storm level, and may place one at the bottom of the deck.";
                        //newPlayer.AbilityList
                    }
                    break;
                case 4:
                    {
                        newPlayer.PlayerRoleName = RoleName.Navigator;
                        newPlayer.WaterLevel = 4;
                        newPlayer.AbilityDescription = "You may move another player up to 3 unblocked tiles per action, including tunnels. You can move the Explorer diagonally and can move the Climber through blocked tiles. When moved in this way, the Climber can also use his power to take along 1 other player-including you!";
                        //newPlayer.AbilityList
                    }
                    break;
                case 5:
                    {
                        newPlayer.PlayerRoleName = RoleName.WaterCarrier;
                        newPlayer.WaterLevel = 5;
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
            if(board.storm.X == newX && board.storm.Y == newY)
            {
                return "invalidMove";
            }
            if(DoubleSandChecker(newX, newY) && players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerRoleName != RoleName.Climber)
            {
                return "blocked";
            }
            else
            {
                if (newX > -1 && newY > -1 && newX < 5 && newY < 5)
                {
                    if (players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions != 0)
                    {
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().X = newX;
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y = newY;
                        players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                        return "validMove";
                    }
                    return "outOfActions";
                }
                return "invalidMove";
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
        }
        public bool RemoveSand(List<Player> players) // We need bool because of invalidatevisual
        {
            int x = players.Where(p => p.TurnOrder == 1).FirstOrDefault().X;
            int y = players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y;
            bool sand = SandTileChecker(x,y);
            if (sand && players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions > 0) //if we are on a sand tile and we can do an action
            {
                board.SandTiles[x,y] -= 1; //we excavate the sand
                players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions -= 1;
                return true; 
            }
            return false;
        }
        public bool Excavate(List<Player> players)
        {
            //implement Excavate (flip the card!!)
            return false;
        }
    }
}
