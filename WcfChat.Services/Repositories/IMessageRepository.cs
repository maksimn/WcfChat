using System.Collections.Generic;
using WcfChat.Services.Repositories.InputModel;
using WcfChat.Services.Repositories.Model;

namespace WcfChat.Services.Repositories {
    interface IMessageRepository {
        IEnumerable<ChatMessage> ChatMessages { get; }

        ChatMessage AddChatMessage(ChatDataInput chatMessage);
    }
}
