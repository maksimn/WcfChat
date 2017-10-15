using System.Collections.Generic;
using System.ServiceModel;
using WcfChat.Contracts.Service;
using WcfChat.Services.Repositories;
using WcfChat.Services.Repositories.InputModel;
using WcfChat.Contracts.Data;

namespace WcfChat.Services {
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ChatService : IChatService {
        private IMessageRepository repo = new MemoryRepository();
        private static Dictionary<string, INewChatMessageCallback> clientCallback = 
            new Dictionary<string, INewChatMessageCallback>();

        public void AddChatMessage(string text) {
            string userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;

            ChatMessage newChatMessage = repo.AddChatMessage(
                new ChatDataInput() { UserName = userName, Text = text }
            );

            INewChatMessageCallback newChatMessageCallback = 
                OperationContext.Current.GetCallbackChannel<INewChatMessageCallback>();

            clientCallback[userName] = newChatMessageCallback;

            foreach (var callback in clientCallback) {
                if (callback.Value != null) {
                    callback.Value.NewChatMessage(new ChatMessage() {
                        Id = newChatMessage.Id,
                        Text = newChatMessage.Text,
                        UserName = newChatMessage.UserName
                    });
                }
            }
        }

        public IEnumerable<ChatMessage> ChatMessages() {
            return repo.ChatMessages;
        }
    }
}
