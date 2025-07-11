using UnityEngine;
using System.Collections;

public class EMTActivator : MonoBehaviour
{
    [Header("Objeto a activar")]
    [SerializeField] private GameObject targetObject;

    [Header("Animación")]
    [SerializeField] private Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private float growDuration = 0.5f;
    [SerializeField] private float activeTime = 5f;

    private string code = "EMT";
    private int codeIndex = 0;
    private Vector3 originalScale;

    void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            originalScale = targetObject.transform.localScale;
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString.ToUpper();

            if (keyPressed == code[codeIndex].ToString())
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
                codeIndex = 0; // reinicia si se presiona algo incorrecto
            }
        }
    }

    void ActivateObject()
    {
        if (targetObject == null) return;

        StopAllCoroutines();
        StartCoroutine(GrowAndHide());
    }

    IEnumerator GrowAndHide()
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

        targetObject.SetActive(false);
        targetObject.transform.localScale = originalScale;
    }
}
