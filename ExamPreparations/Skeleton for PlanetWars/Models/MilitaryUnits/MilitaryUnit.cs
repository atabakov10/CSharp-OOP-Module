using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using PlanetWars.Models.MilitaryUnits.Contracts;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            this.Cost = cost;
            EnduranceLevel = 1;
        }
        public double Cost
        {
            get => cost;
            private set
            {
                cost = value;
            }
        }

        public int EnduranceLevel
        {
            get => this.enduranceLevel;
            private set => this.enduranceLevel = 1;

        }
        public void IncreaseEndurance()
        {
            EnduranceLevel++;
            if (EnduranceLevel >= 20)
            {
                EnduranceLevel = 20;
                throw new ArgumentException("Endurance level cannot exceed 20 power points.");
            }
        }
    }
}
