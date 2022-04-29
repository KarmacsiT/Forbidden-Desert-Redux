using GUI_20212202_MQ7GIA.Logic;
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
    public class BoardWindowViewModel : ObservableRecipient
    {
        public IDisplay Display { get; set; }
        public int TurnsCounter
        {
            get { return Display.TurnsCounter; }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public BoardWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IDisplay>())
        {
        }
        public BoardWindowViewModel(IDisplay display)
        {
            Display = display;
            Messenger.Register<BoardWindowViewModel, string, string>(this, "DisplayInfo", (recipient, msg) =>
            {
                OnPropertyChanged("TurnsCounter");
            });
        }
    }
}
