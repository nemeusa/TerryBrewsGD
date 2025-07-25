using System.Collections;
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
    [SerializeField] LayerMask _beverageLayer;
    [SerializeField] float _agarre;

    [SerializeField] Transform alturaEntrega;



    private float _contador = 0f;
    private bool _contando = false;

    private void Start()
    {
        drinkType = GetComponent<Beverage>();
        _position = transform.position;
    }

    void OnMouseDown()
    {

        _contando = true;

        drinkName = drinkType.drinkType.ToString();
        isDraggingDrink = true;

        _zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        _zCoord -= 0.2f;

        _offset = transform.position - GetMouseWorldPos();


    }

    void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _beverageLayer))
        {
            Client client = hit.collider.GetComponent<Client>();
            float alturaMinimaEntrega = 3; // Ajust� este valor a la altura real de tu barra

            if (hit.point.y >= alturaMinimaEntrega)
            {
                if (client != null)
                {
                    if (_contador >= 0.15f)
                    {
                        _currentRequest = client.currentRequest.ToString();
                        if (drinkName == _currentRequest) client.goodOrder = true;
                        else client.badOrder = true;
                        _currentRequest = null;
                    }
                }
            }
        }

        isDraggingDrink = false;
        transform.position = _position;
        drinkName = null;

        _contando = false;
        _contador = 0f;

        Vector3 dir = _position - transform.position;

        transform.position += dir * 2 * Time.deltaTime;
    }

    void Update()
    {
        if (isDraggingDrink)
        {
            Vector3 targetPos = GetMouseWorldPos() + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _agarre);

        }

        if (_contando)
        {
            _contador += Time.deltaTime;
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
       
        Debug.DrawRay(transform.position, -transform.forward * 5f, Color.red);

    }
}