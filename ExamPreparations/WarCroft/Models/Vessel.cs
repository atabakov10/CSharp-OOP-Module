using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;

namespace NavalVessels.Models
{
    public abstract class Vessel:IVessel
    {
        protected Vessel(string name, double mainWeaponCaliber,double speed,double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;


        }
        private string name;
        private ICaptain captain;
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidVesselName);
                }
                name = value;
            }
        }

        public ICaptain Captain
        {
            get { return captain; }
             set
            {
                if (value==null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCaptainToVessel);
                }
                captain=value;
            }
        }
        
           
        public double ArmorThickness { get; set; }
        public double MainWeaponCaliber { get; set; }
        public double Speed { get; set; }
        public ICollection<string> Targets { get; set; } = new List<string>();
        public virtual void Attack(IVessel target)
        {
            if (target is null)
            {
                throw new NullReferenceException("Target cannot be null.");
            }
            this.ArmorThickness -= target.MainWeaponCaliber;
            if (this.ArmorThickness < 0)
            {
                this.ArmorThickness = 0;
            }
            this.Targets.Add(target.Name);
        }

        public virtual void RepairVessel()
        {
            
        }

        public override string ToString()
        {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine($"- {name}");
           sb.AppendLine($"*Type: {this.GetType().Name}");
           sb.AppendLine($"*Armor thickness: {ArmorThickness}");
           sb.AppendLine($"*Main weapon caliber: {MainWeaponCaliber}");
           sb.AppendLine($"*Speed: {Speed} knots");
           if (Targets.Any())
           {
               sb.AppendLine(string.Join(", ", Targets));
           }
           else
           {
               sb.AppendLine("None");
           }
           return sb.ToString().Trim();

        }
    }
}