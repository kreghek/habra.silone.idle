using System.Threading;
using System.Threading.Tasks;
using Silone.Idle.Data.Services;

namespace Silone.Idle.Web
{
    public static class MainLoop
    {
        public static void Start()
        {
            Task.Run(() =>
            {
                var dice = new Dice();
                var worldManager = new WorldManager(dice);

                while (true)
                {
                    var units = PersonService.Units;
                    worldManager.Process(units);

                    Thread.Sleep(5000);
                }
            });
        }
    }
}