using System;
using System.Collections.Generic;
using System.Text;

namespace T03.ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<string> products;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.products = new List<string>();
        }

        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOperationException("Name cannot be empty");
                }
                this.name = value;
            }
        }

        public decimal Money
        {
            get => this.money;

            private set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("Money cannot be negative");
                }
                this.money = value;
            }
        }

        public bool AddProduct(Product product)
        {
            if (this.Money < product.Cost)
            {
                return false;
            }

            this.products.Add(product.Name);
            this.Money -= product.Cost;
            return true;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"{this.Name} - ");

            if (this.products.Count == 0)
            {
                result.Append("Nothing bought");
            }
            else
            {
                result.Append(string.Join(", ", this.products));
            }
            return result.ToString();
        }
    }
}