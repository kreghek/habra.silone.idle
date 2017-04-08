using System;
using System.Linq;
using Silone.Idle.Data.Services;

namespace Silone.Idle.Data
{
    public class CombatGameTask: GameTask
    {
        public Mob[] Mobs { get; set; }

        public override bool Storyline => false;

        public CombatGameTask(Mob[] mobs)
        {
            Mobs = mobs;
            foreach (var mob in Mobs)
            {
                Value += mob.MaxHP;
            }
        }

        public override void Update(IDice dice, Unit unit)
        {
            foreach (var person in unit.Persons)
            {
                if (person.IsDefeated)
                    continue;

                if (Progress >= Value)
                    break;

                if (dice.TestHit(person))
                {
                    var availableTargetMobs = Mobs.Where(x => x.HP > 0).ToArray();
                    if (!availableTargetMobs.Any())
                        continue;

                    var targetMobIndex = dice.RollTarget(availableTargetMobs);
                    var targetMob = availableTargetMobs[targetMobIndex];

                    var dmg = dice.RollDmg(person.Damage);
                    var normalDmg = Math.Min(targetMob.HP, dmg);
                    targetMob.HP -= normalDmg;
                    Progress += normalDmg;                    
                }
            }

            if (Progress >= Value)
            {
                Resolve();
                return;
            }

            var availableMobs = Mobs.Where(x => x.HP > 0).ToArray();
            foreach (var mob in availableMobs)
            {
                if (mob.HP <= 0)
                    continue;

                if (dice.TestHit(mob))
                {
                    var availableTargetPersons = unit.Persons.Where(x => !x.IsDefeated).ToArray();
                    if (!availableTargetPersons.Any())
                        continue;

                    var targetPersonIndex = dice.RollTarget(availableTargetPersons);
                    var targetPerson = availableTargetPersons[targetPersonIndex];

                    var dmg = dice.RollDmg(mob.Damage);
                    var normalDmg = Math.Min(targetPerson.HP, dmg);
                    targetPerson.HP -= normalDmg;
                }
            }

            var availablePersons = unit.Persons.Where(x => !x.IsDefeated).ToArray();
            if (!availablePersons.Any())
            {
                Reject();
            }
        }
    }
}
