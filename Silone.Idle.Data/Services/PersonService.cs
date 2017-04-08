using System.Collections.Generic;

namespace Silone.Idle.Data.Services
{
    public static class PersonService
    {
        public static List<Unit> Units { get; set; }

        static PersonService()
        {
            var persons = new[] {
                new Person {
                    FullName = "person1 full name",
                    ShortName = "person1",
                    Class = "classname",
                    Damage = 10,
                    MaxHP = 100,
                    HP = 100
                },

                new Person {
                    FullName = "person2 full name",
                    ShortName = "person2",
                    Class = "classname2",
                    Damage = 15,
                    MaxHP = 80,
                    HP = 80
                },

                new Person {
                    FullName = "person3 full name",
                    ShortName = "person3",
                    Class = "classname2",
                    Damage = 8,
                    MaxHP = 150,
                    HP = 150
                }
            };

            Units = new List<Unit> {
                new Unit(persons) {
                    Sid = "Hero Band"
                }
            };
        }
    }
}