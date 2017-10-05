using System.IO;

namespace WcfChat.ConsoleClient {
    class Serializer {
        private const string _ticketFileName = @".\user.txt";

        public void WriteLoginDataToFile(LoginData data) {
            File.Create(_ticketFileName);
            using (var file = new StreamWriter(_ticketFileName)) {
                file.WriteLine($"username: {data.UserName}");
                file.WriteLine($"password: {data.Password}");
            }
        }

        public LoginData ReadLoginDataFromFile() {
            var loginData = new LoginData();

            if (!File.Exists(_ticketFileName)) {
                return null;
            }

            using (var file = new StreamReader(_ticketFileName)) {
                var line1 = file.ReadLine();
                var line2 = file.ReadLine();

                loginData.UserName = line1.StartsWith("username: ") ?
                                     line1.Substring(line1.IndexOf(' ')) : null;
                loginData.Password = line2.StartsWith("password: ") ?
                                     line2.Substring(line2.IndexOf(' ')) : null;
            }

            return loginData;
        }
    }
}
