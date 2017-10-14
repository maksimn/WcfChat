using System.Collections.Generic;
using WcfChat.Services.Repositories.InputModel;
using WcfChat.Services.Repositories.Model;

namespace WcfChat.Services.Repositories {
    class MemoryRepository : IMessageRepository {
        private static List<ChatMessage> _messages = new List<ChatMessage>();

        static MemoryRepository() {
            _messages.Add(new ChatMessage() {
                Id = _messages.Count,
                Text = "Добро пожаловать в чат",
                UserName = "Админ"
            });
        }

        public IEnumerable<ChatMessage> ChatMessages {
            get {
                return _messages;
            }
        }

        public ChatMessage AddChatMessage(ChatDataInput chatMessage) {
            ChatMessage message = new ChatMessage() {
                Id = _messages.Count, Text = chatMessage.Text, UserName = chatMessage.UserName
            };
            _messages.Add(message);
            return message;
        }
    }
}
