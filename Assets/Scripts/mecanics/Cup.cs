using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public CupSize cupSize;
    public DrinkName currentDrink;

    public void Fill(DrinkType bebida)
    {
        if (currentDrink == null)
            currentDrink = new DrinkName();

        currentDrink.drinks.Add(bebida);

        Debug.Log($"Vaso {cupSize} lleno con: {bebida}");
    }
}
