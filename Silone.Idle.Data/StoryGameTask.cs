using System;
using Silone.Idle.Data.Services;

namespace Silone.Idle.Data
{
    public class StoryGameTask : GameTask
    {
        public override bool Storyline => true;

        public override void Update(IDice dice, Unit unit)
        {
            if (dice.TestTaskProgress())
            {
                Progress++;
            }

            if (Progress >= Value)
            {
                Resolve();
            }
        }
    }
}
