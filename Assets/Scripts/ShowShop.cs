using UnityEngine;
using UnityEngine.EventSystems;

public class ShowShop : MonoBehaviour
{
    public GameObject objetoA;
    public GameObject objetoB;
    public GameObject objetoBExtra; // Nuevo objeto complementario

    SoundEfects soundEfects;

    [SerializeField] LayerMask layerMask; 

    void Start()
    {
        soundEfects = GetComponent<SoundEfects>();
        objetoA.SetActive(true);
        objetoB.SetActive(false);
        objetoBExtra.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // No hacer nada si el clic fue sobre UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            DetectarClic();
        }
    }

    void DetectarClic()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            GameObject clickeado = hit.collider.gameObject;

            if (clickeado == objetoA && objetoA.activeSelf)
            {
                objetoA.SetActive(false);
                objetoB.SetActive(true);
                objetoBExtra.SetActive(true);
            }
            else if ((clickeado == objetoB || clickeado == objetoBExtra) && objetoB.activeSelf)
            {
                objetoB.SetActive(false);
                objetoBExtra.SetActive(false);
                objetoA.SetActive(true);
            }
            soundEfects.PlaySoundFromGroup(0);
        }
    }
}
