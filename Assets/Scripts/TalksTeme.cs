using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class TalksTeme : MonoBehaviour
{
    Talks _talks;
    public string currentRequest;
    [SerializeField] TMP_Text textEvent;
    private void Update()
    {
        TalkTheme();
    }

    void TalkTheme()
    {
        if(_talks == Talks.frio)
        {
            Debug.Log("hace frio");
        }

        if (_talks == Talks.calor) 
        {
            Debug.Log("hace calor");

        }

        if (_talks == Talks.transito)
        {
            Debug.Log("no hay transito");
        }

        if (_talks == Talks.despejado)
        {
            Debug.Log("esta despejado");
        }
    }

    void NewEvent()
    {


        string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };

        currentRequest = opciones[UnityEngine.Random.Range(0, opciones.Length)];

    }
}

public enum Talks
{
    frio,
    calor,
    transito,
    despejado
}
