using System.Data;
using System.ServiceModel;
using WcfChat.Contracts.Data;

namespace WcfChat.Contracts.Service {
    [ServiceContract]
    public interface IAuthService {
        [OperationContract]
        [FaultContract(typeof(DuplicateNameException))]
        void RegisterUser(UserRegistrationData userData);

        [OperationContract]
        bool AuthenticateUser(string userName, string password);
    }
}
