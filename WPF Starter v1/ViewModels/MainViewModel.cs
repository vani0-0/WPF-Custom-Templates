using DevExpress.Mvvm;
using System;

namespace WPF_Starter_v1.ViewModels
{
    internal class MainViewModel : Abstract.ViewModelBase
    {
        #region Properties

        private string _greet;

        public string Greet
        {
            get { return _greet; }
            set
            {
                _greet = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public DelegateCommand GreetCommand { get; private set; }

        #endregion

        public MainViewModel()
        {
            GreetCommand = new DelegateCommand(GreetDelegate);
        }

        private void GreetDelegate()
        {
            Greet = "Hello, World!";
        }
    }
}
