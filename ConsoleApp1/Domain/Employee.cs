using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal SalaryPerHour { get; set; }

        public Employee(int id, string firstName, string lastName, decimal salaryPerHour)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SalaryPerHour = salaryPerHour;
        }

        public Employee()
        {
        }

        public static Employee GetByArgs(List<Argument> arguments)
        {
            Employee employee = new Employee();
            foreach (Argument argument in arguments)
            {
                string name = argument.Name;
                string propertyTypeName = employee.GetType().GetProperty(name).PropertyType.Name;
                Type propertyType = Type.GetType("System." + propertyTypeName);

                string value = argument.Value;
                if (propertyTypeName == "Decimal")
                    value = value.Replace(".", ",");

                dynamic valueDyn = Convert.ChangeType(value, propertyType);
                employee.GetType().GetProperty(name).SetValue(employee, valueDyn);
            }
            return employee;
        }
    }
}
