using System.Collections.Generic;
using System.Linq;
using WcfChat.Contracts.Service;
using WcfChat.Services.Repositories;
using ChatDataInputContract = WcfChat.Contracts.Data.ChatDataInput;
using ChatDataInputModel = WcfChat.Services.Repositories.InputModel.ChatDataInput;
using ChatMessageContract = WcfChat.Contracts.Data.ChatMessage;

namespace WcfChat.Services {
    public class ChatService : IChatService {
        private IMessageRepository repo = new MemoryRepository();

        public void AddChatMessage(ChatDataInputContract chatMessage) {
            var chatMessageInput = new ChatDataInputModel() {
                UserName = chatMessage.UserName,
                Text = chatMessage.Text
            };
            repo.AddChatMessage(chatMessageInput);
        }

        public IEnumerable<ChatMessageContract> ChatMessages() {
            return repo.ChatMessages.Select(chatMessage => 
                new ChatMessageContract() {
                    Id = chatMessage.Id,
                    Text = chatMessage.Text,
                    UserName = chatMessage.UserName
                });
        }
    }
}
