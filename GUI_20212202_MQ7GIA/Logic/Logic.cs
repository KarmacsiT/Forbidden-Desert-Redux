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

            //Parts generation
            int shipPartCounter = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i != 2 && j != 2 && shipPartCounter < 4 && random.Next(0,100) < 50)
                    {
                        shipPartCounter++;
                    }
                }
            }
        }
    }
}
