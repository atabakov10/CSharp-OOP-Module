using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using PlanetWars.Utilities.Messages;
using System.Linq;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        public UnitRepository units;
        public WeaponRepository weapons;

        public Planet(string name, double budget)
        {
            this.Name = name;
            this.Budget = budget;
            this.units = new UnitRepository();
            this.weapons = new WeaponRepository();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidPlanetName));
                }

                this.name = value;
            }
        }

        public double Budget
        {
            get => this.budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidBudgetAmount));
                }

                this.budget = value;
            }
        }

        public double MilitaryPower => MilitaryPowerCalculator();

        public IReadOnlyCollection<IMilitaryUnit> Army => this.units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            this.units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");
            sb.AppendLine($"--Forces: {(this.Army.Count > 0 ? string.Join(", ", this.Army.Select(u => u.GetType().Name)) : "No units")}");
            sb.AppendLine($"--Combat equipment: {(this.Weapons.Count > 0 ? string.Join(", ", this.Weapons.Select(w => w.GetType().Name)) : "No weapons")}");
            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public void Spend(double amount)
        {
            if (this.Budget - amount < 0)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnsufficientBudget));
            }

            this.Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in this.units.Models)
            {
                unit.IncreaseEndurance();
            }
        }

        private double MilitaryPowerCalculator()
        {
            double power = this.Army.Sum(u => u.EnduranceLevel) + this.Weapons.Sum(w => w.DestructionLevel);

            if (this.Army.Any(u => u.GetType().Name == "AnonymousImpactUnit"))
            {
                power *= 1.30;
            }

            if (this.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                power *= 1.45;
            }

            return Math.Round(power, 3);
        }
    }
}