
namespace ConsoleApp1
{
    public class Argument
    {
        public string Name { get; set; }
        public string Value { get; set; }
        private const char delimeter = ':';

        public Argument(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public static string GetName(string userArgument)
        {
            return userArgument.Split(delimeter)[0];
        }

        public static string GetValue(string userArgument)
        {
            return userArgument.Split(delimeter)[1];
        }
    }
}
