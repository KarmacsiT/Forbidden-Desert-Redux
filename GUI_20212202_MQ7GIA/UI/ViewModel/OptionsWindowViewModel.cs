using GUI_20212202_MQ7GIA.Logic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212202_MQ7GIA.UI.ViewModel
{
    public class OptionsWindowViewModel : ObservableRecipient
    {
        public RelayCommand ChangeMusicVolume { get; set; }
        public RelayCommand ChangeSoundVolume { get; set; }
        public Sound Sound { get; set; }

        public bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public void SetupSound(Sound sound)
        {
            Sound = sound;
        }
        public OptionsWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                //We need this because otherwise the Designer Mode would give us errors
            }
        }
    }
}
