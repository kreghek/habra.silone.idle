using Silone.Idle.Api;
using Silone.Idle.Data.Services;

namespace Silone.Idle.Data
{
    public abstract class GameTask
    {
        public string Sid { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public int Progress { get; set; }
        public abstract bool Storyline { get; }
        public GameTaskState State { get; set; }

        public abstract void Update(IDice dice, Unit unit);

        public void Resolve()
        {
            State = GameTaskState.Done;
        }

        public void Reject()
        {
            State = GameTaskState.Fail;
        }

        public DtoGameTask GetDto()
        {
            return new DtoGameTask
            {
                Sid = Sid,
                Title = Title,
                Progress = Progress,
                Value = Value,
                State = (Api.GameTaskState)State
            };
        }
    }

    public enum GameTaskState
    {
        WIP,
        Done,
        Fail
    }
}
