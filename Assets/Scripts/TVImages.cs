using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVImages : MonoBehaviour
{
    [Header("Im�genes")]
    [SerializeField] private Texture[] tvScreens;

    [Header("Componentes")]
    [SerializeField] private MeshRenderer screenRenderer;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip switchSound;

    private int currentIndex = 0;

    void Start()
    {
        if (screenRenderer == null)
            screenRenderer = GetComponent<MeshRenderer>();

        if (tvScreens.Length > 0 && screenRenderer != null)
            screenRenderer.material.mainTexture = tvScreens[currentIndex];
    }

    void OnMouseDown() 
    {
        if (tvScreens.Length == 0 || screenRenderer == null) return;

        currentIndex = (currentIndex + 1) % tvScreens.Length;
        screenRenderer.material.mainTexture = tvScreens[currentIndex];

        if (audioSource != null && switchSound != null)
        {
            audioSource.PlayOneShot(switchSound);
        }
    }
}
