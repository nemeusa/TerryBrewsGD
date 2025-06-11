using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class TalksTeme : MonoBehaviour
{
    Talks _talks;
    public string currentClima;
    public string currentEventos;
    [SerializeField] TMP_Text textEvent;
    private void Update()
    {
        TalkTheme();
    }

    void TalkTheme()
    {
        if(_talks == Talks.Frio)
        {
            Debug.Log("hace frio");
        }

        if (_talks == Talks.Calor) 
        {
            Debug.Log("hace calor");
        }

        if (_talks == Talks.Transito)
        {
            Debug.Log("no hay transito");
        }

        if (_talks == Talks.Despejado)
        {
            Debug.Log("esta despejado");
        }
    }

    void NewEvent()
    {


        string[] clima = { "Frio", "Calor"};
        string[] eventos = {"Trafico", "Despejado" };

        currentClima = clima[UnityEngine.Random.Range(0, clima.Length)];
        currentEventos = eventos[UnityEngine.Random.Range(0, eventos.Length)];

    }
}

public enum Talks
{
    Frio,
    Calor,
    Transito,
    Despejado
}
