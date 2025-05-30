using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIno : MonoBehaviour
{
    public float speed = 3;
    Transform actuallyPosition;
    bool activado;

    void Update()
    {
        Vector3 a = Vector3.up;
        if (Input.GetButtonDown("Jump"))
        {
            activado = true;
            Destroy(gameObject, 3);
        }
        if(activado) transform.position += a * speed * Time.deltaTime;
    }
}
