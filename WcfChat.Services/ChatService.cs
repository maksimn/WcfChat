using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WcfChat.Contracts.Service;
using WcfChat.Services.Repositories;
using WcfChat.Services.Repositories.Model;
using ChatDataInputModel = WcfChat.Services.Repositories.InputModel.ChatDataInput;
using ChatMessageContract = WcfChat.Contracts.Data.ChatMessage;

namespace WcfChat.Services {
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ChatService : IChatService {
        private IMessageRepository repo = new MemoryRepository();
        private static Dictionary<string, INewChatMessageCallback> clientCallback = 
            new Dictionary<string, INewChatMessageCallback>();

        public void AddChatMessage(string text) {
            string userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;

            ChatDataInputModel chatMessageInput = new ChatDataInputModel() {
                UserName = userName,
                Text = text
            };
            ChatMessage newChatMessage = repo.AddChatMessage(chatMessageInput);

            INewChatMessageCallback newChatMessageCallback = 
                OperationContext.Current.GetCallbackChannel<INewChatMessageCallback>();

            clientCallback[userName] = newChatMessageCallback;

            foreach (var callback in clientCallback) {
                if (callback.Value != null) {
                    callback.Value.NewChatMessage(new ChatMessageContract() {
                        Id = newChatMessage.Id,
                        Text = newChatMessage.Text,
                        UserName = newChatMessage.UserName
                    });
                }
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
