using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource normalMusic;
    public AudioSource scaryMusic;

    public Player _player; // Referencia al script del jugador para obtener la cordura

    public float fadeSpeed = 1f;
    public float corduraUmbral = 70f; // Umbral para activar la música turbia

    void Start()
    {
        normalMusic.Play();
        scaryMusic.Play();
    }

    void Update()
    {
        float t = Mathf.InverseLerp(0, 100, _player.cordura);

        // Volumen de música normal (siempre ajusta)
        float normalTargetVolume = Mathf.Lerp(0.1f, 1f, t);
        normalMusic.volume = Mathf.MoveTowards(normalMusic.volume, normalTargetVolume, Time.deltaTime * fadeSpeed);

        // Si la cordura es menor al umbral, activamos fade de música turbia
        if (_player.cordura < corduraUmbral)
        {
            float scaryTargetVolume;

            if (_player.cordura <= 30f)
            {
                scaryTargetVolume = 1f; // Volumen al máximo
            }
            else
            {
                // Interpolamos suavemente de 0 (cordura = 70) a 1 (cordura = 30)
                scaryTargetVolume = Mathf.InverseLerp(corduraUmbral, 30f, _player.cordura);
            }

            scaryMusic.volume = Mathf.MoveTowards(scaryMusic.volume, scaryTargetVolume, Time.deltaTime * fadeSpeed);
        }
        else
        {
            scaryMusic.volume = Mathf.MoveTowards(scaryMusic.volume, 0f, Time.deltaTime * fadeSpeed);
        }
    }

    public void SetCordura(int newCordura)
    {
        _player.cordura = Mathf.Clamp(newCordura, 0, 100);
    }
}
