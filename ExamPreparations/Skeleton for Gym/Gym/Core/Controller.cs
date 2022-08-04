using Gym.Core.Contracts;
using Gym.Models;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Repositories.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IEquipment> equipmentRepository;
        private ICollection<IGym> gymCollection;
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IGym gym = gymCollection.First(x => x.Name == gymName);

            if (athleteType != "Boxer" && athleteType != "Weightlifter")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }
            Type type = Type.GetType($"Gym.Models.Athletes.{athleteType}");
            object[] args = new object[]
            {
                athleteName,
                motivation,
                numberOfMedals
            };

            Athlete instance = null;
            try
            {
                instance = Activator.CreateInstance(type, args) as Athlete;
            }
            catch (TargetInvocationException ex)
            {

                throw ex.InnerException;
            }
            if (instance.AllowedGym != gym.GetType().Name)
            {
                return OutputMessages.InappropriateGym;
            }
            gym.AddAthlete(instance);
            return String.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);

        }
        public Controller()
        {
            equipmentRepository = new EquipmentRepository();
            gymCollection = new List<IGym>();
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment typeEquipment = null;
            if (equipmentType == "BoxingGloves")
            {
                typeEquipment = new BoxingGloves();
            }
            else if (equipmentType == "Kettlebell")
            {
                typeEquipment = new Kettlebell();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }
            equipmentRepository.Add(typeEquipment);
            return String.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym = null;
            if (gymType == "BoxingGym")
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }
            gymCollection.Add(gym);
            return String.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            throw new System.NotImplementedException();
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment equpment = equipmentRepository.FindByType(equipmentType);
            if (equpment == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }
            else
            {
                gymCollection.First(x => x.Name == gymName).AddEquipment(equpment);
            }
            return String.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);

        }
        

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var athlete in gymCollection)
            {
                sb.AppendLine(athlete.GymInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string TrainAthletes(string gymName)
        {
            ICollection<IAthlete> athletes = gymCollection.First(x => x.Name == gymName).Athletes;
            foreach (var athlete in athletes)
            {
                athlete.Exercise();
            }
            return string.Format(OutputMessages.EquipmentTotalWeight, athletes.Count);
        }
    }
}