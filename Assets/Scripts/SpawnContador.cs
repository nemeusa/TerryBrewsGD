using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnContador : MonoBehaviour
{
    public GameObject moneyTextPrefab; // arrastrá tu prefab acá
    public Transform spawnPoint; // punto donde aparecerá el texto (puede ser encima de la caja)
    [SerializeField] Player player;

    public SoundEfects soundEfects;

    private void Start()
    {
        soundEfects = GetComponent<SoundEfects>();

    }

    public void MostrarGanancia(float cantidad)
    {
        soundEfects.PlaySoundFromGroup(0);
        GameObject texto = Instantiate(moneyTextPrefab, spawnPoint.position, Quaternion.identity);
        texto.transform.SetParent(null); // importante si el prefab estaba ligado al canvas en World Space

        UnityEngine.Color color = UnityEngine.Color.green;
        string textoDinero = "+ $" + cantidad.ToString("F2");
        texto.GetComponent<ContadorEfect>().SetText(textoDinero, color);
    }

    public void DescontarGanancia(float cantidad)
    {
        soundEfects.PlaySoundFromGroup(1);
        GameObject texto = Instantiate(moneyTextPrefab, spawnPoint.position, Quaternion.identity);
       // texto.transform.SetParent(null); // importante si el prefab estaba ligado al canvas en World Space

        UnityEngine.Color color = UnityEngine.Color.red;

        if (player._score <= 0) cantidad = 0;

        string textoDinero = "- $" + cantidad.ToString("F2");
        texto.GetComponent<ContadorEfect>().SetText(textoDinero, color);
    }
}
