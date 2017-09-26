using System;
using System.ServiceModel;
using WcfChat.ConsoleClient.Proxies;
using WcfChat.Contracts.Data;

namespace WcfChat.ConsoleClient {
    class Program {
        static void Main(string[] args) {
            var authProxy = new AuthClient();

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

            authProxy.Close();
        }
    }
}
