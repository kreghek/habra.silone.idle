using Silone.Idle.Api;

namespace Silone.Idle.Data
{
    public class Person
    {
        public string Sid { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Class { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }

        public bool IsDefeated => HP <= 0;

        public DtoPerson GetDto()
        {
            return new DtoPerson
            {
                Sid = Sid,
                FullName = FullName,
                ShortName = ShortName,
                HP = HP,
                MaxHP = MaxHP,
                Damage = Damage
            };
        }
    }
}
