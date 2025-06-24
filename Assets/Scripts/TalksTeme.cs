using TMPro;
using UnityEngine;

public class TalksTeme : MonoBehaviour
{
    Talks _talks;
    public string currentClima;
    public string currentEventos;
    public string[] currentTheme;
    [SerializeField] TMP_Text textTheme;
    public int _indexTheme;

    private void Start()
    {
        InitialTheme();
        RandomTheme();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) RandomTheme();
        //TalkTheme();
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

    void RandomTheme()
    {
        string charla;

        currentTheme = new string[] { currentClima, currentEventos } ;
        charla = currentTheme[_indexTheme];

        textTheme.text = charla;
        _indexTheme++;
        if (_indexTheme >= 2) _indexTheme = 0;
    }

    void InitialTheme()
    {
        string[] clima = { "Frio", "Calor" };
        string[] eventos = { "Trafico", "Despejado" };

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
