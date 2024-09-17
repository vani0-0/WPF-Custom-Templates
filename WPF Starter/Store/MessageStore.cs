using System;
using WPF_Starter.Models;

namespace WPF_Starter.Store
{
    public class MessageStore
    {
        private Message _message;

        public Message Message
        {
            get => _message;
            set
            {
                _message = value;
                MessageSent?.Invoke();
            }
        }

        public event Action MessageSent;

        public MessageStore()
        {
            _message = new Message();
        }

        public void SendMessage(string message)
        {
            Message = new Message { Content = message };
        }
    }
}
