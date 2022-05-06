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
        public BoardWindowViewModel(List<Player> players)
        {
            SetPlayers(players);
        }
        public void SetPlayers(List<Player> players)
        {
            FirstNumActions = players.Where(p => p.TurnOrder == 1).FirstOrDefault().NumberOfActions;
            FirstColor = PlayerColorGiver(players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerRoleName);
            FirstPlayerName = players.Where(p => p.TurnOrder == 1).FirstOrDefault().PlayerName;
            if (players.Count == 3)
            {
                //third player's stuff
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
