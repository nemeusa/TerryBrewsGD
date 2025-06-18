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

    private float _contador = 0f;
    private bool _contando = false;

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
            StartCoroutine(ScaleAnimation());
            Vector3 targetPos = GetMouseWorldPos() + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _agarre);

        }

        if (_contando)
        {
            _contador += Time.deltaTime;
        }    
    }

    IEnumerator ScaleAnimation()
    {
        //yield return new WaitForSeconds(0.6f);
        //Vector3 scale = transform.localScale;
        //transform.localScale -= scale * 0.6f * Time.deltaTime;
        //Debug.Log("chitito");
        //yield return new WaitForSeconds(0.6f);
        //transform.localScale += scale * 0.6f * Time.deltaTime;
        //Debug.Log("gramde");

        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0f;
        float scaleDuration = 0.6f; // El tiempo que tarda en cambiar la escala.

        // Reduce el tamaño
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * 0.6f, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale * 0.6f; // Asegúrate de que se haya alcanzado el tamaño reducido.

        // Espera antes de aumentar el tamaño
        yield return new WaitForSeconds(0.6f);

        elapsedTime = 0f;

        // Aumenta el tamaño de vuelta al original
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale * 0.6f, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale; // Asegúrate de que se haya alcanzado el tamaño original.

        Debug.Log("Latido completado");
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