using WPF_Starter.Models;
using WPF_Starter.Store;

namespace WPF_Starter.ViewModels
{
    internal class DisplayMessageViewModel : Abstract.ViewModelBase
    {
        private readonly MessageStore _messageStore;

        #region Properties

        public Message Message => _messageStore.Message;
        
        #endregion
        
        public DisplayMessageViewModel(MessageStore messageStore)
        {
            _messageStore = messageStore;
            _messageStore.MessageSent += MessageStore_MessageSent;
        }

        private void MessageStore_MessageSent()
        {
            RaisePropertyChanged();
        }

        internal static DisplayMessageViewModel LoadViewModel(MessageStore messageStore)
        {
            DisplayMessageViewModel viewModel = new DisplayMessageViewModel(messageStore);
            return viewModel;
        }
    }
}
