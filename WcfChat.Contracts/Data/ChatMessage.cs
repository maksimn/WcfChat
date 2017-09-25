using System.Runtime.Serialization;

namespace WcfChat.Contracts.Data {
    [DataContract]
    public class ChatMessage {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Text { get; set; }
    }
}
