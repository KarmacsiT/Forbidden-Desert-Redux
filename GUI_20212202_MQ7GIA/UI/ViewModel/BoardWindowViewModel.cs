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
            SecondNumActions = players.Where(p => p.TurnOrder == 2).FirstOrDefault().NumberOfActions;
            if (players.Count == 3)
            {
                ThirdNumActions = players.Where(p => p.TurnOrder == 3).FirstOrDefault().NumberOfActions;
            }
        }
    }
}
