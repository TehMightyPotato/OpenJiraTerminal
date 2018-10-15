using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenJiraTerminal
{
    class Program
    {
        private static ConnectionHandler connectionHandler;
        private static UserManager userManager;
        private static JsonConverter jsonCOnverter;
        private static LoginManager loginManager;

        private static bool isConnected;


        static void Main(string[] args)
        {
            Console.Title = typeof(Program).Name;
            Init();
            var logpw = GetLoginData();
            loginManager = new LoginManager(logpw[0], logpw[1]);
            Run();
        }

        #region init
        private static void Init()
        {
            isConnected = false;
        }
        #endregion

        #region command handling and execution
        private static void Run()
        {
            while (true)
            {
                var consoleInput = ReadFromConsole();
                if (string.IsNullOrWhiteSpace(consoleInput)) continue;
                try
                {
                    string result = Execute(consoleInput);
                    if(result == null)
                    {
                        Console.Out.WriteLine("Command not recognized. need 'help' ?");
                        continue;
                    }
                    
                    Console.Out.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
        }


        public static string ReadFromConsole(string promptMessage = "console",bool maskInput = false)
        {
            if (!maskInput)
            {
                Console.Write(promptMessage + "> ");
                return Console.ReadLine();
            }
            else
            {
                Console.Write(promptMessage + "> ");
                List<string> result = new List<string>();
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if(key.Key == ConsoleKey.Enter) break;
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        result.RemoveAt(result.Count - 1);
                        continue;
                    }
                    result.Add(key.KeyChar.ToString());
                }
                Console.WriteLine("");
                return String.Join(String.Empty, result);
                
            }
        }

        private static string Execute(string command)
        { 
            switch (command)
            {
                case "connect":
                case "c":
                    return ConnectToServer();
                case "addUser":
                    break;
                case "quit":
                case "q":
                    QuitApplication();
                    break;
                case "help":
                    PrintHelp();
                    break;

            }
            return null;
        }
        #endregion


        #region establish connection
        private static string ConnectToServer()
        {
            var url = ReadFromConsole("jira-url");
            connectionHandler = new ConnectionHandler(url, loginManager.GetLoginCredentials());
            if (connectionHandler.TestConnection())
            {
                return "Connection established!";
            }
            else
            {
                return "Connection failed!";
            }
        }
        #endregion

        #region query methods
        private static void AddUser(string[] args)
        {
            foreach( var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
        #endregion

        #region login stuff
        private static string[] GetLoginData()
        {
            string[] result = new string[2];
            result[0] = ReadFromConsole("login");
            result[1] = ReadFromConsole("password", true);
            return result;
        }
        #endregion

        private static void QuitApplication()
        {
            Environment.Exit(0);
        }

        static void PrintHelp()
        {
            Console.WriteLine("Sample Help text");
        }
    }
}
