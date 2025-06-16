using Unity.VisualScripting;
using UnityEngine;

public class MoveDrinks : MonoBehaviour
{
    public bool isDraggingDrink = false;
    private Vector3 _offset;
    private Vector3 _position;
    private float _zCoord;
    public string _currentRequest;
    Beverage drinkType;
    public string drinkName;

    private void Start()
    {
        drinkType = GetComponent<Beverage>();
        _position = transform.position;
    }

    //void OnMouseDown()
    //{
    //    drinkName = drinkType.drinkType.ToString();
    //    isDraggingDrink = true;
    //    _zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
    //    _offset = transform.position - GetMouseWorldPos();
    //    //0.47f
    //}

    void OnMouseDown()
    {
        drinkName = drinkType.drinkType.ToString();
        isDraggingDrink = true;

        // Obtenemos la distancia Z actual al hacer clic
        _zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        // Acercamos un poco la bebida a la cámara (esto es lo que pediste)
        _zCoord -= 0.2f;

        _offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        // Ray ray = Camera.main.ScreenPointToRay(transform.position);


        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, 5f))
        {
            Client client = hit.collider.GetComponent<Client>();
            if (client != null)
            {
                _currentRequest = client.currentRequest.ToString();
                if (drinkName == _currentRequest) client.goodOrder = true;
                else client.badOrder = true;
                _currentRequest = null;
            }
        }

        isDraggingDrink = false;
        transform.position = _position;
        drinkName = null;
    }

    void Update()
    {
        if (isDraggingDrink)
        {
            Vector3 targetPos = GetMouseWorldPos() + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);

        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1)); // Para ver el collider en la escena.
        Debug.DrawRay(transform.position, -transform.forward * 5f, Color.red);

    }
}