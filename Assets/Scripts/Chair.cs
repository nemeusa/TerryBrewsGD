using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public bool isOcupped{ get; private set; }
    public Transform seatPosition;

    public void Ocuppy()
    {
        isOcupped = true;
    } 
    public void Free()
    {
        isOcupped = false;
    }
}
