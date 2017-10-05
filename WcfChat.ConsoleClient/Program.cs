using System;
using WcfChat.ConsoleClient.Proxies;
using WcfChat.Contracts.Data;

namespace WcfChat.ConsoleClient {
    class Program {
        static void Main(string[] args) {
            // 1. Проверить, есть ли файл ./user.txt , в котором данные пользователя для его 
            // аутентификации.
            // 1.1. Есть и есть данные --> сделать запрос AuthService.AuthenticateUser()
            // Если возвращает true --> перейти в комнату чата
            // В остальных случаях вывести вопрос, хочет ли пользователь зарегистрироваться или
            // войти как существующий пользователь.
            AuthClient authProxy = new AuthClient();
            SerializationManager serializationManager = new SerializationManager();
            ValidationManager validationManager = new ValidationManager();

            LoginData loginData = serializationManager.ReadLoginDataFromFile();

            if (validationManager.IsValid(loginData) && 
                authProxy.AuthenticateUser(loginData.UserName, loginData.Password)) {
                Console.WriteLine("You are in the chat room.");
            } else {
                Console.WriteLine("You need to register or sign in to enter chat room. \n" +
                "Enter R to register a new user. Enter S to sign in as an registered user.");

                string enterType = null;

                while ((enterType = Console.ReadLine()) != null) {
                    if (enterType == "R" || enterType == "S") {
                        break;
                    }
                }

                if (enterType == "R") {
                    Console.Write("Your user name: ");
                    var username = Console.ReadLine();
                    Console.Write("Your password: ");
                    var password = Console.ReadLine();

                    authProxy.RegisterUser(new UserRegistrationData() {
                        UserName = username, Password = password
                    });

                    Console.WriteLine("You successfully registered the new user.");
                }
            }

            authProxy.Close();
        }
    }
}
