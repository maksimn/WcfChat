using WcfChat.Services.Repositories.InputModel;
using WcfChat.Services.Repositories.Model;

namespace WcfChat.Services.Repositories {
    interface IUserRepository {
        void AddUser(UserRegistrationInput input);
        User GetUserByName(string userName);
        bool LoginUser(string userName, string password);
        int UserCount { get; }
        void Clear();
    }
}
