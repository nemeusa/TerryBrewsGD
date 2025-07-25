using UnityEngine;
using UnityEngine.EventSystems;

public class ShowShop : MonoBehaviour
{
    public GameObject _handShop;
    public GameObject _barShop;
    public MeshRenderer _barShopMesh;
    public GameObject _canvasShop; // Nuevo objeto complementario

    SoundEfects soundEfects;

    [SerializeField] LayerMask layerMask;

    GameObject clickea2;

    public bool useShop;

    bool des;

    void Start()
    {
        useShop = false;
        soundEfects = GetComponent<SoundEfects>();
        _barShop.SetActive(true);
        _handShop.SetActive(false);
        _canvasShop.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // No hacer nada si el clic fue sobre UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            //DetectarClic();
            UseShop();
        }
    }

    //void DetectarClic()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
    //    {
    //        GameObject clickeado = hit.collider.gameObject;

    //        if (clickeado == objetoA && objetoA.activeSelf)
    //        {
    //            objetoA.SetActive(false);
    //            objetoB.SetActive(true);
    //            objetoBExtra.SetActive(true);
    //        }
    //        else if ((clickeado == objetoB || clickeado == objetoBExtra) && objetoB.activeSelf)
    //        {
    //            objetoB.SetActive(false);
    //            objetoBExtra.SetActive(false);
    //            objetoA.SetActive(true);
    //        }
    //        soundEfects.PlaySoundFromGroup(0);
    //    }
    //}

    void UseShop()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (_barShopMesh.enabled)
        des = Physics.Raycast(ray, out RaycastHit hit2, 100f, layerMask);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            GameObject clickeado = hit.collider.gameObject;

            if (clickeado == _barShop && _barShopMesh.enabled)
            {
                useShop = true;
                _barShopMesh.enabled = false;
                _handShop.SetActive(true);
                _canvasShop.SetActive(true);
                _barShop.GetComponent<Collider>().enabled = false;
            }
            soundEfects.PlaySoundFromGroup(0);
        }
        if (!des)
            if (!_barShopMesh.enabled)
            {
                useShop = false;
                _barShopMesh.enabled = true;
                _handShop.SetActive(false);
                _canvasShop.SetActive(false);
                soundEfects.PlaySoundFromGroup(0);
                _barShop.GetComponent<Collider>().enabled = true;
            }
    }


}
