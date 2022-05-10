using GUI_20212202_MQ7GIA.Logic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            logic.board = new Models.Board();

            //launchpad
            logic.board.LaunchPadTile = new Models.LaunchPadTile();
            logic.board.LaunchPadTile.X = int.Parse(xdocument.Root.Element("board").Element("LaunchPadTile").Attribute("X").Value);
            logic.board.LaunchPadTile.Y = int.Parse(xdocument.Root.Element("board").Element("LaunchPadTile").Attribute("Y").Value);
            logic.board.LaunchPadTile.IsDiscovered = xdocument.Root.Element("board").Element("LaunchPadTile").Attribute("IsDiscovered").Value == "true" ? true : false;

            //CrashStartTile
            logic.board.CrashStartTile = new Models.CrashStartTile();
            logic.board.CrashStartTile.X = int.Parse(xdocument.Root.Element("board").Element("CrashStartTile").Attribute("X").Value);
            logic.board.CrashStartTile.Y = int.Parse(xdocument.Root.Element("board").Element("CrashStartTile").Attribute("Y").Value);
            logic.board.CrashStartTile.IsDiscovered = xdocument.Root.Element("board").Element("CrashStartTile").Attribute("IsDiscovered").Value == "true" ? true : false;

            
            //TunnelTiles
            logic.board.TunnelTiles = new Models.TunnelTile[3];
            int i = 0;
            foreach (var tile in xdocument.Root.Descendants("TunnelTile"))
            {
                logic.board.TunnelTiles[i] = new Models.TunnelTile();
                logic.board.TunnelTiles[i].X = int.Parse(tile.Attribute("X").Value);
                logic.board.TunnelTiles[i].Y = int.Parse(tile.Attribute("Y").Value);
                logic.board.TunnelTiles[i].IsDiscovered = tile.Attribute("X").Value == "true" ? true : false;
                i++;
            }

            //airshipcluetiles
            logic.board.AirShipClueTiles = new Models.AirShipClueTile[8];
            i = 0;
            foreach (var tile in xdocument.Root.Descendants("AirShipClueTile"))
            {
                logic.board.AirShipClueTiles[i] = new Models.AirShipClueTile();
                logic.board.AirShipClueTiles[i].X = int.Parse(tile.Attribute("X").Value);
                logic.board.AirShipClueTiles[i].Y = int.Parse(tile.Attribute("Y").Value);
                logic.board.AirShipClueTiles[i].IsDiscovered = tile.Attribute("X").Value == "true" ? true : false;
                logic.board.AirShipClueTiles[i].Direction = tile.Attribute("Direction").Value == "X" ? 'X' : 'Y';
                logic.board.AirShipClueTiles[i].PartName = tile.Attribute("PartName").Value;
                i++;
            }

            //OasisMirageTiles
            logic.board.OasisMirageTiles = new Models.OasisMirageTile[3];
            i = 0;
            foreach (var tile in xdocument.Root.Descendants("OasisMirageTile"))
            {
                logic.board.OasisMirageTiles[i] = new Models.OasisMirageTile();
                logic.board.OasisMirageTiles[i].X = int.Parse(tile.Attribute("X").Value);
                logic.board.OasisMirageTiles[i].Y = int.Parse(tile.Attribute("Y").Value);
                logic.board.OasisMirageTiles[i].IsDiscovered = tile.Attribute("X").Value == "true" ? true : false;
                logic.board.OasisMirageTiles[i].IsDried = tile.Attribute("X").Value == "true" ? true : false;
                i++;
            }










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
