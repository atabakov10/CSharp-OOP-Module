using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using PlanetWars.Utilities.Messages;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using System.Linq;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Core.Contracts;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planetRepository;

        public Controller()
        {
            this.planetRepository = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            var planetExist = this.planetRepository.FindByName(planetName);

            if (planetExist == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (unitTypeName != "SpaceForces" && unitTypeName != "StormTroopers" && unitTypeName != "AnonymousImpactUnit")
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            if (planetExist.Army.Any(u => u.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit unit = null;

            if (unitTypeName == "SpaceForces")
            {
                unit = new SpaceForces();
            }
            else if (unitTypeName == "StormTroopers")
            {
                unit = new StormTroopers();
            }
            else if (unitTypeName == "AnonymousImpactUnit")
            {
                unit = new AnonymousImpactUnit();
            }

            planetExist.Spend(unit.Cost);
            planetExist.AddUnit(unit);

            return String.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            var planetExis = this.planetRepository.FindByName(planetName);

            if (planetExis == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planetExis.Weapons.Any(u => u.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            if (weaponTypeName != "SpaceMissiles" && weaponTypeName != "NuclearWeapon" && weaponTypeName != "BioChemicalWeapon")
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            IWeapon weapon = null;

            if (weaponTypeName == "SpaceMissiles")
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else if (weaponTypeName == "NuclearWeapon")
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == "BioChemicalWeapon")
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }

            planetExis.Spend(weapon.Price);
            planetExis.AddWeapon(weapon);

            return String.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            if (this.planetRepository.FindByName(name) != null)
            {
                return String.Format(OutputMessages.ExistingPlanet, name);
            }
            else
            {
                IPlanet planet = new Planet(name, budget);
                this.planetRepository.AddItem(planet);

                return String.Format(OutputMessages.NewPlanet, name);
            }
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in this.planetRepository.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var planetOneByName = this.planetRepository.FindByName(planetOne);
            var planeTwoByName = this.planetRepository.FindByName(planetTwo);

            var planetOneMP = planetOneByName.MilitaryPower;
            var planetTwoMP = planeTwoByName.MilitaryPower;

            IPlanet winner = null;
            IPlanet loser = null;

            if (planetOneMP == planetTwoMP)
            {
                if ((planetOneByName.Weapons.Any(w => w.GetType().Name == "NuclearWeapon") && planeTwoByName.Weapons.Any(w => w.GetType().Name == "NuclearWeapon")) || (planetOneByName.Weapons.Any(w => w.GetType().Name != "NuclearWeapon") && planeTwoByName.Weapons.Any(w => w.GetType().Name != "NuclearWeapon")))
                {
                    var budgetPlanetOne = planetOneByName.Budget * 0.5;
                    var budgetPlanetTwo = planeTwoByName.Budget * 0.5;

                    planetOneByName.Spend(budgetPlanetOne);
                    planeTwoByName.Spend(budgetPlanetTwo);

                    return String.Format(OutputMessages.NoWinner);
                }
                else if (planetOneByName.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    winner = planetOneByName;
                    loser = planeTwoByName;
                }
                else if (planeTwoByName.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    winner = planeTwoByName;
                    loser = planetOneByName;
                }
            }
            else if (planetOneMP > planetTwoMP)
            {
                winner = planetOneByName;
                loser = planeTwoByName;
            }
            else if (planetOneMP < planetTwoMP)
            {
                winner = planeTwoByName;
                loser = planetOneByName;
            }
            var winnerSlashBudget = winner.Budget * 0.5;
            var loserHalfBudget = loser.Budget * 0.5;

            winner.Spend(winnerSlashBudget);
            winner.Profit(loserHalfBudget);

            var loserAss = loser.Army.Sum(u => u.Cost) + loser.Weapons.Sum(w => w.Price);
            winner.Profit(loserAss);
            this.planetRepository.RemoveItem(loser.Name);

            return String.Format(OutputMessages.WinnigTheWar, winner.Name, loser.Name);
        }

        public string SpecializeForces(string planetName)
        {
            var planet = this.planetRepository.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.NoUnitsFound));
            }

            planet.Spend(1.25);
            planet.TrainArmy();

            return String.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}