using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
{
    public abstract class Weapon : IWeapon
    {
        private string name;
        protected int durability;

        protected Weapon(string name, int durability)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Weapon type cannot be null or empty!");
            }

            if (durability < 0)
            {
                throw new ArgumentException("Durability cannot be below 0.");
            }

            this.name = name;
            this.durability = durability;
        }

        public string Name { get { return name; } }

        public int Durability { get { return durability; } }

        public abstract int DoDamage();

    }
}