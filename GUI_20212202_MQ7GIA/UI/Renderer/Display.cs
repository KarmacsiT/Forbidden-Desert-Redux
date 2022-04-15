using GUI_20212202_MQ7GIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GUI_20212202_MQ7GIA.Logic;

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

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            double tileWidth = size.Width / 5;
            double tileHeight = size.Height / 5;

            for (int x = 0; x < 5; x++)
            {

                for (int y = 0; y < 5; y++)
                {
                    SolidColorBrush solidColorBrush = new SolidColorBrush();

                    switch (logic.TileNames[x,y])
                    {
                        case "Storm":
                            {
                                drawingContext.DrawRectangle(Brushes.Coral, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        case "AirShipClueTile":
                            {
                                drawingContext.DrawRectangle(Brushes.Green, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        case "LaunchPadTile":
                            {
                                drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        case "TunnelTile":
                            {
                                drawingContext.DrawRectangle(Brushes.Brown, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        case "Mirage":
                            {
                                drawingContext.DrawRectangle(Brushes.Gray, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        case "Oasis":
                            {
                                drawingContext.DrawRectangle(Brushes.Blue, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        case "EmptyShelter":
                            {
                                drawingContext.DrawRectangle(Brushes.Red, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        case "FriendlyWater":
                            {
                                drawingContext.DrawRectangle(Brushes.Purple, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        case "FriendlyQuest":
                            {
                                drawingContext.DrawRectangle(Brushes.Pink, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
                        default:
                            {
                                drawingContext.DrawRectangle(Brushes.Yellow, new Pen(Brushes.Black, 1), new Rect(y * tileWidth, x * tileHeight, tileWidth, tileHeight));
                            }
                            break;
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
