using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> weapons;

        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models
        {
            get => weapons.AsReadOnly();
        }
        public void AddItem(IWeapon model)
        {
            weapons.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            var weaponToFind = weapons.FirstOrDefault(x => GetType().Name == name);
            return weaponToFind;
        }

        public bool RemoveItem(string name)
        {
           var weaponToRemove = weapons.FirstOrDefault(x => GetType().Name == name);
           return weapons.Remove(weaponToRemove);
        }
    }
}
