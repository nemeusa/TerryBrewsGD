using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    [Header("Diálogos por tema")]
    public ThemeDialogue[] themeDialogues;

    [HideInInspector] public string currentDialogue;
    [HideInInspector] public string Theme;

    [SerializeField] Client _client;

    bool useTheme;
    TalkTheme themeToUse;
    ThemeType themeTypeToUse;

    [SerializeField] int _probabilityTheme = 50;
    BarManager _barManeger;
    //TMP_Text textCharla;

    Color _currentColor;

    private void Awake()
    {
        _client = GetComponent<Client>();
        //textCharla = _client.textCharla;
        //_currentColor = textCharla.color;
    }

    public void CharlaThemes()
    {
       useTheme = Random.Range(0, 101) < _probabilityTheme;

        string frase;

        if (useTheme)
        {
            PrepareDialogue();

            frase = GetPhraseByTheme(themeToUse);
            Theme = themeToUse.ToString();

            Debug.Log($" Cliente habló sobre: {Theme} | Tipo: {themeTypeToUse} | Impostor: {_client.imposter}");
        }
        else
        {
            frase = Charla(); // diálogo neutral
        }

        currentDialogue = frase;
    }

    void PrepareDialogue()
    {
        // Elegir un tipo de tema (Clima, Evento, etc.)
        var tipos = System.Enum.GetValues(typeof(ThemeType)).Cast<ThemeType>().ToList();
        themeTypeToUse = tipos[Random.Range(0, tipos.Count)];

        // Obtener el tema correcto activo en la partida según ese tipo
        string temaCorrecto = _client.player._talkTheme.currentThemes[themeTypeToUse];

        if (_client.imposter)
        {
            // Elegir un tema distinto del correcto dentro del mismo tipo
            var posibles = themeDialogues
                .Where(t => t.type == themeTypeToUse && t.theme.ToString() != temaCorrecto)
                .ToList();

            if (posibles.Count == 0)
            {
                Debug.LogWarning("No hay temas incorrectos disponibles, se usará el correcto");
                themeToUse = (TalkTheme)System.Enum.Parse(typeof(TalkTheme), temaCorrecto);
            }
            else
            {
                themeToUse = posibles[Random.Range(0, posibles.Count)].theme;
            }
        }
        else
        {
            // Cliente bueno dice el tema correcto
            themeToUse = (TalkTheme)System.Enum.Parse(typeof(TalkTheme), temaCorrecto);
        }
    }

    public void Verification()
    {
        string temaCorrecto = _client.player._talkTheme.currentThemes[themeTypeToUse];

        if (!useTheme) return;

        if (_client.imposter && themeToUse.ToString() == temaCorrecto)
        {
            Debug.Log(" Impostor acertó el tema (falló la mentira)");
            CharlaThemes();
        }

        else if (!_client.imposter && themeToUse.ToString() != temaCorrecto)
        {
            Debug.Log(" Cliente bueno se equivocó de tema");
            CharlaThemes();
        }
    }

    private string GetPhraseByTheme(TalkTheme theme)
    {
        foreach (var entry in themeDialogues)
        {
            if (entry.theme == theme)
            {
                if (entry.phrases.Length == 0) return "[sin frases]";
                return entry.phrases[Random.Range(0, entry.phrases.Length)];
            }
        }

        return "[tema no encontrado]";
    }

    public string Charla()
    {
        if(_barManeger == null) _barManeger = _client._barManeger;

        string charla;

        if (!_barManeger.tutorial)
        {


            if (!_client.imposter)
            {



                charla = _client.charlaGood[UnityEngine.Random.Range(0, _client.charlaGood.Length)];


            }

            else
            {
                charla = _client.charlaBad[UnityEngine.Random.Range(0, _client.charlaBad.Length)];

            }
        }
        else
        {

            if (!_client.imposter)
            {
                charla = _client.charlaGood[_barManeger.indexGood];
                _barManeger.indexGood = Mathf.Min(_barManeger.indexGood + 1, _client.charlaGood.Length - 1); // No pasar el límite


            }

            else
            {
                charla = _client.charlaBad[_barManeger.indexBad];
                _barManeger.indexBad = Mathf.Min(_barManeger.indexBad + 1, _client.charlaBad.Length - 1);

            }
        }

        return charla;
    }




}


[System.Serializable]
public class ThemeDialogue
{
    public TalkTheme theme;
    public ThemeType type;
    [TextArea(2, 5)] public string[] phrases;
}

public enum TalkTheme
{
    Frio,
    Calor,
    Trafico,
    Carretera_Libre
}