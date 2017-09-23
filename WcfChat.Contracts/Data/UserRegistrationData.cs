using System.Runtime.Serialization;

namespace WcfChat.Contracts.Data {
    [DataContract]
    public class UserRegistrationData {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
