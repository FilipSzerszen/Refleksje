using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Refleksje
{
    class Program
    {
        static void Display(object obj)
        {
            Type objType = obj.GetType();
            var properties = objType.GetProperties();

            foreach (var property in properties)
            {
                var propValue = property.GetValue(obj);
                var propType = propValue.GetType();
                if (propType.IsPrimitive || propType == typeof(string))
                {
                    var displayPropertyAttribute = property.GetCustomAttribute<DisplayPropertyAttribute>();
                    if (displayPropertyAttribute != null)
                        Console.WriteLine($"property {displayPropertyAttribute.DisplayName}: {propValue}");
                    else
                        Console.WriteLine($"property {property.Name}: {propValue}");

                }
            }
        }


        static void Main(string[] args)
        {
            Address address = new Address()
            {
                City = "Krakow",
                PostalCode = "31-556",
                Street = "Grodzka 5"
            };

            Person person = new Person()
            {
                FirstName = "John",
                LastName = "Doe",
                Address = address
            };
            Display(person);
            Display(person.Address);

            Console.WriteLine("\r\nInsert person property to change:");
            var propertyToUpdate = Console.ReadLine();
            Console.WriteLine("\r\nInsert value to change:");
            var valueToUpdate = Console.ReadLine();


            setValue(person, propertyToUpdate, valueToUpdate);

            Display(person);
            Display(person.Address);
        }
        public static void setValue<T>(T obj, string prop, string value)
        {
            Type objType = obj.GetType();
            var properties = objType.GetProperty(prop);

            if (properties != null)
            {
                properties.SetValue(obj, value);
            }
        }
    }
}
