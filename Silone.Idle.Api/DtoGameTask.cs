using System;

namespace Silone.Idle.Api
{
    [Serializable]
    public class DtoGameTask
    {
        public string Sid;
        public string Title;
        public float Progress;
        public float Value;
        public string Reward;
        public GameTaskState State;   
    }

    public enum GameTaskState
    {
        WIP,
        Done,
        Fail
    }
}