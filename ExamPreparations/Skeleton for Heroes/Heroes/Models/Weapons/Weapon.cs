using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private string name;
        protected int durability;

        public Weapon(string _name, int _durability)
        {
            if (string.IsNullOrEmpty(_name))
            {
                throw new ArgumentException("Weapon type cannot be null or empty!");
            }

            if (_durability < 0)
            {
                throw new ArgumentException("Durability cannot be below 0.");
            }

            name = _name;
            durability = _durability;
        }

        public string Name { get { return name; } }

        public int Durability { get { return durability; } }

        public abstract int DoDamage();

    }
}