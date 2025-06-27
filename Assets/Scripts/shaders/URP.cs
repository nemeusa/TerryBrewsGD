using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class URP : MonoBehaviour
{
    [Header("Renderer Features")]
    public ScriptableRendererFeature _shootURP;
    public ScriptableRendererFeature _damageURP;

    private void Awake()
    {   
        _shootURP.SetActive(false);
        _damageURP.SetActive(false);
    }

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

        yield return new WaitForSeconds(0.6f);

        _damageURP.SetActive(false);
    }
    void URPOff() 
    {
        _shootURP.SetActive(false);
        _damageURP.SetActive(false);
    }
}
