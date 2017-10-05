using System;
using WcfChat.ConsoleClient.Proxies;
using WcfChat.Contracts.Data;

namespace WcfChat.ConsoleClient {
    class Program {
        static AuthClient authProxy = new AuthClient();
        static Serializer serializer = new Serializer();
        static Validator validator = new Validator();

        static void Main(string[] args) {
            // 1. Проверить, есть ли файл ./user.txt , в котором данные пользователя для его 
            // аутентификации.
            // 1.1. Есть и есть данные --> сделать запрос AuthService.AuthenticateUser()
            // Если возвращает true --> перейти в комнату чата
            // В остальных случаях вывести вопрос, хочет ли пользователь зарегистрироваться или
            // войти как существующий пользователь.
            LoginData loginData = serializer.ReadLoginDataFromFile();

            if (validator.IsValid(loginData) && 
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
                    UserRegistrationData userRegistrationData = new UserRegistrationData();

                    bool isValid = true;
                    do {
                        string username = ConsoleReadUserName();
                        string password = ConsoleReadPassword();
                        Console.Write("Repeat ");
                        string confirmPassword = ConsoleReadPassword();

                        if (confirmPassword != password) {
                            Console.WriteLine("You have to enter the same password twice. Reenter your data.");
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    } while(!isValid);
                    
                    authProxy.RegisterUser(userRegistrationData);

                    Console.WriteLine("You successfully registered the new user.");
                }
            }

            authProxy.Close();
        }

        static string ConsoleReadUserName() {
            string username = string.Empty;

            do {
                Console.Write("Your user name: ");
                username = Console.ReadLine();
            } while (!validator.IsUserNameValid(username));

            return username;
        }

        static string ConsoleReadPassword() {
            string password = string.Empty;

            do {
                Console.Write("Your password: ");
                password = ConsoleHiddenInput();
            } while (!validator.IsPasswordValid(password));

            return password;
        }

        public static string ConsoleHiddenInput() {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter) {
                if (info.Key != ConsoleKey.Backspace) {
                    Console.Write("*");
                    password += info.KeyChar;
                } else if (info.Key == ConsoleKey.Backspace) {
                    if (!string.IsNullOrEmpty(password)) {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
    }
}
