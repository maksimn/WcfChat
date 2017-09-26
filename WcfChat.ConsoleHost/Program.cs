using System;
using System.ServiceModel;
using WcfChat.Services;

namespace WcfChat.ConsoleHost {
    class Program {
        static void Main(string[] args) {
            var hostAuthService = new ServiceHost(typeof(AuthService));
            var hostChatService = new ServiceHost(typeof(ChatService));

            hostAuthService.Open();
            hostChatService.Open();

            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            hostAuthService.Close();
            hostChatService.Close();
        }
    }
}
