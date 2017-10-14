using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WcfChat.Contracts.Service;
using WcfChat.Services.Repositories;
using WcfChat.Services.Repositories.Model;
using ChatDataInputContract = WcfChat.Contracts.Data.ChatDataInput;
using ChatDataInputModel = WcfChat.Services.Repositories.InputModel.ChatDataInput;
using ChatMessageContract = WcfChat.Contracts.Data.ChatMessage;

namespace WcfChat.Services {
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ChatService : IChatService {
        private IMessageRepository repo = new MemoryRepository();

        public void AddChatMessage(ChatDataInputContract chatMessage) {
            string userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;

            ChatDataInputModel chatMessageInput = new ChatDataInputModel() {
                UserName = userName,
                Text = chatMessage.Text
            };
            ChatMessage newChatMessage = repo.AddChatMessage(chatMessageInput);

            INewChatMessageCallback newChatMessageCallback = 
                OperationContext.Current.GetCallbackChannel<INewChatMessageCallback>();

            if (newChatMessageCallback != null) {
                newChatMessageCallback.NewChatMessage(new ChatMessageContract() {
                    Id = newChatMessage.Id,
                    Text = newChatMessage.Text,
                    UserName = newChatMessage.UserName
                });
            } else {
                throw new FaultException("ERROR: New Chat Message Callback is null.");
            }
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
