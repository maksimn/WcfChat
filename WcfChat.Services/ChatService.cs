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
        private static Dictionary<string, INewChatMessageCallback> clientCallbacks = 
            new Dictionary<string, INewChatMessageCallback>();

        public ChatService() {
            SetCallbackChannelForThisUser();
        }

        public void AddChatMessage(string text) {
            string userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;

            ChatMessage newChatMessage = repo.AddChatMessage(
                new ChatDataInput() { UserName = userName, Text = text }
            );

            BroadcastChatMessage(newChatMessage);
        }

        public IEnumerable<ChatMessage> ChatMessages() {
            return repo.ChatMessages;
        }

        private static void BroadcastChatMessage(ChatMessage chatMessage) {
            foreach (var callback in clientCallbacks) {
                if (callback.Value != null) {
                    callback.Value.NewChatMessage(chatMessage);
                }
            }
        }

        private static void SetCallbackChannelForThisUser() {
            string userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;
            INewChatMessageCallback newChatMessageCallback =
                OperationContext.Current.GetCallbackChannel<INewChatMessageCallback>();

            clientCallbacks[userName] = newChatMessageCallback;
        }
    }
}
