using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> heroes)
        {

            var knights = heroes.Where(h => h.GetType().Name == nameof(Knight)).ToList();
            var barbarians = heroes.Where(h => h.GetType().Name == nameof(Barbarian)).ToList();

            int deadKnights = 0;
            int deadBarbarians = 0;

            var thereAlive = barbarians.FirstOrDefault(k => k.IsAlive == true) != null;

            while (thereAlive)
            {

                foreach (var knight in knights)
                {
                    if (knight.IsAlive)
                    {
                        foreach (var barbarian in barbarians)
                        {
                            if (barbarian.IsAlive && knight.Weapon != null)
                            {
                                barbarian.TakeDamage(knight.Weapon.DoDamage());

                                if (!barbarian.IsAlive)
                                {
                                    deadBarbarians++;
                                }
                            }
                        }
                    }

                }

                foreach (var barbarian in barbarians)
                {
                    if (barbarian.IsAlive)
                    {
                        foreach (var knight in knights)
                        {
                            if (knight.IsAlive && barbarian.Weapon != null)
                            {
                                knight.TakeDamage(barbarian.Weapon.DoDamage());

                                if (!knight.IsAlive)
                                {
                                    deadKnights++;
                                }
                            }
                        }
                    }

                }
                thereAlive = barbarians.FirstOrDefault(k => k.IsAlive == true) != null;
            }
            if (knights.Count != 0)
            {
                return $"The knights took {deadKnights} casualties but won the battle.";
            }

            else
            {
                return $"The barbarians took {deadBarbarians} casualties but won the battle.";
            }

        }
    }
}