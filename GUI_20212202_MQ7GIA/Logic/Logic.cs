using GUI_20212202_MQ7GIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_MQ7GIA.Logic
{
    public class Logic
    {
        Board board;
        Deck deck;
        GameStatus status;
        Player player;
        ShipParts[] shipParts;

        public Logic()
        {
            Random random = new Random();
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
                board.AirShipClueTiles[i].X = -1;
                board.AirShipClueTiles[i].Y = -1;
            }
            //AirShipClueTile generation
            foreach (AirShipClueTile tile in board.AirShipClueTiles)
            {
                int X = random.Next(0, 4);
                int Y = random.Next(0, 4);
                //Avoid number generation for 2 and check if card is already generated
                while (X != 2 && Y != 2 && (board.AirShipClueTiles.Any(coord => coord.X == X) && board.AirShipClueTiles.Any(coord => coord.Y == Y)))
                {
                    X = random.Next(0, 4);
                    Y = random.Next(0, 4);
                }
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
                else if (cardCounter<4) tile.PartName = shipParts[1].Name;
                else if (cardCounter<6) tile.PartName = shipParts[2].Name;
                else tile.PartName = shipParts[3].Name;
                cardCounter++;
            }
        }
    }
}
