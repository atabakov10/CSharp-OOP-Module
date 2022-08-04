using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private readonly List<Item> items;

        
        private int capacity = 100;

        protected Bag(int capacity)
        {
            Capacity = capacity;
            this.items = new List<Item>();
        }
        public int Capacity
        {
            get { return capacity;}
            set { value = capacity; }
        }

        public int Load => Items.Select(x=> x.Weight).Sum();
        public IReadOnlyCollection<Item> Items => items;
        public void AddItem(Item item)
        {
            if (Load + item.Weight > Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }
            this.items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (!items.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }
            var findItem = items.FirstOrDefault(x => x.GetType().Name == name);
            if (findItem is null)
            {
                throw  new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }

            items.Remove(findItem);
            return findItem;
        }
    }
}
