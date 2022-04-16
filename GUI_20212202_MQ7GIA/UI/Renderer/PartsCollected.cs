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
    public class PartsCollected : FrameworkElement
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

            double tileWidth = 80;
            double tileHeight = 80;

            for (int x = 0; x < 5; x++)
            {

                for (int y = 0; y < 5; y++)
                {
                    SolidColorBrush brush = null;
                    switch (logic.PartTiles[x, y])
                    {
                        case "Crystal":
                            {
                                if (logic.PartPickedChecker(x,y))
                                {
                                    brush = Brushes.LightBlue;
                                }
                                else
                                {
                                    brush = Brushes.DarkBlue;
                                }
                                if (brush != null) drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 1), new Rect(0, 0, tileWidth, tileHeight));
                            }
                            break;
                        case "Engine":
                            {
                                if (logic.PartPickedChecker(x, y))
                                {
                                    brush = Brushes.LightGray;
                                }
                                else
                                {
                                    brush = Brushes.Gray;
                                }
                                if (brush != null) drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 1), new Rect(90, 0, tileWidth, tileHeight));
                            }
                            break;
                        case "Compass":
                            {
                                if (logic.PartPickedChecker(x, y))
                                {
                                    brush = Brushes.Red;
                                }
                                else
                                {
                                    brush = Brushes.Maroon;
                                }
                                if (brush != null) drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 1), new Rect(180, 0, tileWidth, tileHeight));
                            }
                            break;
                        case "Propeller":
                            {
                                if (logic.PartPickedChecker(x, y))
                                {
                                    brush = Brushes.Green;
                                }
                                else
                                {
                                    brush = Brushes.DarkGreen;
                                }
                                if (brush != null) drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 1), new Rect(270, 0, tileWidth, tileHeight));
                            }
                            break;
                        default:
                            {
                            }
                            break;
                   
                    }
                    
                }
            }
        }
    }
}
