using GUI_20212202_MQ7GIA.Logic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI_20212202_MQ7GIA.Models;
using System.Xml.Linq;
using System.Windows.Media.Imaging;
using System.Globalization;

namespace GUI_20212202_MQ7GIA.UI.ViewModel
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RelayCommand StartGame { get; set; }
        public RelayCommand OpenOptions { get; set; }
        public RelayCommand ContinueGame { get; set; }
        public Sound Sound { get; set; }
        public int NumberOfActions { get; set; }

        private void StartGameSequence()
        {
            GameLogic logic = new GameLogic(Sound);
            GameSetupWindow gameSetup = new GameSetupWindow(logic, Sound);
            gameSetup.ShowDialog();
        }
        private void OptionsWindow(Sound sound)
        {
            OptionsWindow window = new OptionsWindow(sound);
            window.ShowDialog();
        }

        private void ContinueGameSequence()
        {
            BoardWindow boardWindow = new BoardWindow(ReadXml(), Sound);
            boardWindow.ShowDialog();
        }
        private GameLogic ReadXml()
        {
            XDocument xdocument = XDocument.Load("savegame.xml");
            GameLogic logic = new GameLogic(Sound);

            //
            //board
            logic.board = new Board();

            //launchpad
            logic.board.LaunchPadTile = new LaunchPadTile();
            logic.board.LaunchPadTile.X = int.Parse(xdocument.Root.Element("board").Element("LaunchPadTile").Attribute("X").Value);
            logic.board.LaunchPadTile.Y = int.Parse(xdocument.Root.Element("board").Element("LaunchPadTile").Attribute("Y").Value);
            logic.board.LaunchPadTile.IsDiscovered = xdocument.Root.Element("board").Element("LaunchPadTile").Attribute("IsDiscovered").Value == "true" ? true : false;

            //CrashStartTile
            logic.board.CrashStartTile = new CrashStartTile();
            logic.board.CrashStartTile.X = int.Parse(xdocument.Root.Element("board").Element("CrashStartTile").Attribute("X").Value);
            logic.board.CrashStartTile.Y = int.Parse(xdocument.Root.Element("board").Element("CrashStartTile").Attribute("Y").Value);
            logic.board.CrashStartTile.IsDiscovered = xdocument.Root.Element("board").Element("CrashStartTile").Attribute("IsDiscovered").Value == "true" ? true : false;


            //TunnelTiles
            logic.board.TunnelTiles = new TunnelTile[3];
            int i = 0;
            foreach (var tile in xdocument.Root.Descendants("TunnelTile"))
            {
                logic.board.TunnelTiles[i] = new TunnelTile();
                logic.board.TunnelTiles[i].X = int.Parse(tile.Attribute("X").Value);
                logic.board.TunnelTiles[i].Y = int.Parse(tile.Attribute("Y").Value);
                logic.board.TunnelTiles[i].IsDiscovered = tile.Attribute("IsDiscovered").Value == "true" ? true : false;
                i++;
            }

            //airshipcluetiles
            logic.board.AirShipClueTiles = new AirShipClueTile[8];
            i = 0;
            foreach (var tile in xdocument.Root.Descendants("AirShipClueTile"))
            {
                logic.board.AirShipClueTiles[i] = new AirShipClueTile();
                logic.board.AirShipClueTiles[i].X = int.Parse(tile.Attribute("X").Value);
                logic.board.AirShipClueTiles[i].Y = int.Parse(tile.Attribute("Y").Value);
                logic.board.AirShipClueTiles[i].IsDiscovered = tile.Attribute("IsDiscovered").Value == "true" ? true : false;
                logic.board.AirShipClueTiles[i].Direction = tile.Attribute("Direction").Value == "X" ? 'X' : 'Y';
                logic.board.AirShipClueTiles[i].PartName = tile.Attribute("PartName").Value;
                i++;
            }

            //OasisMirageTiles
            logic.board.OasisMirageTiles = new OasisMirageTile[3];
            i = 0;
            foreach (var tile in xdocument.Root.Descendants("OasisMirageTile"))
            {
                logic.board.OasisMirageTiles[i] = new OasisMirageTile();
                logic.board.OasisMirageTiles[i].X = int.Parse(tile.Attribute("X").Value);
                logic.board.OasisMirageTiles[i].Y = int.Parse(tile.Attribute("Y").Value);
                logic.board.OasisMirageTiles[i].IsDiscovered = tile.Attribute("IsDiscovered").Value == "true" ? true : false;
                logic.board.OasisMirageTiles[i].IsDried = tile.Attribute("IsDried").Value == "true" ? true : false;
                i++;
            }

            //ShelterTiles
            logic.board.ShelterTiles = new ShelterTile[8];
            i = 0;
            foreach (var tile in xdocument.Root.Descendants("ShelterTile"))
            {
                logic.board.ShelterTiles[i] = new ShelterTile();
                logic.board.ShelterTiles[i].X = int.Parse(tile.Attribute("X").Value);
                logic.board.ShelterTiles[i].Y = int.Parse(tile.Attribute("Y").Value);
                logic.board.ShelterTiles[i].IsDiscovered = tile.Attribute("IsDiscovered").Value == "true" ? true : false;
                logic.board.ShelterTiles[i].ShelterType = (ShelterVariations)Enum.Parse(typeof(ShelterVariations), tile.Attribute("ShelterType").Value);
                i++;
            }

            //storm
            logic.board.storm = new Storm();
            logic.board.storm.X = int.Parse(xdocument.Root.Element("board").Element("storm").Attribute("X").Value);
            logic.board.storm.Y = int.Parse(xdocument.Root.Element("board").Element("storm").Attribute("Y").Value);

            //SandTiles
            logic.board.SandTiles = new int[5, 5];
            var allsandtiles = xdocument.Root.Descendants("Column").Attributes("value");
            int add = 0;
            for (i = 0; i < logic.board.SandTiles.GetLength(0); i++)
            {
                for (int j = 0; j < logic.board.SandTiles.GetLength(1); j++)
                {
                    logic.board.SandTiles[i, j] = int.Parse(allsandtiles.ToList()[j + add].Value);
                }
                add += 5;
            }

            //
            //Deck
            logic.Deck = new Deck();


            //AvailableItemCards
            logic.Deck.AvailableItemCards = new List<ItemCard>();
            var tt = xdocument.Root.Descendants("AvailableItemCard");
            i = 0;
            foreach (var card in xdocument.Root.Descendants("AvailableItemCard"))
            {
                logic.Deck.AvailableItemCards.Add(new ItemCard(card.Attribute("Name").Value, card.Attribute("IsDiscarded").Value == "true" ? true : false, card.Attribute("InPlayerHand").Value == "true" ? true : false, card.Attribute("Display").Value));
                i++;
            }

            //AvailableItemCards
            logic.Deck.AvailableStormCards = new List<StormCard>();
            i = 0;
            foreach (var card in xdocument.Root.Descendants("AvailableStormCard"))
            {
                logic.Deck.AvailableStormCards.Add(new StormCard(card.Attribute("Name").Value, card.Attribute("IsDiscarded").Value == "true" ? true : false, int.Parse(card.Attribute("XMove").Value), int.Parse(card.Attribute("YMove").Value)));
                i++;
            }

            //
            //Players
            logic.Players = new List<Player>();
            foreach (var player in xdocument.Root.Descendants("Player"))
            {
                Player ourplayer = new Player();
                ourplayer.X = int.Parse(player.Attribute("X").Value);
                ourplayer.Y = int.Parse(player.Attribute("Y").Value);
                ourplayer.NumberOfActions = int.Parse(player.Attribute("NumberOfActions").Value);
                ourplayer.PlayerName = player.Attribute("PlayerName").Value;
                ourplayer.TurnOrder = int.Parse(player.Attribute("TurnOrder").Value);
                ourplayer.PlayerRoleName = (RoleName)Enum.Parse(typeof(RoleName), player.Attribute("PlayerRoleName").Value);
                ourplayer.WaterLevel = int.Parse(player.Attribute("WaterLevel").Value);
                ourplayer.MaxWaterLevel = int.Parse(player.Attribute("MaxWaterLevel").Value);
                ourplayer.AbilityDescription = player.Attribute("AbilityDescription").Value;

                ourplayer.Cards = new List<ItemCard>();

                foreach (var cards in player.Elements("PlayerCards"))
                {
                    if (int.Parse(cards.Attribute("WhichPlayer").Value) == ourplayer.TurnOrder)
                    {
                        if (cards.Attributes().Count() > 1)
                        {
                            ourplayer.Cards.Add(new ItemCard(cards.Attribute("Name").Value, cards.Attribute("IsDiscarded").Value == "true" ? true : false, cards.Attribute("InPlayerHand").Value == "true" ? true : false, cards.Attribute("Display").Value));
                        }
                    }
                }

                logic.Players.Add(ourplayer);
            }


            //
            //CurrentPlayer 
            logic.CurrentPlayer = int.Parse(xdocument.Root.Element("CurrentPlayer").Attribute("value").Value);

            //
            //shipparts
            logic.shipParts = new ShipParts[4];
            i = 0;
            foreach (var part in xdocument.Root.Descendants("ShipPart"))
            {
                logic.shipParts[i] = new ShipParts();
                logic.shipParts[i].X = int.Parse(part.Attribute("X").Value);
                logic.shipParts[i].Y = int.Parse(part.Attribute("Y").Value);
                logic.shipParts[i].Name = part.Attribute("Name").Value;
                logic.shipParts[i].IsPickedUp = part.Attribute("IsPickedUp").Value == "true" ? true : false;
                i++;
            }

            //
            //sound
            logic.Sound = new Sound();
            logic.Sound.MusicVolume = double.Parse(xdocument.Root.Element("Sound").Attribute("MusicVolume").Value, CultureInfo.InvariantCulture);
            logic.Sound.SoundVolume = double.Parse(xdocument.Root.Element("Sound").Attribute("SoundVolume").Value, CultureInfo.InvariantCulture);

            //
            //tilenames
            logic.TileNames = new string[5, 5];
            var alltilenames = xdocument.Root.Descendants("TileNameColumn").Attributes("Name");
            add = 0;
            for (i = 0; i < logic.TileNames.GetLength(0); i++)
            {
                for (int j = 0; j < logic.TileNames.GetLength(1); j++)
                {
                    logic.TileNames[i, j] = alltilenames.ToList()[j + add].Value;
                }
                add += 5;
            }

            //
            //PartTiles
            logic.PartTiles = new string[5, 5];
            var allparttiles = xdocument.Root.Descendants("PartTileNameColumn").Attributes("Name");
            add = 0;
            for (i = 0; i < logic.PartTiles.GetLength(0); i++)
            {
                for (int j = 0; j < logic.PartTiles.GetLength(1); j++)
                {
                    logic.PartTiles[i, j] = allparttiles.ToList()[j + add].Value == "null" ? null : allparttiles.ToList()[j + add].Value;
                }
                add += 5;
            }

            //
            // DifficultyLevel 
            logic.DifficultyLevel = xdocument.Root.Element("DifficultyLevel").Attribute("value").Value;

            //
            //NumberOfPlayers
            logic.NumberOfPlayers = logic.Players.Count();

            //
            //StormProgress
            logic.StormProgress = double.Parse(xdocument.Root.Element("StormProgress").Attribute("value").Value, CultureInfo.InvariantCulture);

            //
            //StormProgressNumberOfCards 
            logic.StormProgressNumberOfCards = int.Parse(xdocument.Root.Element("StormProgressNumberOfCards").Attribute("value").Value);

            return logic;
        }
        public MainWindowViewModel()
        {
            Sound = new Sound();
            Sound.PlayMusic("Scarface - Bolivia Theme.mp3");
            StartGame = new RelayCommand(StartGameSequence);
            OpenOptions = new RelayCommand(() => OptionsWindow(Sound));
            ContinueGame = new RelayCommand(ContinueGameSequence);  // checking if savegame is null?
        }
    }
}
