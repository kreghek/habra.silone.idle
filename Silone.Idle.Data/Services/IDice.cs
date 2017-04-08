using System.Collections.Generic;

namespace Silone.Idle.Data.Services
{
    public interface IDice
    {
        int RollTaskIndex();
        int RollTaskValue(int index);
        bool TestTaskProgress();

        bool TestCombat();
        int RollMobGroupSize();
        void RollMobStates(Mob mob);

        bool TestHit(Person person);
        bool TestHit(Mob mob);
        int RollTarget(IEnumerable<Mob> mobs);
        int RollTarget(IEnumerable<Person> person);
        int RollDmg(int dmg);
    }
}
