using UnityEngine;
using System.Collections;

public class EMTActivator3D : MonoBehaviour
{
    [Header("Objeto 3D")]
    [SerializeField] private GameObject targetObject;
    [SerializeField] private MeshRenderer meshRenderer;

    [Header("Texturas aleatorias")]
    [SerializeField] private Texture[] textures;

    [Header("Animación")]
    [SerializeField] private Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private float growDuration = 0.5f;
    [SerializeField] private float activeTime = 5f;

    [Header("Código secreto")]
    [SerializeField] private string code = "EMT";
    private int codeIndex = 0;

    void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            targetObject.transform.localScale = Vector3.zero;
        }

        if (meshRenderer == null && targetObject != null)
            meshRenderer = targetObject.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            string key = Input.inputString.ToUpper();

            if (key == code[codeIndex].ToString())
            {
                codeIndex++;
                if (codeIndex >= code.Length)
                {
                    codeIndex = 0;
                    ActivateObject();
                }
            }
            else
            {
                codeIndex = 0;
            }
        }
    }

    void ActivateObject()
    {
        // Cambiar textura aleatoria
        if (textures.Length > 0 && meshRenderer != null)
        {
            int index = Random.Range(0, textures.Length);
            meshRenderer.material.mainTexture = textures[index];
        }

        StopAllCoroutines();
        StartCoroutine(GrowAndShrink());
    }

    IEnumerator GrowAndShrink()
    {
        targetObject.transform.localScale = Vector3.zero;
        targetObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / growDuration;
            targetObject.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            yield return null;
        }

        targetObject.transform.localScale = targetScale;

        yield return new WaitForSeconds(activeTime);

        elapsed = 0f;
        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / growDuration;
            targetObject.transform.localScale = Vector3.Lerp(targetScale, Vector3.zero, t);
            yield return null;
        }

        targetObject.transform.localScale = Vector3.zero;
        targetObject.SetActive(false);
    }
}
