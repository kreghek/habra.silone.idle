using System.Collections.Generic;
using System.Linq;

namespace Silone.Idle.Data.Services
{
    public class WorldManager
    {
        private readonly IDice dice;

        public WorldManager(IDice dice)
        {
            this.dice = dice;
        }

        public void Process(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                ProcessUnit(unit);
            }
        }

        private void ProcessUnit(Unit unit)
        {
            if (unit.IsDefeated)
            {
                var currentTask = unit.Tasks.FirstOrDefault();
                if (currentTask != null && !currentTask.Storyline)
                {
                    unit.Tasks.Remove(currentTask);
                }

                unit.UpdateRessuraction();
                return;
            }

            if (unit.Tasks.Any())
            {
                var currentTask = unit.Tasks.First();
                if (currentTask.Storyline && dice.TestCombat())
                {
                    GenerateCombatTask(unit);
                }
                else
                {
                    ProcessUnitTask(unit, currentTask);
                }
            }
            else
            {
                GenerateStorylineTask(unit);
            }
        }

        private void GenerateCombatTask(Unit unit)
        {
            var mobGroupSize = dice.RollMobGroupSize();
            var mobs = new Mob[mobGroupSize];
            for (var i = 0; i < mobGroupSize; i++)
            {
                mobs[i] = new Mob
                {
                    Name = "Mob " + i
                };

                dice.RollMobStates(mobs[i]);
                mobs[i].HP = mobs[i].MaxHP;
            }

            var task = new CombatGameTask(mobs)
            {
                Title = "Combat"
            };
            unit.Tasks.Insert(0, task);
        }

        private void GenerateStorylineTask(Unit unit)
        {
            var taskIndex = dice.RollTaskIndex();
            unit.Tasks.Add(new StoryGameTask
            {
                Title = "Task " + taskIndex,
                Value = dice.RollTaskValue(taskIndex),
            });
        }

        private void ProcessUnitTask(Unit unit, GameTask task)
        {
            if (task.State != GameTaskState.WIP)
            {
                unit.Tasks.Remove(task);
                if (task.Storyline && task.State == GameTaskState.Done)
                {
                    unit.CompleteCount++;
                }
            }
            else
            {
                task.Update(dice, unit);
            }
        }
    }
}
