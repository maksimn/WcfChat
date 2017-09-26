﻿using System.Collections.Generic;
using System.ServiceModel;
using WcfChat.Contracts.Data;
using WcfChat.Contracts.Service;

namespace WcfChat.ConsoleClient.Proxies {
    class ChatClient : ClientBase<IChatService>, IChatService {
        public void AddChatMessage(ChatDataInput chatMessage) {
            Channel.AddChatMessage(chatMessage);
        }

        public IEnumerable<ChatMessage> ChatMessages() {
            return Channel.ChatMessages();
        }
    }
}