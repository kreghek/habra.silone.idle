using System;
using System.Collections.Generic;
using System.Linq;

namespace Silone.Idle.Data.Services
{
    public class Dice : IDice
    {
        private Random rnd = new Random();

        public int RollDmg(int dmg)
        {
            var minDmg = (int)(dmg * 0.5f);
            var maxDmg = (int)(dmg * 1.5f);

            var resultDmg = rnd.Next(minDmg, maxDmg);
            return resultDmg;
        }

        public int RollMobGroupSize()
        {
            return rnd.Next(1, 10);
        }

        public void RollMobStates(Mob mob)
        {
            mob.Damage = rnd.Next(3, 8);
            mob.MaxHP = rnd.Next(10, 30);
        }

        public int RollTarget(IEnumerable<Mob> mobs)
        {
            return rnd.Next(mobs.Count());
        }

        public int RollTarget(IEnumerable<Person> person)
        {
            return rnd.Next(person.Count());
        }

        public int RollTaskIndex()
        {
            return rnd.Next(1, 11);
        }

        public int RollTaskValue(int index)
        {
            return rnd.Next(100 + index * 10, (int)((100 + index * 10) * 1.5f));
        }

        public bool TestCombat()
        {
            return rnd.Next(100) < 20;
        }

        public bool TestHit(Person person)
        {
            return rnd.Next(100) > 50;
        }

        public bool TestHit(Mob mob)
        {
            return rnd.Next(100) > 50;
        }

        public bool TestTaskProgress()
        {
            return rnd.Next(0, 100) < 70;
        }
    }
}
