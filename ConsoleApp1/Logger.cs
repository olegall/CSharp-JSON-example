using System;

namespace ConsoleApp1
{
    public class Logger
    {
        private Logger() { }

        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void Log(Employee employee)
        {
            Console.WriteLine("Id: " + employee.Id + "  First name: " + employee.FirstName + "  Last name: " + employee.LastName + "  SalaryPerHour: " + employee.SalaryPerHour);
        }
    }
}