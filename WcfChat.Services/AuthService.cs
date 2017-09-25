using WcfChat.Contracts.Data;
using WcfChat.Contracts.Service;
using WcfChat.Services.Repositories;
using WcfChat.Services.Repositories.InputModel;

namespace WcfChat.Services {
    public class AuthService : IAuthService {
        private IUserRepository repo = new MemoryRepository();

        public bool AuthenticateUser(string userName, string password) {
            return repo.LoginUser(userName, password);
        }

        public void RegisterUser(UserRegistrationData userData) {
            var userDataInput = new UserRegistrationInput() {
                UserName = userData.UserName, Password = userData.Password
            };
            repo.AddUser(userDataInput);
        }
    }
}
