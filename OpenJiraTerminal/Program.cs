using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JsonObjects;

namespace OpenJiraTerminal
{
    class Program
    {
        private static ConnectionHandler connectionHandler;
        private static UserManager userManager;
        private static JsonConverter jsonConverter;
        private static LoginManager loginManager;
        private static CSVReader csvReader;

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
            jsonConverter = new JsonConverter();
            csvReader = new CSVReader();
        }
        #endregion

        #region command handling and execution
        private static void Run()
        {
            while (true)
            {
                var consoleInput = ReadFromConsole();
                if (string.IsNullOrWhiteSpace(consoleInput[0])) continue;
                try
                {
                    string result = Execute(consoleInput);
                    if(string.IsNullOrWhiteSpace(result))
                    {
                        Console.Out.WriteLine("Command not recognized. need 'help' ?");
                        continue;
                    }
                    
                    Console.Out.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message + "lelol");
                }
            }
        }


        public static string[] ReadFromConsole(string promptMessage = "console",bool maskInput = false)
        {
            if (!maskInput)
            {
                Console.Write(promptMessage + "> ");
                return Console.ReadLine().Split(" ");
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
                return String.Join(String.Empty, result).Split(" ");
                
            }
        }

        private static string Execute(string[] command)
        { 
            switch (command[0])
            {
                case "connect":
                case "c":
                    return ConnectToServer();
                case "addUser":
                    return AddUser(command);
                case "addUsersFromFile":
                    return AddUsersFromFile(command);
                case "quit":
                case "q":
                    QuitApplication();
                    break;
                case "help":
                    return PrintHelp();
            }
            return null;
        }
        #endregion

        #region connection and query building
        private static string ConnectToServer()
        {
            var url = ReadFromConsole("jira-url");
            connectionHandler = new ConnectionHandler(url[0], loginManager.GetLoginCredentials());
            if (connectionHandler.TestConnection())
            {
                return "Connection established!";
            }
            else
            {
                return "Connection failed!";
            }
        }

        private static string BuildQueryString(Api api, string data = "")
        {
            string apiResult = "";
            switch (api)
            {
                case Api.addUser:
                    apiResult = "/rest/api/2/user";
                    break;
            }
            return connectionHandler.url + apiResult;
        }
        #endregion

        #region query methods
        private static string AddUser(string[] command)
        {
            CreateUserJson json = new CreateUserJson(command[1], command[2], command[3], command[4]);
            var response = connectionHandler.QueryServer(RequestType.POST, BuildQueryString(Api.addUser) ,jsonConverter.BuildJsonFromObject(json));
            var jsonObject = jsonConverter.BuildObjectFromJson(JsonObjectType.AddUserResponse, response);
            if(jsonObject is CreateUserResponseJson)
            {
                return "Successfully added user: " + command[4];
            }
            return "Failed to create user.";
        }

        private static string AddUsersFromFile(string[] command)
        {
            List<string[]> table = csvReader.ReadAddUserCsv(command[1]);

            List<string> success = new List<string>();
            List<string> failure = new List<string>();

            foreach(string[] element in table)
            {
                string[] arguments = { "addUser", element[0], element[1], element[2], element[3] };
                if (AddUser(arguments).Contains("Successfully"))
                {
                    success.Add(element[3]);
                }
                else
                {
                    failure.Add(element[3]);
                }
            }
            if(failure.Count == 0)
            {
                return "Successfully created all users!";
            }
            else
            {
                string failedUsers = "";
                foreach(string element in failure)
                {
                    failedUsers = failedUsers + " | " + element;
                }
                return "Failed to create users: " + failedUsers;
            }
        }
        #endregion

        #region login stuff
        private static string[] GetLoginData()
        {
            string[] result = new string[2];
            result[0] = ReadFromConsole("login")[0];
            result[1] = ReadFromConsole("password", true)[0];
            return result;
        }
        #endregion

        private static void QuitApplication()
        {
            Environment.Exit(0);
        }

        #region help messages
        private static string PrintHelp()
        {
            Console.WriteLine("commands: ");
            Console.WriteLine("");
            Console.WriteLine("Connect to Jira server. Needs to be done BEFORE anythin else happens");
            Console.WriteLine("connect");
            Console.WriteLine("");
            Console.WriteLine("Add user to Jira");
            Console.WriteLine("addUser <username> <password> <email> <displayName> (<Send Notification true/false>)");
            Console.WriteLine("");
            Console.WriteLine("Add users from CSV File, needs to be separated with | pipe char, needs to be in the same order as addUser");
            Console.WriteLine("addUsersFromFile <path\to\file.csv>");
            Console.WriteLine("");
            Console.WriteLine("Quit the application");
            Console.WriteLine("quit / q");
            return "";
        }
        #endregion
    }
}
