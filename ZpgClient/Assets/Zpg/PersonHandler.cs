using Silone.Idle.Api;
using UnityEngine;
using UnityEngine.UI;

public class PersonHandler : MonoBehaviour
{
    public Text NameText;
    public Text HpText;
    public Text DmgText;

    public void UpdateData(DtoPerson person)
    {
        NameText.text = person.ShortName;
        HpText.text = "HP: " + person.HP + "/" + person.MaxHP;
        DmgText.text = "Dmg: " + (int)(person.Damage * 0.5f) + "-" + (int)(person.Damage * 1.5f);
    }
}
