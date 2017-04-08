using System;

namespace Silone.Idle.Api
{
    [Serializable]
    public class DtoPerson
    {
        public string Sid;
        public string FullName;
        public string ShortName;
        public int MaxHP;
        public int HP;
        public int Damage;
    }
}