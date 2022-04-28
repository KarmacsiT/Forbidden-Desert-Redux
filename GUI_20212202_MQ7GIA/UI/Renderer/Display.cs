using GUI_20212202_MQ7GIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GUI_20212202_MQ7GIA.Logic;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;

namespace GUI_20212202_MQ7GIA.UI.Renderer
{
    public class Display : FrameworkElement, IDisplay
    {
        // Models
        GameLogic logic;
        Size size;
        MediaPlayer player = new MediaPlayer();
        public List<Player> players = new List<Player>();
        List<string> colors = new List<string>();

        public void SetupLogic(GameLogic logic, List<Player> players, List<string> colors)
        {
            this.logic = logic;
            this.players = players;
            this.colors = colors;
            //Storm playing and looping
            player.Open(new Uri(Path.Combine("ImageAssets/Tiles", "storm.gif"), UriKind.RelativeOrAbsolute));
            player.Play();
            player.MediaEnded += LoopGif;
        }
        public int TurnsCounter
        {
            get
            {
                if (players.Count == 0)
                {
                    return 4;
                }
                else return players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions; 
            }
            set
            {
                TurnsCounter = value;
            }
        }
        public void Resize(Size size)
        {
            this.size = size;
        }
        void LoopGif(object sender, EventArgs e)
        {
            player.Position = new TimeSpan(0, 0, 1);
            player.Play();
        }
        public Brush SandBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Sand", "sand.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush DoubleSandBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Sand", "blocked.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush BlackPiece
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "black_piece.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush WhitePiece
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "white_piece.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BluePiece
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "blue_piece.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush YellowPiece
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "yellow_piece.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush GreenPiece
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "green_piece.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush RedPiece
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "red_piece.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            bool discovered = false;

            double tileWidth = size.Width / 5;
            double tileHeight = size.Height / 5;

