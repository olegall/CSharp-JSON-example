using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Command
    {
        private static readonly string[]  operations = {"add", "get", "getall", "update", "delete"};
        private static readonly char prefix = '-';
        private static readonly char delimiter = ' ';
        private static readonly string nameCheckedForPositiveValue = "SalaryPerHour";

        public string Operation { get; set; }
        public List<Argument> Arguments { get; set; }

        public Command(string userCommand)
        {
            Operation = GetOperation(userCommand);
            Arguments = GetArguments(userCommand);
        }

        private string GetOperation(string userCommand)
        {
            return userCommand.Split(delimiter)[0];
        }

        private List<Argument> GetArguments(string userCommand)
        {
            List<String> userArguments = userCommand.Split(delimiter).ToList();
            userArguments.RemoveAt(0);

            List<Argument> arguments = new List<Argument>();
            foreach (var userArg in userArguments)
            {
                string name = Argument.GetName(userArg);
                string value = Argument.GetValue(userArg);
                arguments.Add(new Argument(name, value));
            }
            return arguments;
        }

        public static void Validate(Command command)
        {
            List<Argument> args = command.Arguments;

            string operation = command.Operation;
            if (!operation.StartsWith(prefix.ToString()))
                throw new ArgumentException("Нет префикса операции или он неправильный");

            if (args.Any(x => x.Name == String.Empty))
                throw new ArgumentException("В команде нет имени аргумента");

            if (args.Any(x => x.Value == String.Empty))
                throw new ArgumentException("В команде нет значения аргумента");

            if (!operations.Contains(operation.Replace(prefix.ToString(), String.Empty)))
                throw new ArgumentException("Такой команды нет в списке или она введена неправильно");

            if (args.Select(x => x.Name).Contains("Id"))
            {
                string id = args.Where(x => x.Name == "Id").Select(x => x.Value).SingleOrDefault();
                if (Convert.ToInt32(id) <= 0)
                    throw new ArgumentException("В команде Значение Id отрицательное или равно 0");
            }
            
            if (args.Select(x => x.Name).Contains(nameCheckedForPositiveValue))
            {
                var value = args.Where(x => x.Name == nameCheckedForPositiveValue).Select(x => x.Value).SingleOrDefault().Replace(".", ",");
                if (Convert.ToDecimal(value) <= 0)
                    throw new ArgumentException("Значение аргумента SalaryPerHour меньше или равно 0");
            }

            IEnumerable<string> employeeProps = typeof(Employee).GetProperties().Select(x => x.Name);
            IEnumerable<string> argNames = args.Select(x => x.Name);
            if (employeeProps.Intersect(argNames).Count() != argNames.Count())
                throw new ArgumentException("Некорректное имя аргумента команды");
        }
    }
}