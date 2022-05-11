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
using Microsoft.Toolkit.Mvvm.Messaging;
using GUI_20212202_MQ7GIA.UI.ViewModel;

namespace GUI_20212202_MQ7GIA.UI.Renderer
{
    public class TerraScopeRenderer : FrameworkElement
    {
        // Models
        GameLogic logic;
        Size size;
        public void SetupLogic(GameLogic logic)
        {
            this.logic = logic;
        }
        public void Resize(Size size)
        {
            this.size = size;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            //for some reason we don't get the value of logic, but i really need it for getting the tile's value. It works everywhere else but not here.
            base.OnRender(drawingContext);
            var tileUnderPeek = logic.TileUnderPeek;

            double tileWidth = size.Width / 5;
            double tileHeight = size.Height / 5;

            ImageBrush brush = null;
            string typeOfCard = logic.TileNames[tileUnderPeek.X, tileUnderPeek.Y];
            int pos = -1;

            switch (typeOfCard)
            {
                case "AirShipClueTile":
                    pos = logic.CardFinder(logic.board.AirShipClueTiles, tileUnderPeek.X, tileUnderPeek.Y);
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
                            break;
                        default:
                            break;
                    }
                    break;
                case "LaunchPadTile":
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Launchpad Tile.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case "CrashStartTile":
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Crash Tile.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case "TunnelTile":
                    pos = logic.CardFinder(logic.board.TunnelTiles, tileUnderPeek.X, tileUnderPeek.Y);
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Tunnel.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case "Mirage":
                    pos = logic.CardFinder(logic.board.OasisMirageTiles, tileUnderPeek.X, tileUnderPeek.Y);
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Mirage.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case "Oasis":
                    pos = logic.CardFinder(logic.board.OasisMirageTiles, tileUnderPeek.X, tileUnderPeek.Y);
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Oasis.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case "EmptyShelter":
                    pos = logic.CardFinder(logic.board.ShelterTiles, tileUnderPeek.X, tileUnderPeek.Y);
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Shelter_1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case "FriendlyWater":
                    pos = logic.CardFinder(logic.board.ShelterTiles, tileUnderPeek.X, tileUnderPeek.Y);
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Shelter_2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case "FriendlyQuest":
                    pos = logic.CardFinder(logic.board.ShelterTiles, tileUnderPeek.X, tileUnderPeek.Y);
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Shelter_3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case "Hostile":
                    pos = logic.CardFinder(logic.board.ShelterTiles, tileUnderPeek.X, tileUnderPeek.Y);
                    brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Tiles", "Shelter_4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                default:
                    break;
            }
            if (brush is not null)
            {
                drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 1), new Rect(tileUnderPeek.X * tileWidth, tileUnderPeek.Y * tileHeight, tileWidth, tileHeight));
            }
        }
    }
}