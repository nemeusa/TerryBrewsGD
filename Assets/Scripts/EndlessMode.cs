using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndlessMode : MonoBehaviour
{
    [SerializeField] TMP_Text recordText;

    private void Start()
    {
        if (recordText != null)
        recordText.text = $"Record: {GameStats.recordEndless}";
    }
    public void AddTotalCash(int cash)
    {
        if (GameStats.recordEndless < cash)
        GameStats.recordEndless = cash;
    }
}
