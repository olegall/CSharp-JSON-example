using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetByArgs()
        {
            // закомментированные строки вызывают исключения
            List<Employee> employees = new List<Employee>()
            {
                { new Employee(1, "Иван", "Иван", (decimal)100.5)},
                { new Employee(0, "Иван", "Иван", (decimal)100.5)},
                { new Employee(-1, "Иван", "Иванов", (decimal)100.5)},
                { new Employee(10000, "Иван", "Иванов", (decimal)100.5)},
                { new Employee(-10000, "Иван", "Иванов", (decimal)100.5)},
                { new Employee(1, "Иван", "Иванов", 0)},
                { new Employee(1, "Иван", "Иванов", -1)},
                { new Employee(1, "Иван", "Иванов", (decimal)10000.5)},
                { new Employee(1, "Иван", "Иванов", (decimal)100.5)},
                { new Employee(1, "Ivan", "Ivanov", (decimal)100.5)},
                { new Employee(1, "Iван", "Иваnov", (decimal)100.5)},
                { new Employee(1, "Иван3", "Иванов444444", (decimal)100.5)},
                { new Employee(1, "~`!@#$%^&*()-_+=?<>.,/\\|*'\"", "~`!@#$%^&*()-_+=?<>.,/\\|*'\"", (decimal)100.5)},
                { new Employee(1, null, null, (decimal)100.5)}
            };

            foreach (var empl in employees)
            {
                List<Argument> commandArgs = new List<Argument>()
                {
                    { new Argument("Id", empl.Id.ToString()) },
                    { new Argument("FirstName", empl.FirstName) },
                    { new Argument("LastName", empl.LastName) },
                    { new Argument("SalaryPerHour", empl.SalaryPerHour.ToString()) }
                };
                Assert.IsNotNull(empl);
                Assert.AreEqual(empl.Id, Employee.GetByArgs(commandArgs).Id);
                Assert.AreEqual(empl.FirstName, Employee.GetByArgs(commandArgs).FirstName);
                Assert.AreEqual(empl.LastName, Employee.GetByArgs(commandArgs).LastName);
                Assert.AreEqual(empl.SalaryPerHour, Employee.GetByArgs(commandArgs).SalaryPerHour);
            }
        }
    }
}