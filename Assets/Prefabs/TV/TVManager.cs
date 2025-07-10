using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class TVManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TalksThemes talksThemes;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Renderer tvRenderer; // El mesh renderer del cubo

    [Header("Videos de Clima")]
    [SerializeField] private VideoClip clipCalor;
    [SerializeField] private VideoClip clipFrio;

    [Header("Videos de Evento")]
    [SerializeField] private VideoClip clipTrafico;
    [SerializeField] private VideoClip clipCarreteraLibre;

    [Header("Fade")]
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Sonido")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip staticClip;

    [Header("Canales extra")]
    [SerializeField] private VideoClip[] canalesFalsos;
    public int CanalesFalsosLength => canalesFalsos.Length;

    private Material _material;

    private void Awake()
    {
        talksThemes.RandomTheme();
        if (tvRenderer != null)
        {
            _material = tvRenderer.material;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            talksThemes.CambiarCanal();
    }

    public void RandomChanel()
    {
        if(talksThemes._boton.GetComponent<ActivarTVManagerConQ>().yaActivado)
        talksThemes.CambiarCanal();
    }

    public void UpdateTVVideo()
    {
        VideoClip newClip = null;
        int climaIndex = 0;
        int eventoIndex = 1;

        if (talksThemes._indexTheme == climaIndex)
        {
            if (talksThemes.currentThemes[ThemeType.Clima] == "Frio") newClip = clipFrio;
            else if (talksThemes.currentThemes[ThemeType.Clima] == "Calor") newClip = clipCalor;
        }
        else if (talksThemes._indexTheme == eventoIndex)
        {
            if (talksThemes.currentThemes[ThemeType.Evento] == "Trafico") newClip = clipTrafico;
            else if (talksThemes.currentThemes[ThemeType.Evento] == "Carretera_Libre") newClip = clipCarreteraLibre;
        }
        else
        {
            int falseIndex = talksThemes._indexTheme - 2;
            if (falseIndex >= 0 && falseIndex < canalesFalsos.Length)
                newClip = canalesFalsos[falseIndex];
        }
        if (newClip != null)
            StartCoroutine(ChangeChannelWithFade(newClip));
    }


    private IEnumerator ChangeChannelWithFade(VideoClip newClip)
    {
        // 1. Fade Out
        if (_material != null)
            yield return StartCoroutine(FadeTo(0f));

        // 2. Sonido de cambio
        if (audioSource != null && staticClip != null)
            audioSource.PlayOneShot(staticClip);

        // 3. Cambiar el video
        videoPlayer.clip = newClip;
        videoPlayer.Play();

        // 4. Fade In
        if (_material != null)
            yield return StartCoroutine(FadeTo(1f));
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        if (_material == null || !_material.HasProperty("_Color"))
            yield break;

        Color startColor = _material.color;
        float startAlpha = startColor.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            _material.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        _material.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }
}
