using System;
using System.ServiceModel;
using WcfChat.Services;

namespace WcfChat.ConsoleHost {
    class Program {
        static void Main(string[] args) {
            var hostChatService = new ServiceHost(typeof(ChatService));
            
            hostChatService.Open();

            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            hostChatService.Close();
        }
    }
}
