using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace ConsoleApp1
{
    class JsonClient
    {
        private readonly string path = Environment.CurrentDirectory.Replace("\\bin\\Debug", String.Empty)+"\\Employees.txt";

        public IEnumerable<T> Read<T>()
        {
            List<T> json = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
            if (json == null)
                throw new FileNotFoundException("Файл с JSON пустой");
            return json;
        }

        public void Write<T>(List<T> employees)
        {
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                new JsonSerializer().Serialize(writer, employees);
            }
        }

        public void AddObject(List<Argument> arguments)
        {
            List<Employee> employeesFromJSON = (List<Employee>)Read<Employee>();
            Employee employee = Employee.GetByArgs(arguments);
            employee.Id = employeesFromJSON.Max(x => x.Id) + 1;
            employeesFromJSON.Add(employee);
            Write(employeesFromJSON);
        }
                        
        public Employee GetObject(int id)
        {
            if (!HasJsonId(id))
            {
                Logger.Log("Нет такого Id в JSON файле!. Нельзя получить JSON объект");
                return null;
            }
            Employee employee = Read<Employee>().Where(x => x.Id == id).SingleOrDefault();
            return employee;
        }

        public void UpdateObject(List<Argument> arguments)
        {
            var id = Convert.ToInt32(arguments.Where(x => x.Name == "Id").SingleOrDefault().Value);
            if (!HasJsonId(id))
            {
                Logger.Log("Нет такого Id в JSON файле!. Нельзя обновить JSON объект");
                return;
            }
            List<Employee> employeesByJSON = (List<Employee>)Read<Employee>();
            Employee employeeByCommand = Employee.GetByArgs(arguments);
            foreach (Employee jsonEmployee in employeesByJSON)
            {
                if (jsonEmployee.Id == employeeByCommand.Id)
                {
                    IEnumerable<PropertyInfo> props = new List<PropertyInfo>(jsonEmployee.GetType().GetProperties());
                    foreach (PropertyInfo prop in props)
                    {
                        object propValue = prop.GetValue(employeeByCommand, null);
                        bool isStringNotNull = prop.PropertyType.Name == "String" && propValue != null;
                        bool isDecimalNotZero = prop.PropertyType.Name == "Decimal" && Convert.ToDecimal(propValue) != 0;
                        if (isStringNotNull || isDecimalNotZero)
                            prop.SetValue(jsonEmployee, propValue);
                    }
                }
            }
            Write(employeesByJSON);
        }

        public void DeleteObject(int id)
        {
            List<Employee> employees = (List<Employee>)Read<Employee>();
            employees.RemoveAll(x => x.Id == id);
            Write(employees);
        }

        private bool HasJsonId(int id)
        {
            int jsonId = Read<Employee>().Where(x => x.Id == id).Select(x => x.Id).SingleOrDefault();
            if (jsonId == 0)
                return false;
            return true;
        }
    }
}