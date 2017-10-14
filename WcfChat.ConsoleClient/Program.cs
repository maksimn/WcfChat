using System;
using System.Collections.Generic;
using WcfChat.ConsoleClient.Proxies;
using WcfChat.Contracts.Data;

namespace WcfChat.ConsoleClient {
    class Program {
        static void PrintChatMessageInConsole(ChatMessage chatMessage) {
            string placeholder = "**********************************************************************";
            Console.WriteLine($"{placeholder}{chatMessage.Id}");
            Console.WriteLine($"{chatMessage.UserName}:");
            Console.WriteLine(chatMessage.Text);
        }

        static void Main(string[] args) {
            Console.WriteLine("Соединение с чатом...");

            ChatClient chatClient = new ChatClient();
            IEnumerable<ChatMessage> chatMessages = null;

            try {
                chatMessages = chatClient.ChatMessages();
            } catch(Exception) {
                Console.WriteLine("Произошла ошибка соединения с чатом. Выход из программы.");
                return;
            }

            foreach(ChatMessage chatMessage in chatMessages) {
                PrintChatMessageInConsole(chatMessage);
            }
            Console.WriteLine();

            // Ввод сообщений в чат
            while (true) {
                string chatMessageText = Console.ReadLine();
                if (chatMessageText.Length == 0) {
                    continue;
                } else if (string.Compare(chatMessageText, ":q", true) == 0) {
                    break;
                }

                chatClient.AddChatMessage(new ChatDataInput() {
                    Text = chatMessageText
                });
            }

            chatClient.Close();
        }
    }
}
