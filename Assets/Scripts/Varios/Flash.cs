using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public Volume volumenPost;
    public bool volumeActive;
    Player _player;

    public IEnumerator PostActive()
    {
        volumeActive = true;
        if (volumenPost != null) volumenPost.enabled = volumeActive;
        //yield return new WaitForSeconds (0.5f);
        yield return new WaitForSeconds (0.5f);
        volumeActive = false;
        if (volumenPost != null) volumenPost.enabled = volumeActive;

    }
}
