using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;

namespace NavalVessels.Models
{
    public class Captain:ICaptain
    {
        private string fullname;
        private int combatExperience;
        private List<IVessel> vessels;

        public Captain(string fullname)
        {
            this.fullname = fullname;
           vessels= new List<IVessel>();
        }
        public string FullName
        {
            get { return fullname; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);
                }
                fullname = value;
            }
        }
        public int CombatExperience
        {
            get { return combatExperience;}
            private set
            {
                
                combatExperience = value;
               
            }
        }

        public ICollection<IVessel> Vessels => vessels;
        public void AddVessel(IVessel vessel)
        {
            if (vessel is null)
            {
                throw new ArgumentNullException(ExceptionMessages.InvalidVesselForCaptain);
            }
            this.Vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            combatExperience += 10;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {this.Vessels.Count} vessels.");
            sb.AppendLine(base.ToString());
            return sb.ToString().TrimEnd();
        }
    }
}