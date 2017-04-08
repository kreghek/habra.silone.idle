using System;

namespace Silone.Idle.Api
{
    [Serializable]
    public class DtoUnit
    {
        public string Sid;
        public int CompleteCount;
        public DtoGameTask GameTask;
        public DtoPerson[] Persons;
    }
}