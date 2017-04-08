using System.Collections;
using Silone.Idle.Api;
using UnityEngine;
using UnityEngine.UI;

public class MainHandler : MonoBehaviour
{
    public PersonHandler[] Persons;
    public Text taskText;

    void Start()
    {
        StartCoroutine(RepeatingFunction());
    }

    IEnumerator RepeatingFunction()
    {
        while (true)
        {
            StartCoroutine(RequestData());

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator RequestData()
    {
        var www = new WWW("http://localhost:1085/api/unit");
        yield return www;
        var data = www.text;
        var unit = JsonUtility.FromJson<DtoUnit>(data);

        for (var i = 0; i < unit.Persons.Length; i++)
        {
            Persons[i].UpdateData(unit.Persons[i]);
        }

        if (unit.GameTask != null)
        {
            taskText.text = unit.GameTask.Title + " " + unit.GameTask.Progress + "/" + unit.GameTask.Value;
            if (unit.GameTask.State == GameTaskState.Done)
            {
                taskText.text += " - Done";
            }
            else if (unit.GameTask.State == GameTaskState.Fail)
            {
                taskText.text += " - Fail";
            }
        }
        else
        {
            taskText.text = string.Empty;
        }
    }
}
