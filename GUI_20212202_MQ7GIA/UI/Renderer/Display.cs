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
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets", "placeholder.png"), UriKind.RelativeOrAbsolute))); //REPLACE THIS WITH PROPER SAND ASSET!!!
            }
        }

        public Brush DoubleSandBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets", "placeholder.png"), UriKind.RelativeOrAbsolute))); //REPLACE THIS WITH PROPER DOUBLE SAND ASSET!!!
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
                    SolidColorBrush brush;
                    switch (logic.TileNames[x,y])
                    {
                        case "Storm":
                            {
                                brush = Brushes.Coral;
                            }
                            break;
                        case "AirShipClueTile":
                            {
                                brush = Brushes.Green;
                            }
                            break;
                        case "LaunchPadTile":
                            {
                                brush = Brushes.Black;
                            }
                            break;
                        case "TunnelTile":
                            {
                                brush = Brushes.Brown;
                            }
                            break;
                        case "Mirage":
                            {
                                brush = Brushes.Gray;
                            }
                            break;
                        case "Oasis":
                            {
                                brush = Brushes.Blue;
                            }
                            break;
                        case "EmptyShelter":
                            {
                                brush = Brushes.Red;
                            }
                            break;
                        case "FriendlyWater":
                            {
                                brush = Brushes.Purple;
                            }
                            break;
                        case "FriendlyQuest":
                            {
                                brush = Brushes.Pink;
                            }
                            break;
                        default:
                            {
                                brush = Brushes.Yellow;
                            }
                            break;
                    }
                    drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 1), new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    if (logic.SandTileChecker(x,y))
                    {
                        drawingContext.DrawRectangle(SandBrush, new Pen(Brushes.Black, 1), new Rect(x*tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                    if (logic.DoubleSandChecker(x, y))
                    {
                        drawingContext.DrawRectangle(DoubleSandBrush, new Pen(Brushes.Black, 1), new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }

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

    }
}
