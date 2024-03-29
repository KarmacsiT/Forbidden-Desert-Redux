﻿using GUI_20212202_MQ7GIA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_20212202_MQ7GIA
{
    /// <summary>
    /// Interaction logic for WinningWindow.xaml
    /// </summary>
    public partial class WinningWindow : Window
    {
        public Sound Sound { get; set; }
        public WinningWindow(Sound sound)
        {
            InitializeComponent();
            Sound = sound;
            Sound.PlaySound("GameWonSound.mp3");
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Sound.PlayMusic("Scarface - Bolivia Theme.mp3");
        }
    }
}
