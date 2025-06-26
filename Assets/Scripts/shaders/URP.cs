using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class URP : MonoBehaviour
{
    [Header("Renderer Features")]
    [SerializeField] private ScriptableRendererFeature _shootURP;
    [SerializeField] private ScriptableRendererFeature _damageURP;

    public IEnumerator ShootURP()
    {

        _shootURP.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        Debug.Log("shoot urp desactivado");

        _shootURP.SetActive(false);
    }

    public IEnumerator damageURP()
    {

        _damageURP.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        _damageURP.SetActive(false);
    }
}
