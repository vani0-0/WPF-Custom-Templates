﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Starter_v1.Abstract
{
    public abstract class RaisePropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
