using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalksThemes : MonoBehaviour
{
    public string currentClima;
    public string currentEventos;
    public string[] currentTheme;
    [SerializeField] TMP_Text textTheme;
    public int _indexTheme;
    [SerializeField] private TVManager tvManagerVideo;
    public GameObject _boton;
    

    [Header("Canales Extras")]
    [SerializeField] private TVManager tvManagerVideos;

    public Dictionary<ThemeType, string> currentThemes = new();

    bool _newChanel;
    private Color originalColor;


    private void Start()
    {
        InitialTheme();
        RandomTheme();

        originalColor = textTheme.color;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            CambiarCanal();
    }

    public void RandomTheme()
    {
        currentTheme = new string[] { currentThemes[ThemeType.Clima], currentThemes[ThemeType.Evento] };

        string charla = "";

        if (_indexTheme < currentTheme.Length)
        {
            charla = currentTheme[_indexTheme];
            textTheme.text = charla;
        }
        else
        {
            textTheme.text = "canal random";
        }

        tvManagerVideo?.UpdateTVVideo();

        _indexTheme++;
        if (tvManagerVideo != null)
        {
            int totalTemas = currentTheme.Length + tvManagerVideo.CanalesFalsosLength;

            if (_indexTheme >= totalTemas)
                _indexTheme = 0;

        }
    }


    void InitialTheme()
    {
        string[] clima = { "Frio", "Calor" };
        string[] eventos = { "Trafico", "Carretera_Libre" };

        //currentClima = clima[UnityEngine.Random.Range(0, clima.Length)];

        //currentEventos = eventos[UnityEngine.Random.Range(0, eventos.Length)];

        currentThemes[ThemeType.Clima] = clima[Random.Range(0, clima.Length)];
        currentThemes[ThemeType.Evento] = eventos[Random.Range(0, eventos.Length)];
    }


    IEnumerator CambiaCanal()
    {
        _newChanel = true;
        yield return new WaitForSeconds(2);
        RandomTheme();
        _newChanel = false;
    }

    void CambiarCanal()
    {
        _newChanel = true;
        RandomTheme();
        _newChanel = false;
    }

    //public string GetCurrentThemeSafe()
    //{
    //    if (_indexTheme >= currentTheme.Length || _indexTheme < 0)
    //        return currentTheme[0];
    //    else
    //        return currentTheme[_indexTheme];
    //}
    //public void ChangeColorToRed()
    //{
    //    StartCoroutine(ChangeColorCoroutine());
    //}

    private IEnumerator ChangeColorCoroutine()
    {
        _boton.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        _boton.GetComponent<MeshRenderer>().material.color = originalColor;

    }
}

public enum ThemeType
{
    Clima,
    Evento
}

