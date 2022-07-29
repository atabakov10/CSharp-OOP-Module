using System;
using System.Collections.Generic;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
        private List<Character> party;
        private List<Item> pool;
		public WarController()
		{
			pool = new List<Item>();
            party = new List<Character>();
        }

		public string JoinParty(string[] args)
		{
            var characterType = args[0];
            var name = args[1];
            if (characterType != "Warrior")
            { 

            }
			else if (characterType )
            {
                
            }
        }

		public string AddItemToPool(string[] args)
		{
			throw new NotImplementedException();
		}

		public string PickUpItem(string[] args)
		{
			throw new NotImplementedException();
		}

		public string UseItem(string[] args)
		{
			throw new NotImplementedException();
		}

		public string GetStats()
		{
			throw new NotImplementedException();
		}

		public string Attack(string[] args)
		{
			throw new NotImplementedException();
		}

		public string Heal(string[] args)
		{
			throw new NotImplementedException();
		}
	}
}
