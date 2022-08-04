using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private readonly HeroRepository heroRepository;
        private readonly WeaponRepository weaponRepository;

        public Controller()
        {
            heroRepository = new HeroRepository();
            weaponRepository = new WeaponRepository();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var hero = heroRepository.FindByName(heroName);
            var weapon = weaponRepository.FindByName(weaponName);

            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }
            if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }
            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }


            hero.AddWeapon(weapon);
            weaponRepository.Remove(weapon);

            return $"Hero {hero.Name} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero;

            if (heroRepository.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }

            if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
                heroRepository.Add(hero);

                return $"Successfully added Sir {name} to the collection.";
            }

            else if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
                heroRepository.Add(hero);

                return $"Successfully added Barbarian {name} to the collection.";
            }

            else
            {
                throw new InvalidOperationException("Invalid hero type.");
            }
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon;

            if (weaponRepository.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }

            if (type == nameof(Claymore))
            {
                weapon = new Claymore(name, durability);
                weaponRepository.Add(weapon);

                return $"A {type.ToLower()} {name} is added to the collection.";
            }

            else if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
                weaponRepository.Add(weapon);

                return $"A {type.ToLower()} {name} is added to the collection.";
            }
            else
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }


        }

        public string HeroReport()
        {
            var results = new StringBuilder();
            var heroes = heroRepository.Models
                .ToList()
                .OrderBy(h => h.GetType().Name)
                .ThenByDescending(h => h.Health)
                .ThenByDescending(h => h.Name);

            foreach (var hero in heroes)
            {
                var nameWeapon = hero.Weapon == null ? "Unarmed" : hero.Weapon.Name;

                results.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                results.AppendLine($"--Health: {hero.Health}");
                results.AppendLine($"--Armour: {hero.Armour}");
                results.AppendLine($"--Weapon: {nameWeapon}");
            }

            return results.ToString();
        }

        public string StartBattle()
        {
            IMap map = new Map();

            return map.Fight(heroRepository.Models.ToList());
        }
    }
}