using UnityEngine;

public class AudioClick : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
