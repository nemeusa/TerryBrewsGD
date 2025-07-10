using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEfects : MonoBehaviour
{
    [SerializeField] private List<SoundGroup> soundGroups = new List<SoundGroup>();
    private AudioSource _myAudioSource;

    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    // Llamás este método con el índice del grupo que querés reproducir
    public void PlaySoundFromGroup(int groupIndex)
    {
        if (groupIndex < 0 || groupIndex >= soundGroups.Count) return;

        AudioClip[] clips = soundGroups[groupIndex].clips;

        if (clips.Length == 0) return;

        int soundIndex = Random.Range(0, clips.Length);
        _myAudioSource.PlayOneShot(clips[soundIndex]);
    }
}

[System.Serializable]
public class SoundGroup
{
    public string groupName;
    public AudioClip[] clips;
}

