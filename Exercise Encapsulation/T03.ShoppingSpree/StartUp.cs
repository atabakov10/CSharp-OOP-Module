using System;
using System.Collections.Generic;
using System.Linq;

namespace T03.ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var personInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            var productInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);

            List<Person> people = new List<Person>();

            foreach (var person in personInput)
            {
                string[] personData = person.Split("=");
                var name = personData[0];
                var money = decimal.Parse(personData[1]);

                try
                {
                    Person currentPerson = new Person(name, money);
                    people.Add(currentPerson);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            List<Product> products = new List<Product>();

            foreach (var prod in productInput)
            {
                string[] productData = prod.Split("=");
                var name = productData[0];
                var cost = decimal.Parse(productData[1]);

                try
                {
                    Product currentProduct = new Product(name, cost);
                    products.Add(currentProduct);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            while (true)
            {
                var inputData = Console.ReadLine().Split();

                if (inputData[0] == "END")
                {
                    break;
                }

                var personName = inputData[0];
                var productName = inputData[1];

                if (people.Any(x => x.Name == personName) && products.Any(x => x.Name == productName))
                {
                    var person = people.Find(x => x.Name == personName);
                    var product = products.Find(x => x.Name == productName);

                    if (person.AddProduct(product))
                    {
                        Console.WriteLine($"{personName} bought {productName}");
                    }
                    else
                    {
                        Console.WriteLine($"{personName} can't afford {productName}");

                    }
                }
            }

            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }

    }
}