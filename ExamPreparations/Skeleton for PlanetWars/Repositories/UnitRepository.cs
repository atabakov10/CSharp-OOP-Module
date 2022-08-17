using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private readonly List<IMilitaryUnit> units;

        public UnitRepository()
        {
            units= new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models
        {
            get => units.AsReadOnly();
        }
        public void AddItem(IMilitaryUnit model)
        {
            units.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
        {
            var unitsToRemove = units.FirstOrDefault(x => x.GetType().Name == name);
            return unitsToRemove;
        }

        public bool RemoveItem(string name)
        {
            var unitsToRemove = units.FirstOrDefault(x => x.GetType().Name == name);
            return units.Remove(unitsToRemove);
        }
    }
}
