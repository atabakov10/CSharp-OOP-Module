using System;
using System.Reflection.Metadata;
using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;

        private float baseHealth;

        private float health;
        private float baseArmor;
        private float armor;

        
        public float Armor
        {
            get => armor;
           private set
            {
                if (value < 0)
                {
                    return;
                } 
                armor = value;
            }
        }

        public float BaseArmor
        {
            get => baseArmor;
            set => baseArmor = value;
        }

        public float Health 
        {
            get => health;
            set
            {
                if (value > BaseHealth || value < 0)
                {
                    return;
                }
                health = value;
            }
        }

        public float BaseHealth
        {
            get => baseHealth;
            set => baseHealth = value;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.CharacterNameInvalid);
                }
                this.name = value;
            }
        }

        public Bag Bag { get; private set; }

        public float AbilityPoints { get; private set; } 

        public bool IsAlive { get; set; } = true;

		protected void EnsureAlive()
		{
			if (!this.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
		}
        public void TakeDamage(float hitPoints)
        {
            if (IsAlive != true)
            {
                return;
            }
            else
            {
                float leftHitPoints = 0;
                if (hitPoints > armor)
                {//   100     120 
                    leftHitPoints = Math.Abs(armor - hitPoints);
                    armor -= hitPoints;

                    health -= leftHitPoints;
                }
            }
        }
        public void UseItem(Item item)
        {
           EnsureAlive();
           item.AffectCharacter(this);
        }

	}
}