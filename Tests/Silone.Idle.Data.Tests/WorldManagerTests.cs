using System.Linq;
using Moq;
using NUnit.Framework;
using Silone.Idle.Data.Services;

namespace Silone.Idle.Data.Tests
{
    [TestFixture]
    public class WorldManagerTests
    {
        [Test]
        public void Process_GenerateNewStoryTask_NewTaskAdded()
        {
            // ARRANGE
            var person = new Person()
            {
                HP = 100,
                MaxHP = 100
            };
            var unit = new Unit(new[] { person });
            var dice = new Dice();
            var worldManager = new WorldManager(dice);


            // ACT
            worldManager.Process(new[] { unit });



            // ASSERT
            Assert.AreEqual(1, unit.Tasks.Count());
            Assert.AreEqual(true, unit.Tasks.First().Storyline);
        }

        [Test]
        public void Process_GenerateCombatTask_CombatInitialized()
        {
            // ARRANGE
            var person = new Person()
            {
                HP = 100,
                MaxHP = 100
            };
            var unit = new Unit(new[] { person });
            unit.Tasks.Add(new StoryGameTask { Value = 100, Title = "task" });
            var diceMock = new Mock<IDice>();
            diceMock.Setup(x => x.TestCombat()).Returns(true);
            diceMock.Setup(x => x.RollMobGroupSize()).Returns(1);
            diceMock.Setup(x => x.RollMobStates(It.IsAny<Mob>()))
                .Callback((Mob mob)=> {
                    mob.MaxHP = 1;
                });
            var worldManager = new WorldManager(diceMock.Object);



            // ACT
            worldManager.Process(new[] { unit });



            // ASSERT
            Assert.IsInstanceOf(typeof(CombatGameTask), unit.Tasks.First());
            Assert.Greater(unit.Tasks.First().Value, 0);
        }

        [Test]
        public void Process_MobsHit_FirstPersonReceivedAllDamage()
        {
            // ARRANGE
            var person = new Person()
            {
                HP = 100,
                MaxHP = 100
            };
            var unit = new Unit(new[] { person });
            unit.Tasks.Add(new CombatGameTask(new[] { new Mob { MaxHP = 1, HP = 1, Damage = 1 } }) { Title = "combat" });
            var diceMock = new Mock<IDice>();
            diceMock.Setup(x => x.TestHit(It.IsAny<Person>())).Returns(false);
            diceMock.Setup(x => x.TestHit(It.IsAny<Mob>())).Returns(true);
            diceMock.Setup(x => x.RollDmg(It.IsAny<int>())).Returns(1);
            var worldManager = new WorldManager(diceMock.Object);



            // ACT
            worldManager.Process(new[] { unit });



            // ASSERT
            Assert.AreEqual(99, person.HP);
        }

        [Test]
        public void Process_PersonHits_FirstMobReceivedAllDamage()
        {
            // ARRANGE
            var person = new Person()
            {
                HP = 100,
                MaxHP = 100,
                Damage = 20
            };

            var mob = new Mob { MaxHP = 100, HP = 100, Damage = 1 };

            var unit = new Unit(new[] { person });
            unit.Tasks.Add(new CombatGameTask(new[] { mob }) { Title = "combat" });
            var diceMock = new Mock<IDice>();
            diceMock.Setup(x => x.TestHit(It.IsAny<Person>())).Returns(true);
            diceMock.Setup(x => x.TestHit(It.IsAny<Mob>())).Returns(false);
            diceMock.Setup(x => x.RollDmg(It.IsAny<int>())).Returns(20);
            var worldManager = new WorldManager(diceMock.Object);



            // ACT
            worldManager.Process(new[] { unit });



            // ASSERT
            Assert.AreEqual(80, mob.HP);
        }
    }
}
