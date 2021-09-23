using System.Diagnostics;

namespace IniFileUser.Models
{
    public class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void SayHello()
        {
            Debug.WriteLine($"I am {Name}, {Age} years old.");
        }

        public void Update(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
