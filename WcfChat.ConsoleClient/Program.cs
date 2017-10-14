using System;
using System.Collections.Generic;
using WcfChat.ConsoleClient.Proxies;
using WcfChat.Contracts.Data;

namespace WcfChat.ConsoleClient {
    class Program {
        static void PrintChatMessageInConsole(ChatMessage chatMessage) {
            string placeholder = "*********************************************************";
            Console.WriteLine($"{placeholder}{chatMessage.Id}");
            Console.WriteLine($"{chatMessage.UserName}:");
            Console.WriteLine(chatMessage.Text);
        }

        static void Main(string[] args) {
            Console.WriteLine("Соединение с чатом...");

            ChatClient chatClient = new ChatClient();
            IEnumerable<ChatMessage> chatMessages = chatClient.ChatMessages();

            foreach(ChatMessage chatMessage in chatMessages) {
                PrintChatMessageInConsole(chatMessage);
            }
            Console.WriteLine();

            chatClient.Close();
        }
    }
}
