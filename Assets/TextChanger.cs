using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cambio;
    private void Start()
    {
        if (_cambio != null)
            StartCoroutine(CambiarTexto());
    }

    private IEnumerator CambiarTexto()
    {
        while (true)
        {
            _cambio.text = "     No signal.\r\nPress Q to retry";
            yield return new WaitForSeconds(1f);

            _cambio.text = "     No signal.\r\nPress CH to retry";
            yield return new WaitForSeconds(1f);
        }
    }
}
