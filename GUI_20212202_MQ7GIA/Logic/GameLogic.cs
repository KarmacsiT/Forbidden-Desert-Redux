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
        public List<Player> Players { get; set; }
        public ShipParts[] shipParts { get; set; }
        public string[,] TileNames { get; set; }
        public string[,] PartTiles { get; set; }

        Random random = new Random();
        public GameLogic()
        {
            bool[,] isTaken = new bool[5, 5];
            TileNames = new string[5, 5];

            board = new Board
            {
                TunnelTiles = new TunnelTile[3], // Okay, we have 3 tunnel tiles, not 2
                AirShipClueTiles = new AirShipClueTile[8],
                LaunchPadTile = new LaunchPadTile(),
                OasisMirageTiles = new OasisMirageTile[3],
                ShelterTiles = new ShelterTile[9],
                SandTiles = new int[5,5],
                storm = new Storm()
            };
            board.storm.X = 2;
            board.storm.Y = 2;
            TileNames[board.storm.X, board.storm.Y] = "Storm";
            isTaken[board.storm.X, board.storm.Y] = true;

            shipParts = new ShipParts[4];
            shipParts[0] = new ShipParts { Name = "Crystal"};
            shipParts[1] = new ShipParts { Name = "Engine"};
            shipParts[2] = new ShipParts { Name = "Compass"};
            shipParts[3] = new ShipParts { Name = "Propeller"};
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
            cardCounter = 0;

            PartTiles = new string[5, 5];

            foreach (ShipParts part in shipParts)
            {
                part.X = board.AirShipClueTiles[cardCounter].X;
                part.Y = board.AirShipClueTiles[cardCounter + 1].Y;
                PartTiles[part.X, part.Y] = board.AirShipClueTiles[cardCounter].PartName;
                cardCounter += 2;
            }

            // LaunchPadTile 
            int[] coordinates = CoordinateGiver(isTaken);
            board.LaunchPadTile = new LaunchPadTile();
            board.LaunchPadTile.X = coordinates[0];
            board.LaunchPadTile.Y = coordinates[1];
            isTaken[board.LaunchPadTile.X, board.LaunchPadTile.Y] = true;
            TileNames[board.LaunchPadTile.X, board.LaunchPadTile.Y] = "LaunchPadTile";

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
                    if (x == X && y == Y && board.SandTiles[x,y] > 0)
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
        public void MoveStorm(int x, int y)
        {
            if(x != 0)
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
                        if(x>0)
                        {
                            board.storm.X += 1;
                        }
                        else
                        {
                            board.storm.X -= 1;
                        }
                    }
                }
            }
            else
            {
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
                            board.storm.Y += 1;
                        }
                        else
                        {
                            board.storm.Y -= 1;
                        }
                    }
                }
            }           
        }
        private Player PlayerInit(string playerName, int turnOrder, int rolenum)  //This time, roles are not random
        {
            Player newPlayer = new Player()
            {
                X = board.LaunchPadTile.X,
                Y = board.LaunchPadTile.Y,
                NumberOfActions = 4,
                ActionDescription = "ActionDescriptionLongString",    //needs to be finished
                Cards = new List<ItemCard>(),
                PlayerName = playerName,
                TurnOrder = turnOrder,               
            };
            switch (rolenum)     //Abilitylist not finished
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
    }
}
