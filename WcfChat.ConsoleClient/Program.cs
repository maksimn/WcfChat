using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using WcfChat.ConsoleClient.Proxies;
using WcfChat.Contracts.Data;
using WcfChat.Contracts.Service;

namespace WcfChat.ConsoleClient {
    class Program : INewChatMessageCallback  {
        static void Main(string[] args) {
            new Program().Run();
        }

        static void PrintThreadId() {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        }

        private void Run() {
            Console.WriteLine("Соединение с чатом...");
            // PrintThreadId();

            ChatClient chatClient = new ChatClient(new InstanceContext(this));
            IEnumerable<ChatMessage> chatMessages = null;

            try {
                chatMessages = chatClient.ChatMessages();

                foreach (ChatMessage chatMessage in chatMessages) {
                    PrintChatMessageInConsole(chatMessage);
                }
                Console.WriteLine();

                // Ввод сообщений в чат
                while (true) {
                    string chatMessageText = Console.ReadLine();
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    if (chatMessageText.Length == 0) {
                        continue;
                    } else if (string.Compare(chatMessageText, ":q", true) == 0) {
                        break;
                    }

                    chatClient.AddChatMessage(chatMessageText);
                }
            } catch (Exception exc) {
                Console.WriteLine("Произошла ошибка соединения с чатом. Выход из программы.");
                Console.WriteLine(exc.Message);
            } finally {
                try {
                    chatClient.Close();
                } catch (Exception) {
                }
            }
        }

        private static void PrintChatMessageInConsole(ChatMessage chatMessage) {
            string placeholder = "**********************************************************************";
            Console.WriteLine($"{placeholder}{chatMessage.Id}");
            Console.WriteLine($"{chatMessage.UserName}:");
            Console.WriteLine(chatMessage.Text);
            Console.WriteLine();
        }

        public void NewChatMessage(ChatMessage chatMessage) {
            PrintChatMessageInConsole(chatMessage);
            // PrintThreadId(); // Метод вызывается в другом потоке, не в потоке метода Run()
        }
    }
}
