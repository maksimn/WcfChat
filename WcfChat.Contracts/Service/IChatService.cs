﻿using System.Collections.Generic;
using System.ServiceModel;
using WcfChat.Contracts.Data;

namespace WcfChat.Contracts.Service {
    [ServiceContract(CallbackContract = typeof(INewChatMessageCallback))]
    public interface IChatService {
        [OperationContract]
        IEnumerable<ChatMessage> ChatMessages();

        [OperationContract]
        void AddChatMessage(string text);
    }
}
