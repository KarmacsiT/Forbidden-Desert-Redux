using GUI_20212202_MQ7GIA.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_20212202_MQ7GIA.UI.Renderer
{
    class CurrentPlayerCards : FrameworkElement
    {
        // Models
        /*GameLogic logic;
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

            double tileWidth = 50;
            double tileHeight = 50;

            for (int x = 0; x < 5; x++)
            {

                for (int y = 0; y < 5; y++)
                {
                    ImageBrush imageAsset = null;
                    switch (logic.PartTiles[x, y])
                    {
                        case "Crystal":
                            {
                                if (logic.PartPickedChecker(x, y))
                                {
                                    imageAsset = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Parts", "Crystal_Collected.png"), UriKind.RelativeOrAbsolute)));
                                }
                                else
                                {
                                    imageAsset = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Parts", "Crystal_unacquired.png"), UriKind.RelativeOrAbsolute)));
                                }
                                if (imageAsset != null) drawingContext.DrawRectangle(imageAsset, new Pen(Brushes.Black, 1), new Rect(0, 0, tileWidth, tileHeight));
                            }
                            break;

                        case "Engine":
                            {
                                if (logic.PartPickedChecker(x, y))
                                {
                                    imageAsset = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Parts", "Engine_Collected.png"), UriKind.RelativeOrAbsolute)));
                                }
                                else
                                {
                                    imageAsset = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Parts", "Engine_unacquired.png"), UriKind.RelativeOrAbsolute)));
                                }
                                if (imageAsset != null) drawingContext.DrawRectangle(imageAsset, new Pen(Brushes.Black, 1), new Rect(60, 0, tileWidth, tileHeight));
                            }
                            break;

                        case "Compass":
                            {
                                if (logic.PartPickedChecker(x, y))
                                {
                                    imageAsset = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Parts", "Compass_Collected.png"), UriKind.RelativeOrAbsolute)));
                                }
                                else
                                {
                                    imageAsset = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Parts", "Compass_unacquired.png"), UriKind.RelativeOrAbsolute)));
                                }
                                if (imageAsset != null) drawingContext.DrawRectangle(imageAsset, new Pen(Brushes.Black, 1), new Rect(120, 0, tileWidth, tileHeight));
                            }
                            break;

                        case "Propeller":
                            {
                                if (logic.PartPickedChecker(x, y))
                                {
                                    imageAsset = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Parts", "Propeller_Collected.png"), UriKind.RelativeOrAbsolute)));
                                }
                                else
                                {
                                    imageAsset = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Parts", "Propeller_Unaquired.png"), UriKind.RelativeOrAbsolute)));
                                }
                                if (imageAsset != null) drawingContext.DrawRectangle(imageAsset, new Pen(Brushes.Black, 1), new Rect(180, 0, tileWidth, tileHeight));
                            }
                            break;
                        default:
                            {
                            }
                            break;

                    }

                }
            }
        }*/
    }
}
