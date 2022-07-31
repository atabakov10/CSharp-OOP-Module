using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private readonly List<IFormulaOneCar> cars;

        public FormulaOneCarRepository()
        {
            cars = new List<IFormulaOneCar>();
        }
        public IReadOnlyCollection<IFormulaOneCar> Models
        {
            get => cars.AsReadOnly();
        }
        public void Add(IFormulaOneCar model)
        {
            cars.Add(model);
        }

        public bool Remove(IFormulaOneCar model)
        {
            return cars.Remove(model);
        }

        public IFormulaOneCar FindByName(string name)
        {
           return cars.FirstOrDefault(c => c.Model == name);
        }
    }
}