            for (int x = 0; x < 5; x++)
            {

                for (int y = 0; y < 5; y++)
                {
                    ImageBrush brush = null;
                    string typeOfCard = logic.TileNames[x, y];
                    int pos = -1;
                    
                    switch (typeOfCard)
                    {
                        case "AirShipClueTile":
                            pos = logic.CardFinder(logic.board.AirShipClueTiles, x, y);
                            if (logic.board.AirShipClueTiles[pos].IsDiscovered == true)
                            {
                                //we need 8 conditions since we have both X and Y pointers, also 4 shipparts
                                switch (logic.board.AirShipClueTiles[pos].PartName)
                                {
                                    case "Crystal":
                                        if (logic.board.AirShipClueTiles[pos].Direction == 'X')
                                        {
                                            brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Crystal Clue Tile Column.png"), UriKind.RelativeOrAbsolute)));
                                        }
                                        else
                                        {
                                            brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Crystal Clue Tile Row.png"), UriKind.RelativeOrAbsolute)));
                                        }
                                        discovered = true;
                                        break;
                                    case "Engine":
                                        if (logic.board.AirShipClueTiles[pos].Direction == 'X')
                                        {
                                            brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Engine Clue Tile Column.png"), UriKind.RelativeOrAbsolute)));
                                        }
                                        else
                                        {
                                            brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Engine Clue Tile Row.png"), UriKind.RelativeOrAbsolute)));
                                        }
                                        discovered = true;
                                        break;
                                    case "Compass":
                                        if (logic.board.AirShipClueTiles[pos].Direction == 'X')
                                        {
                                            brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Compass Clue Tile Column.png"), UriKind.RelativeOrAbsolute)));
                                        }
                                        else
                                        {
                                            brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Compass Clue Tile Row.png"), UriKind.RelativeOrAbsolute)));
                                        }
                                        discovered = true;
                                        break;
                                    case "Propeller":
                                        if (logic.board.AirShipClueTiles[pos].Direction == 'X')
                                        {
                                            brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Propeller Clue Tile Column.png"), UriKind.RelativeOrAbsolute)));
                                        }
                                        else
                                        {
                                            brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Propeller Clue Tile  Row.png"), UriKind.RelativeOrAbsolute)));
                                        }
                                        discovered = true;
                                        break;
                                    default:
                                        brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tile Backside.png"), UriKind.RelativeOrAbsolute))); //we need a default case...
                                        discovered = false;
                                        break;
                                }
                                
                            }
                            break;
                        case "LaunchPadTile":
                            if (logic.board.LaunchPadTile.IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Launchpad Tile.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        case "CrashStartTile":
                            if (logic.board.CrashStartTile.IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Crash Tile.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        case "TunnelTile":
                            pos = logic.CardFinder(logic.board.TunnelTiles, x, y);
                            if (logic.board.TunnelTiles[pos].IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tunnel.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        case "Mirage":
                            pos = logic.CardFinder(logic.board.OasisMirageTiles, x, y);
                            if (logic.board.OasisMirageTiles[pos].IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Mirage.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        case "Oasis":
                            pos = logic.CardFinder(logic.board.OasisMirageTiles, x, y);
                            if (logic.board.OasisMirageTiles[pos].IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Oasis.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        case "EmptyShelter":
                            pos = logic.CardFinder(logic.board.ShelterTiles, x, y);
                            if (logic.board.ShelterTiles[pos].IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Shelter_1.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        case "FriendlyWater":
                            pos = logic.CardFinder(logic.board.ShelterTiles, x, y);
                            if (logic.board.ShelterTiles[pos].IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Shelter_2.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        case "FriendlyQuest":
                            pos = logic.CardFinder(logic.board.ShelterTiles, x, y);
                            if (logic.board.ShelterTiles[pos].IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Shelter_3.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        case "Hostile":
                            pos = logic.CardFinder(logic.board.ShelterTiles, x, y);
                            if (logic.board.ShelterTiles[pos].IsDiscovered == true)
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Shelter_4.png"), UriKind.RelativeOrAbsolute)));
                                discovered = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (discovered == false)
                    {
                        switch (logic.TileNames[x, y])
                        {
                            //case "Storm":
                            //    {
                            //        brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "storm.gif"), UriKind.RelativeOrAbsolute)));
                            //    }
                            //    break;
                            case "AirShipClueTile":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tile Backside.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                            case "LaunchPadTile":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tile Backside.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                            case "TunnelTile":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tile Backside.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                            case "Mirage":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Oasis Tile.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                            case "Oasis":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Oasis Tile.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                            case "CrashStartTile":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Crash Tile.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;

                            case "EmptyShelter":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tile Backside.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                            case "FriendlyWater":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tile Backside.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                            case "FriendlyQuest":
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tile Backside.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                            default:
                                {
                                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tile Backside.png"), UriKind.RelativeOrAbsolute)));
                                }
                                break;
                        }
                    }
                    if (brush is not null)
                    {
                        drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 1), new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                    if (x == logic.board.storm.X && y == logic.board.storm.Y)
                    {
                        //player.Open(new Uri("A:\\4\\Programming 4\\game project\\GUI_20212202_MQ7GIA\\imageassets\\Tiles\\storm.gif"));
                        player.Open(new Uri(Path.Combine("ImageAssets/Tiles", "storm.gif"), UriKind.RelativeOrAbsolute));
                        player.Play();
                        drawingContext.DrawVideo(player, new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }

                    if (logic.SandTileChecker(x, y))
                    {
                        drawingContext.DrawRectangle(SandBrush, new Pen(Brushes.Black, 1), new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                    if (logic.DoubleSandChecker(x, y))
                    {
                        drawingContext.DrawRectangle(DoubleSandBrush, new Pen(Brushes.Black, 1), new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }


                    //Piece draw for when only one player is standing on the starting tile or any tile
                    //drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "white_piece.png"), UriKind.RelativeOrAbsolute)), new Rect(startX * tileWidth + 43, startY * tileHeight + 25, tileWidth / 3.5, tileHeight / 2));

                    //Piece draw when two players are on the same tile
                    if (players.Count == 2)
                    {
                        drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", colors[0]), UriKind.RelativeOrAbsolute)), new Rect(players[0].X * tileWidth + 10, players[0].Y * tileHeight + 25, tileWidth / 3.5, tileHeight / 2));
                        drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", colors[1]), UriKind.RelativeOrAbsolute)), new Rect(players[1].X * tileWidth + 75, players[1].Y * tileHeight + 25, tileWidth / 3.5, tileHeight / 2));
                    }
                    else
                    {
                        drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", colors[0]), UriKind.RelativeOrAbsolute)), new Rect(players[0].X * tileWidth + 10, players[0].Y * tileHeight + 5, tileWidth / 3.5, tileHeight / 2));
                        drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", colors[1]), UriKind.RelativeOrAbsolute)), new Rect(players[1].X * tileWidth + 75, players[1].Y * tileHeight + 5, tileWidth / 3.5, tileHeight / 2));
                        drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", colors[2]), UriKind.RelativeOrAbsolute)), new Rect(players[2].X * tileWidth + 43, players[2].Y * tileHeight + 50, tileWidth / 3.5, tileHeight / 2));
                    }
                    //


                    //Piece draw when three players are on the same tile


                    //if (x == logic.board.storm.X && y == logic.board.storm.Y) //Storm Render
                    //{
                    //    drawingContext.DrawRectangle(Brushes.Coral, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                    //}

                    //else
                    //{
                    //    drawingContext.DrawRectangle(Brushes.Yellow, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                    //}
                    discovered = false;
                }
            }
        }
        public void MoveTheStorm(int x, int y)
        {
            logic.MoveStorm(x, y);
        }
        public bool MoveThePlayer(int x, int y)
        {
            bool invalidate = false;
            string validationMessage = String.Empty;
            if (x != 0 && y != 0)  //diagonal movement
            {
                if (players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerRoleName == RoleName.Explorer)
                {
                    validationMessage = logic.MovePlayer(players.Where(p => p.TurnOrder == 1).FirstOrDefault().X + x, players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y + y, players);

                    if (validationMessage is "validMove")
                    {
                        invalidate = true;
                    }
                    else if (validationMessage is "blocked")
                    {
                        MessageBox.Show("Hint: You can't move to this tile because it is blocked.");
                        invalidate = false;
                    }
                    else if (validationMessage is "outOfActions")
                    {
                        MessageBox.Show("Hint: You can't move anymore because you are out of actions.");
                        invalidate = false;
                    }
                }
            }
            else
            {
                validationMessage = logic.MovePlayer(players.Where(p => p.TurnOrder == 1).FirstOrDefault().X + x, players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y + y, players);

                if (validationMessage is "validMove")
                {
                    invalidate = true;
                }
                else if (validationMessage is "blocked")
                {
                    MessageBox.Show("Hint: You can't move to this tile because it is blocked.");
                    invalidate = false;
                }
                else if (validationMessage is "outOfActions")
                {
                    MessageBox.Show("Hint: You can't move anymore because you are out of actions.");
                    invalidate = false;
                }
            }
            return invalidate;
        }
        public void EndTurn()
        {
            logic.Endturn(players);
        }
        public bool RemoveSand()
        {
            bool invalidate = false;
            string validationMessage = logic.RemoveSand(players);
            if (validationMessage == "validMove")
            {
                invalidate = true;
            }
            else if (validationMessage == "outOfActions")
            {
                MessageBox.Show("Hint: You can't remove the sand because you are out of actions.");
            }
            else if (validationMessage == "notSand")
            {
                MessageBox.Show("Hint: This is not a sand tile.");
            }
            return invalidate;
        }
        public bool Excavate()
        {
            bool invalidate = false;
            string validationMessage = logic.Excavate(players);
            if (validationMessage == "validMove")
            {
                invalidate = true;
            }
            else if (validationMessage == "outOfActions")
            {
                MessageBox.Show("Hint: You can't excavate because you are out of actions.");
            }
            else if (validationMessage == "alreadyDiscovered")
            {
                MessageBox.Show("Hint: You've already discovered the tile.");
            }
            return invalidate;
        }
    }
}
