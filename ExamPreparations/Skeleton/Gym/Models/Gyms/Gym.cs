using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private int capacity;
        private readonly ICollection<IEquipment> equipments;
        private readonly ICollection<IAthlete> athletes;

        protected Gym(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
        }

        public string Name
        {
            get 
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }
                name = value;
            }
        }

        public int Capacity
        {
            get
            {
                return capacity;
            }
            private set
            {
                capacity = value;
            }
        }

        public double EquipmentWeight => equipments.Select(x => x.Weight).Sum();

        public ICollection<IEquipment> Equipment => equipments;

        public ICollection<IAthlete> Athletes => athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (Capacity == 0)
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughSize);
            }
            athletes.Add(athlete);
            Capacity--;
        }

        public void AddEquipment(IEquipment equipment)
        {
            equipments.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} is a {this.Name.GetType()}:");
            if (athletes.Any())
            {
                foreach (var athlete in athletes)
                {
                    sb.AppendLine($"Athletes: {athlete.FullName}");
                }
            }
            else
            {
                sb.AppendLine("No athletes");
            }
            sb.AppendLine($"Equipment total count: {equipments.Count}");
            sb.AppendLine($"Equipment total weight: {EquipmentWeight:f2} grams");
            return sb.ToString().Trim();               
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            var removedAthlete = athletes.FirstOrDefault(x => x.FullName == athlete.FullName);
            if (removedAthlete != null)
            {
                return true;
            }
            return false;
        }
    }
}
