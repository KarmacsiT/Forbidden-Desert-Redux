using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using GUI_20212202_MQ7GIA.UI.Renderer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GUI_20212202_MQ7GIA.UI.ViewModel
{
    public class BoardWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        string firstcolor;
        public string FirstColor
        {
            get { return firstcolor; }
            set
            {
                firstcolor = value;
                OnPropertyChanged("FirstColor");
            }
        }
        string secondcolor;
        public string SecondColor
        {
            get { return secondcolor; }
            set
            {
                secondcolor = value;
                OnPropertyChanged("SecondColor");
            }
        }

        string thirdcolor;
        public string ThirdColor
        {
            get { return thirdcolor; }
            set
            {
                thirdcolor = value;
                OnPropertyChanged("ThirdColor");
            }
        }
        int firstNumActions;
        public int FirstNumActions
        {
            get { return firstNumActions; }
            set
            {
                firstNumActions = value;
                OnPropertyChanged("FirstNumActions");
            }
        }
        string firstPlayerName;
        public string FirstPlayerName
        {
            get { return firstPlayerName; }
            set
            {
                firstPlayerName = value;
                OnPropertyChanged("FirstPlayerName");
            }
        }
        string secondPlayerName;
        public string SecondPlayerName
        {
            get { return secondPlayerName; }
            set
            {
                secondPlayerName = value;
                OnPropertyChanged("SecondPlayerName");
            }
        }
        string thirdPlayerName;
        public string ThirdPlayerName
        {
            get { return thirdPlayerName; }
            set
            {
                thirdPlayerName = value;
                OnPropertyChanged("ThirdPlayerName");
            }
        }
        public BoardWindowViewModel(List<Player> players)
        {
            SetPlayers(players);
        }
        public BoardWindowViewModel()
        {

        }
        public void SetPlayers(List<Player> players)
        {
            FirstNumActions = players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions;
            FirstColor = PlayerColorGiver(players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerRoleName);
            SecondColor = PlayerColorGiver(players.Where(p => p.TurnOrder == 2).FirstOrDefault().PlayerRoleName);
            FirstPlayerName = players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerName;
            SecondPlayerName = players.Where(p => p.TurnOrder == 2).FirstOrDefault().PlayerName;
            if (players.Count == 3)
            {
                ThirdColor = PlayerColorGiver(players.Where(p => p.TurnOrder == 3).FirstOrDefault().PlayerRoleName);
                ThirdPlayerName = players.Where(p => p.TurnOrder == 3).FirstOrDefault().PlayerName;
            }
        }
        private string PlayerColorGiver(RoleName roleName)
        {
            switch (roleName)
            {
                case RoleName.Archeologist:
                    return "Red";
                case RoleName.Climber:
                    return "Gray";
                case RoleName.Explorer:
                    return "Green";
                case RoleName.Meteorologist:
                    return "White";
                case RoleName.Navigator:
                    return "Yellow";
                case RoleName.WaterCarrier:
                    return "LightBlue";
                default:
                    return "Magenta";      // this should never happen
            }
        }
    }
}
