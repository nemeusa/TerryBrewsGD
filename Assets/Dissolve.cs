using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [Header("Dissolve")]
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private string alphaProperty = "_Cutoff"; // o "_AlphaClipThreshold"
    [SerializeField] private float dissolveDuration = 2f;

    private Material _materialInstance;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        // Clonamos el material para no afectar a otros enemigos
        _materialInstance = Instantiate(dissolveMaterial);
        _renderer.material = _materialInstance;

        // Valor inicial visible
        _materialInstance.SetFloat(alphaProperty, 0f);
    }

    public void TriggerDissolve()
    {
        StartCoroutine(DissolveRoutine());
    }

    private System.Collections.IEnumerator DissolveRoutine()
    {
        float time = 0f;

        while (time < dissolveDuration)
        {
            float t = time / dissolveDuration; // De 0 a 1
            _materialInstance.SetFloat(alphaProperty, t);
            time += Time.deltaTime;
            yield return null;
        }

        _materialInstance.SetFloat(alphaProperty, 1f); // Valor final
        gameObject.SetActive(false); // <<--- Lo desactiva en vez de destruirlo
    }
}
