using System;
using System.Collections.Generic;
using WcfChat.Services.Repositories.InputModel;
using WcfChat.Services.Repositories.Model;

namespace WcfChat.Services.Repositories {
    class MemoryRepository : IUserRepository, IMessageRepository {
        public IEnumerable<ChatMessage> ChatMessages {
            get {
                throw new NotImplementedException();
            }
        }

        public int UserCount {
            get {
                throw new NotImplementedException();
            }
        }

        public void AddChatMessage(ChatDataInput chatMessage) {
            throw new NotImplementedException();
        }

        public void AddUser(UserRegistrationInput input) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public User GetUserByName(string userName) {
            throw new NotImplementedException();
        }

        public bool LoginUser(string userName, string password) {
            throw new NotImplementedException();
        }
    }
}
