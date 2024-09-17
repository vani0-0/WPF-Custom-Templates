using DevExpress.Mvvm;
using WPF_Starter.Store;
using WPF_Starter.Views;

namespace WPF_Starter.ViewModels
{
    internal class MainViewModel : Abstract.ViewModelBase
    {
        private readonly MessageStore _messageStore;

        #region Properties

        public DisplayMessageView DisplayMessageView { get; set; }

        public DisplayMessageViewModel DisplayMessageViewModel { get; }

        #endregion

        #region Commands

        public DelegateCommand<string> UpdateMessageCommand { get; set; }

        public DelegateCommand ShowMessageCommand { get; set; }

        #endregion

        public MainViewModel(MessageStore messageStore, DisplayMessageViewModel displayMessageViewModel)
        {
            UpdateMessageCommand = new DelegateCommand<string>(UpdateMessage);
            _messageStore = messageStore;

            DisplayMessageViewModel = displayMessageViewModel;
        }

        private void UpdateMessage(string message)
        {
            _messageStore.SendMessage(message);
        }

    }
}
