using System.ServiceModel;
using WcfChat.Contracts.Data;
using WcfChat.Contracts.Service;

namespace WcfChat.ConsoleClient.Proxies {
    class AuthClient : ClientBase<IAuthService>, IAuthService {
        public bool AuthenticateUser(string userName, string password) {
            return Channel.AuthenticateUser(userName, password);
        }

        public void RegisterUser(UserRegistrationData userData) {
            Channel.RegisterUser(userData);
        }
    }
}
