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
        List<Player> players;
        public void SetupModel(GameLogic logic, List<Player> players)
        {
            this.logic = logic;
            this.players = players;
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
        }
        public bool ItemPickUp()
        {
            bool invalidate = false;
            string validationMessage = logic.ItemPickUp(players);
            if (validationMessage == "validMove")
            {
                invalidate = true;
            }
            else if (validationMessage == "outOfActions")
            {
                MessageBox.Show("Hint: You can't pick the item up because you are out of actions.");
            }
            else if (validationMessage == "notAnItem")
            {
                MessageBox.Show("Hint: There is no item to pick up from this tile.");
            }
            else if (validationMessage == "alreadyPickedUp")
            {
                MessageBox.Show("Hint: You've already picked up the item.");
            }
            return invalidate;
        }
    }
}
