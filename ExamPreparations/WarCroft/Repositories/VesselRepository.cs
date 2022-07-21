using System.Collections.Generic;
using System.Linq;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;

namespace NavalVessels.Repositories
{
    public class VesselRepository:IRepository<IVessel>
    {
        private readonly List<IVessel> repo;

        public VesselRepository()
        {
             repo = new List<IVessel>();
        }

        public IReadOnlyCollection<IVessel> Models => repo.AsReadOnly();
        public void Add(IVessel model)
        {
            this.repo.Add(model);
        }

        public bool Remove(IVessel model)
        {
            return this.repo.Remove(model);
        }

        public IVessel FindByName(string name)
        {
           
            return this.repo.FirstOrDefault(x => x.Name == name);
        }
    }
}