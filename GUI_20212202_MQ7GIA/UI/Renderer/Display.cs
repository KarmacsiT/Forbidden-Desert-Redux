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
    public class Display : FrameworkElement
    {
        // Models
        GameLogic logic;
        Size size;
        GameSetupWindow setupWindow;
        MediaPlayer player = new MediaPlayer();
        List<Player> players = new List<Player>();
        List<string> colors = new List<string>();

        public void SetupLogic(GameLogic logic)
        {
            this.logic = logic;
            //Storm playing and looping
            player.Open(new Uri(Path.Combine("ImageAssets/Tiles", "storm.gif"), UriKind.RelativeOrAbsolute));
            player.Play();
            player.MediaEnded += LoopGif;
        }
        public void SetupGameSetup(GameSetupWindow setupWindow)
        {
            this.setupWindow = setupWindow;
            //playerGeneration
            if (setupWindow.PlayerThreeName is null && players.Count == 0)
            {
                players.Add(logic.PlayerInit(setupWindow.PlayerOneName, 1, players));
                players.Add(logic.PlayerInit(setupWindow.PlayerTwoName, 2, players));
            }

            else if (players.Count == 0)
            {
                players.Add(logic.PlayerInit(setupWindow.PlayerOneName, 1, players));
                players.Add(logic.PlayerInit(setupWindow.PlayerTwoName, 2, players));
                players.Add(logic.PlayerInit(setupWindow.PlayerThreeName, 3, players));
            }

            //Create some logic that matches the role to the piece color

            foreach (Player player in players)
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


            double tileWidth = size.Width / 5;
            double tileHeight = size.Height / 5;

            for (int x = 0; x < 5; x++)
            {

                for (int y = 0; y < 5; y++)
                {
                    ImageBrush brush;
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
            if (x != 0 && y != 0)  //diagonal movement
            {
                if (players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerRoleName == RoleName.Explorer)
                {
                    invalidate = logic.MovePlayer(players.Where(p => p.TurnOrder == 1).FirstOrDefault().X + x, players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y + y, players);
                }
            }
            else
            {
                invalidate = logic.MovePlayer(players.Where(p => p.TurnOrder == 1).FirstOrDefault().X + x, players.Where(p => p.TurnOrder == 1).FirstOrDefault().Y + y, players);
            }
            return invalidate;
        }
        public void EndTurn()
        {
            logic.Endturn(players);
        }
        public bool RemoveSand()
        {
            return logic.RemoveSand(players);
        }
        public bool Excavate()
        {
            //implement it!!!
            return true;
        }
    }
}
