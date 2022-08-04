using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Heroes
{
    public class Barbarian : Hero
    {
        public Barbarian(string _name, int _health, int _armour)
            : base(_name, _health, _armour)
        {
        }
    }
}