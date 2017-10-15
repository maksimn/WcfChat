using System.Collections.Generic;
using WcfChat.Services.Repositories.InputModel;
using WcfChat.Contracts.Data;

namespace WcfChat.Services.Repositories {
    interface IMessageRepository {
        IEnumerable<ChatMessage> ChatMessages { get; }

        ChatMessage AddChatMessage(ChatDataInput chatMessage);
    }
}
