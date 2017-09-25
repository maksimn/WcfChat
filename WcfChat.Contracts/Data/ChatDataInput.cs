using System.Runtime.Serialization;

namespace WcfChat.Contracts.Data {
    [DataContract]
    public class ChatDataInput {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Text { get; set; }
    }
}
