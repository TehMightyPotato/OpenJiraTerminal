using System;

namespace OpenJiraTerminal
{
    class Program
    {
        private static ConnectionHandler connectionHandler;
        private static UserManager userManager;
        private static JsonConverter jsonCOnverter;

        static void Main(string[] args)
        { 

            if(args.Length == 0)
            {
                PrintHelp();
                return;
            }
            switch (args[0])
            {
                case ("addUser"):
                    AddUser(args);
                    break;
            }
        }

        private static void AddUser(string[] args)
        {
            foreach( var arg in args)
            {
                Console.WriteLine(arg);
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("Sample Help text");
        }
    }
}
