namespace WcfChat.ConsoleClient {
    class ValidationManager {
        public bool IsUserNameValid(string userName) {
            return userName != null && userName.Length > 0;
        }

        public bool IsPasswordValid(string password) {
            return password != null && password.Length > 0;
        }

        public bool IsValid(LoginData loginData) {
            return IsUserNameValid(loginData.Password) && IsUserNameValid(loginData.UserName);
        }
    }
}
