﻿using System.Collections.Generic;
using System.ServiceModel;
using WcfChat.Contracts.Data;
using WcfChat.Contracts.Service;

namespace WcfChat.ConsoleClient.Proxies {
    class ChatClient : DuplexClientBase<IChatService>, IChatService {
        public ChatClient(InstanceContext instanceContext) : base(instanceContext) {
        }

        public void AddChatMessage(string text) {
            Channel.AddChatMessage(text);
        }

        public IEnumerable<ChatMessage> ChatMessages() {
            return Channel.ChatMessages();
        }
    }
}
