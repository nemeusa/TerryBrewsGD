using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI impostoresTexto;
    [SerializeField] TextMeshProUGUI ordersTexto;
    [SerializeField] TextMeshProUGUI sanityTexto;

    void Start()
    {
        impostoresTexto.text = "Imposters found: " + GameStats.impostoresEliminados;
        ordersTexto.text = "Successful orders: " + GameStats.ordersCompletadas;
        sanityTexto.text = "Sanity level: " + GameStats.sanity;
    }
}
