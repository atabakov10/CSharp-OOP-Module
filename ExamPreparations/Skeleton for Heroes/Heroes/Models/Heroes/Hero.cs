using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Heroes
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon _weapon;
        private bool isAlive;


        protected Hero(string _name, int _health, int _armour)
        {
            if (string.IsNullOrEmpty(_name))
            {
                throw new ArgumentException("Hero name cannot be null or empty!");
            }

            if (_health < 0)
            {
                throw new ArgumentException("Hero health cannot be below 0.");
            }

            if (_armour < 0)
            {
                throw new ArgumentException("Hero armour cannot be below 0.");
            }

            name = _name;
            health = _health;
            armour = _armour;
            isAlive = true;
        }

        public string Name { get { return name; } }

        public int Health
        {
            get
            {
                return health;
            }
        }
        public int Armour
        {
            get
            {
                return armour;
            }
        }

        public bool IsAlive { get { return isAlive; } }

        public IWeapon Weapon
        {
            get
            {
                return _weapon;
            }
        }

        public void AddWeapon(IWeapon weapon)
        {
            if (weapon == null)
            {
                throw new ArgumentException("Weapon cannot be null.");
            }
            _weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            armour -= points;

            if (armour < 0)
            {
                health += armour;
                armour = 0;
            }

            if (health <= 0)
            {
                isAlive = false;
                health = 0;
            }
        }
    }
}