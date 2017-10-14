using System.ServiceModel;
using WcfChat.Contracts.Data;

namespace WcfChat.Contracts.Service {
    [ServiceContract]
    public interface INewChatMessageCallback {
        [OperationContract(IsOneWay = false)]
        void NewChatMessage(ChatMessage chatMessage);
    }
}
