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
        //ConsoleColor firstcolor;
        //public ConsoleColor FirstColor
        //{
        //    get { return firstcolor; }
        //    set
        //    {
        //        firstcolor = value;
        //        OnPropertyChanged("FirstColor");
        //    }
        //}
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
        int secondNumActions;
        public int SecondNumActions
        {
            get { return secondNumActions; }
            set
            {
                secondNumActions = value;
                OnPropertyChanged("SecondNumActions");
            }
        }
        int thirdNumActions;
        public int ThirdNumActions
        {
            get { return thirdNumActions; }
            set
            {
                thirdNumActions = value;
                OnPropertyChanged("ThirdNumActions");
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
            SecondNumActions = players.Where(p => p.TurnOrder == 2).FirstOrDefault().NumberOfActions;
            if (players.Count == 3)
            {
                ThirdNumActions = players.Where(p => p.TurnOrder == 3).FirstOrDefault().NumberOfActions;
            }
        }
        //private ConsoleColor PlayerColorGiver(RoleName roleName)
        //{
        //    switch (roleName)
        //    {
        //        case RoleName.Archeologist:
        //            return ConsoleColor.Red;
        //        case RoleName.Climber:
        //            return ConsoleColor.Gray;
        //        case RoleName.Explorer:
        //            return ConsoleColor.Green;
        //        case RoleName.Meteorologist:
        //            return ConsoleColor.White;
        //        case RoleName.Navigator:
        //            return ConsoleColor.Yellow;
        //        case RoleName.WaterCarrier:
        //            return ConsoleColor.Blue;
        //        default:
        //            return ConsoleColor.Magenta;      // this should never happen
        //    }
        //}
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
                    return "Blue";
                default:
                    return "Magenta";      // this should never happen
            }
        }
    }
}
