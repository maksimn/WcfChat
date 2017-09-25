using System;
using WcfChat.Contracts.Data;
using WcfChat.Contracts.Service;

namespace WcfChat.Services {
    public class AuthService : IAuthService {
        public bool AuthenticateUser(string userName, string password) {
            throw new NotImplementedException();
        }

        public void RegisterUser(UserRegistrationData userData) {
            throw new NotImplementedException();
        }
    }
}
