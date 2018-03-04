using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonClient jsonClient = new JsonClient();

            for (int i = 0; i < args.Length; i++)
            {
                Command command = new Command(args[i]);
                Command.Validate(command);
                int id = 0;
                switch (command.Operation)
                {
                    case "-get":
                        id = Convert.ToInt32(command.Arguments[0].Value);
                        Employee employee = jsonClient.GetObject(id);
                        if (employee != null)
                            Logger.Log(employee);
                        break;
                    case "-getall":
                        IEnumerable<Employee> employees = jsonClient.Read<Employee>();
                        break;
                    case "-delete":
                        id = Convert.ToInt32(command.Arguments[0].Value);
                        jsonClient.DeleteObject(id);
                        break;
                    case "-add":
                        jsonClient.AddObject(command.Arguments);
                        break;
                    case "-update":
                        jsonClient.UpdateObject(command.Arguments);
                        break;
                }
            }
            Console.ReadLine();
        }
    }
}
