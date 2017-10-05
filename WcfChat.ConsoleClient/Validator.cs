namespace WcfChat.ConsoleClient {
    class Validator {
        public bool IsUserNameValid(string userName) {
            return userName != null && userName.Length > 0;
        }

        public bool IsPasswordValid(string password) {
            return password != null && password.Length > 0;
        }

        public bool IsValid(LoginData loginData) {
            return loginData != null && IsUserNameValid(loginData.UserName) && 
                IsPasswordValid(loginData.Password);
        }
    }
}
