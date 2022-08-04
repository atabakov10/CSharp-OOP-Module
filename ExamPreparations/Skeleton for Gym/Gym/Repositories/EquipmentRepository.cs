using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> equipmentRepository;
        public EquipmentRepository()
        {
            equipmentRepository = new List<IEquipment>();
        }
        public IReadOnlyCollection<IEquipment> Models => equipmentRepository;

        public void Add(IEquipment model)
        {
            equipmentRepository.Add(model);
        }

        public IEquipment FindByType(string type)
        {
            return equipmentRepository.FirstOrDefault(x => x.GetType().Name == type);
        }

        public bool Remove(IEquipment model)
        {
            return equipmentRepository.Remove(model);
        }
    }
}
