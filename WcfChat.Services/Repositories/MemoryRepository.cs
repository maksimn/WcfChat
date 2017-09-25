using System.Collections.Generic;
using System.Data;
using WcfChat.Services.Repositories.InputModel;
using WcfChat.Services.Repositories.Model;

namespace WcfChat.Services.Repositories {
    class MemoryRepository : IUserRepository, IMessageRepository {
        private static List<User> _users = new List<User>();
        private static List<ChatMessage> _messages = new List<ChatMessage>();

        public IEnumerable<ChatMessage> ChatMessages {
            get {
                return _messages;
            }
        }

        public int UserCount {
            get {
                return _users.Count;
            }
        }

        public void AddUser(UserRegistrationInput input) {
            var userName = input.UserName;
            var password = input.Password;

            if (_users.Find(u => u.Name == userName) != null) {
                throw new DuplicateNameException();
            }
            int newId = _users.Count;
            var newUser = new User() { Id = newId, Name = userName, PasswordHash = password };

            _users.Add(newUser);
        }

        public void Clear() {
            _users.Clear();
        }

        public User GetUserByName(string userName) {
            return _users.Find(u => u.Name == userName);
        }

        public bool LoginUser(string userName, string password) {
            var user = GetUserByName(userName);

            return user != null && user.PasswordHash == password;
        }

        public void AddChatMessage(ChatDataInput chatMessage) {
            var message = new ChatMessage() {
                Id = _messages.Count, Text = chatMessage.Text, UserName = chatMessage.UserName
            };
            _messages.Add(message);
        }
    }
}
