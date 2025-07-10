using UnityEngine;

public class ClickCounterActivator : MonoBehaviour
{
    [Tooltip("N�mero de clics necesarios para activar el objeto")]
    public int clicksToActivate = 3;

    [Tooltip("El objeto que se activar� y empezar� a orbitar")]
    public GameObject orbitingObject;

    private int currentClicks = 0;

    void OnMouseDown()
    {
        currentClicks++;

        if (currentClicks >= clicksToActivate && orbitingObject != null)
        {
            orbitingObject.SetActive(true);
        }
    }
}
