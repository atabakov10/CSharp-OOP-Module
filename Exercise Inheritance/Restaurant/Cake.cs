using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class Cake : Dessert
    {
        public Cake(string name, decimal price, double grams, double calories) : base(name, 5, 250, 1000)
        { 
        }
    }
}
