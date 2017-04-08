using System.Collections.Generic;
using System.Linq;
using Silone.Idle.Api;

namespace Silone.Idle.Data
{
    public class Unit
    {
        public string Sid { get; set; }
        public Person[] Persons { get; set; }
        public List<GameTask> Tasks { get; set; }
        public int CompleteCount { get; set; }
        public int RessuractionCounter { get; set; }

        public Unit(Person[] persons)
        {
            Persons = persons;
            Tasks = new List<GameTask>();
        }

        public bool IsDefeated
        {
            get
            {
                foreach (var person in Persons)
                {
                    if (!person.IsDefeated)
                        return false;
                }

                return true;
            }
        }

        public void UpdateRessuraction()
        {
            if (RessuractionCounter < 10)
            {
                RessuractionCounter++;
            }
            else
            {
                RessuractionCounter = 0;

                foreach (var person in Persons)
                {
                    person.HP = person.MaxHP;
                }
            }
        }

        public DtoUnit GetDto()
        {
            var currentTask = Tasks.First();

            return new DtoUnit
            {
                Sid = Sid,
                CompleteCount = CompleteCount,
                Persons = Persons.Select(x => x.GetDto()).ToArray(),
                GameTask = currentTask.GetDto()
            };
        }
    }
}
