using System;
using System.Collections.Generic;
using WcfChat.Contracts.Data;
using WcfChat.Contracts.Service;

namespace WcfChat.Services {
    public class ChatService : IChatService {
        public void AddChatMessage(ChatDataInput chatMessage) {
            throw new NotImplementedException();
        }

        public IEnumerable<ChatMessage> ChatMessages() {
            throw new NotImplementedException();
        }
    }
}
