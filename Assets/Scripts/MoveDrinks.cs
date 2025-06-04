using UnityEngine;
using UnityEngine.EventSystems;

public class MoveDrinks : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Rigidbody _objectRb;
    [SerializeField] private float _lerpSpeed = 5f;
    private bool _isDragging = false;

    private Vector3 _worldPosition;

    private void Awake()
    {
        _objectRb = GetComponent<Rigidbody>();
        if (_objectRb == null)
        {
            Debug.LogError("No Rigidbody found on the object. Adding Rigidbody...");
            _objectRb = gameObject.AddComponent<Rigidbody>();
            _objectRb.freezeRotation = true;
        }
    }

    private void Start()
    {
        // Verificar si el objeto tiene un Collider.
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("No Collider found on the object! Please add a Collider.");
        }

        _objectRb.useGravity = false;
        _objectRb.isKinematic = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown triggered");
        _isDragging = true;
        _objectRb.useGravity = false;
        _objectRb.isKinematic = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            Debug.Log("OnDrag triggered");
            // Convertir la posición del ratón a coordenadas del mundo.
            float z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 pointPosition = new Vector3(eventData.position.x, eventData.position.y, z);
            _worldPosition = Camera.main.ScreenToWorldPoint(pointPosition);

            // Mover el objeto de forma suave.
            Vector3 newPosition = Vector3.Lerp(transform.position, _worldPosition, Time.deltaTime * _lerpSpeed);
            _objectRb.MovePosition(newPosition);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp triggered");
        _isDragging = false;
        _objectRb.useGravity = true;
        _objectRb.isKinematic = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1)); // Para ver el collider en la escena.
    }
}