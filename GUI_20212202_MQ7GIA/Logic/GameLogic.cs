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
        Deck deck;
        GameStatus status;
        Player player;
        ShipParts[] shipParts;
        public string[,] Names { get; set; }

        Random random = new Random();
        public GameLogic()
        {
            board = new Board
            {
                TunnelTiles = new TunnelTile[3], // Okay, we have 3 tunnel tiles, not 2
                AirShipClueTiles = new AirShipClueTile[8],
                LaunchPadTile = new LaunchPadTile(),
                OasisMirageTiles = new OasisMirageTile[3],
                ShelterTiles = new ShelterTile[9],
                storm = new Storm()
            };
            board.storm.X = 2;
            board.storm.Y = 2;
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
            bool[,] isTaken = new bool[5, 5];
            Names = new string[5, 5];

            foreach (AirShipClueTile tile in board.AirShipClueTiles)
            {
                int X = random.Next(0, 5);
                int Y = random.Next(0, 5);

                //Avoid number generation for 2 and check if card is already generated
                while (X is 2 && Y is 2 || isTaken[X, Y])
                {
                    X = random.Next(0, 5);
                    Y = random.Next(0, 5);
                }
                tile.X = X;
                tile.Y = Y;
                isTaken[X, Y] = true;
                Names[X, Y] = "AirShipClueTile";
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

            foreach (ShipParts part in shipParts)
            {
                part.X = board.AirShipClueTiles[cardCounter].X;
                part.Y = board.AirShipClueTiles[cardCounter + 1].Y;
                Names[part.X, part.Y] = board.AirShipClueTiles[cardCounter].PartName;
                cardCounter += 2;
            }

            // LaunchPadTile 
            board.LaunchPadTile = new LaunchPadTile();
            board.LaunchPadTile.X = CoordinateGiver(isTaken)[0];
            board.LaunchPadTile.Y = CoordinateGiver(isTaken)[1];
            Names[board.LaunchPadTile.X, board.LaunchPadTile.Y] = "LaunchPadTile";

            // TunnelTiles && OasisMirageTiles
            bool dry = false;

            for (int i = 0; i < 3; i++)
            {
                board.TunnelTiles[i] = new TunnelTile();
                board.TunnelTiles[i].X = CoordinateGiver(isTaken)[0];
                board.TunnelTiles[i].Y = CoordinateGiver(isTaken)[1];
                Names[board.TunnelTiles[i].X, board.TunnelTiles[i].Y] = "TunnelTile";

                board.OasisMirageTiles[i] = new OasisMirageTile();
                board.OasisMirageTiles[i].X = CoordinateGiver(isTaken)[0];
                board.OasisMirageTiles[i].Y = CoordinateGiver(isTaken)[1];

                if (dry == false)
                {
                    int chance = random.Next(0, 3);
                    if (chance == 0)
                    {
                        board.OasisMirageTiles[i].IsDried = true;
                    }
                    dry = true;
                }
                else
                {
                    board.OasisMirageTiles[i].IsDried = false;
                }

                if (board.OasisMirageTiles[i].IsDried == false)
                {
                    Names[board.OasisMirageTiles[i].X, board.OasisMirageTiles[i].Y] = "Oasis";
                }
                else Names[board.OasisMirageTiles[i].X, board.OasisMirageTiles[i].Y] = "Mirage";
            }

            // ShelterTiles
            for (int i = 0; i < board.ShelterTiles.Length; i++)
            {
                board.ShelterTiles[i] = new ShelterTile();
            }

            foreach (ShelterTile tile in board.ShelterTiles)
            {
                for (int x = 0; x < isTaken.GetLength(0); x++)
                {
                    for (int y = 0; y < isTaken.GetLength(1); y++)
                    {
                        if (isTaken[x, y] is false)
                        {
                            tile.X = x;
                            tile.Y = y;
                        }
                    }
                }
                int chance = random.Next(0, 101);
                if (chance <= 50)
                {
                    tile.ShelterType = ShelterVariations.Empty;
                    Names[tile.X, tile.Y] = "EmptyShelter";
                }
                else if (chance > 50 && chance <= 65)
                {
                    tile.ShelterType = ShelterVariations.FriendlyWater;
                    Names[tile.X, tile.Y] = "FriendlyWater";
                }
                else if (chance > 65 && chance <= 80)
                {
                    tile.ShelterType = ShelterVariations.FriendlyQuest;
                    Names[tile.X, tile.Y] = "FriendlyQuest";
                }
                else
                {
                    tile.ShelterType = ShelterVariations.Hostile;
                    Names[tile.X, tile.Y] = "Hostile";
                }
            }

        }

        private int[] CoordinateGiver(bool[,] isTaken)
        {
            int[] result = new int[2];   // 0--X , 1 ---Y
            int x = random.Next(0, 5);
            int y = random.Next(0, 5);
            while (x is 2 && y is 2 && isTaken[x, y] is not false)  //The coordinate can't be the coordinate of the storm start coordinate which is [2,2] or taken
            {
                x = random.Next(0, 5);
                y = random.Next(0, 5);
            }
            isTaken[x, y] = true;
            result[0] = x;
            result[1] = y;
            return result;
        }
    }
}
