using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorEfect : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float fadeDuration = 1f;

    CanvasGroup canvasGroup;
    Vector3 moveDirection = Vector3.up;
    float timer;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        transform.position += moveDirection * floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

        if (canvasGroup.alpha <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string moneyText, Color color)
    {
        TMP_Text textComponent = GetComponent<TMP_Text>();
        textComponent.text = moneyText;
        textComponent.color = color;
    }

}
