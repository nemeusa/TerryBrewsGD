using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoad : MonoBehaviour
{
    public GameObject _loadElement;
    public Slider _progress;
    public void SceneLoader(string nombreEscena)
    {
        StartCoroutine(AsyncCharge(nombreEscena));
    }

    IEnumerator AsyncCharge(string nombreEscena)
    {
        _loadElement.SetActive(true); 

        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);

        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f);
            _progress.value = progreso;
            yield return null;
        }
    }
}
