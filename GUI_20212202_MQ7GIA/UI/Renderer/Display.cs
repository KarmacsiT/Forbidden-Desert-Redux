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

        public void SetupModel(GameLogic logic)
        {
            this.logic = logic;
        }
        public void Resize(Size size)
        {
            this.size = size;
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
                        case "Storm":
                            {
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "storm.gif"), UriKind.RelativeOrAbsolute)));
                            }
                            break;
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


                    if (logic.SandTileChecker(x, y))
                    {
                        drawingContext.DrawRectangle(SandBrush, new Pen(Brushes.Black, 1), new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                    if (logic.DoubleSandChecker(x, y))
                    {
                        drawingContext.DrawRectangle(DoubleSandBrush, new Pen(Brushes.Black, 1), new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }

                    int startX = logic.board.CrashStartTile.X;
                    int startY = logic.board.CrashStartTile.Y;


                    //Piece draw for when only one player is standing on the starting tile or any tile
                    //drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "white_piece.png"), UriKind.RelativeOrAbsolute)), new Rect(startX * tileWidth + 43, startY * tileHeight + 25, tileWidth / 3.5, tileHeight / 2));

                    //Piece draw when two players are on the same tile
                    //drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "black_piece.png"), UriKind.RelativeOrAbsolute)), new Rect(startX * tileWidth + 10, startY * tileHeight + 25, tileWidth / 3.5, tileHeight / 2));

                    //drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "yellow_piece.png"), UriKind.RelativeOrAbsolute)), new Rect(startX * tileWidth + 75, startY * tileHeight + 25, tileWidth / 3.5, tileHeight / 2));


                    //Piece draw when three players are on the same tile
                    drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "black_piece.png"), UriKind.RelativeOrAbsolute)), new Rect(startX * tileWidth + 10, startY * tileHeight + 5, tileWidth / 3.5, tileHeight / 2));

                    drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "yellow_piece.png"), UriKind.RelativeOrAbsolute)), new Rect(startX * tileWidth + 75, startY * tileHeight + 5, tileWidth / 3.5, tileHeight / 2));

                    drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("ImageAssets/Pieces", "red_piece.png"), UriKind.RelativeOrAbsolute)), new Rect(startX * tileWidth + 43, startY * tileHeight + 50, tileWidth / 3.5, tileHeight / 2));

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

    }
}
