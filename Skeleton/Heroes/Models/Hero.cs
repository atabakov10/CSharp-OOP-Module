using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Models.Contracts;

namespace Heroes.Models
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            this.Name = name;
            this.Health = health;
            this.Armour = armour;
        }
        public string Name
        {
            get => name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Hero name cannot be null or empty.");
                }
                name = value;
            }
        }
        public int Health
        {
            get => health;

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero health cannot be below 0.");
                }
                health = value;
            }
        }
        public int Armour
        {
            get => armour;

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero armour cannot be below 0.");
                }
                armour = value;
            }
        }
        public IWeapon Weapon
        {
            get => weapon;

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Weapon cannot be null.");
                }
                weapon = value;
            }
        }

        public bool IsAlive
        {
            get => true;
            private set
            {
                if (Health > 0)
                {
                    value = true;
                }

                value = false;
            }
        }

        public void TakeDamage(int points)
        {
            var pointsLeft = 0;
            while (Armour >= 0)
            { 
                Armour-=points;
                if (Armour <= 0)
                {
                    Armour = 0;
                    pointsLeft += points;
                    break;
                }
            }

            if (Health > 0)
            {
                Health -= pointsLeft;
                if (Health <= 0)
                {
                    Health = 0;
                }
            }
        }

        public void AddWeapon(IWeapon weapon)
        {
            AddWeapon(weapon);
        }
    }
}
