using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderOutline : MonoBehaviour
{
    public GameObject outlineMesh;
    void Awake()
    {
        outlineMesh.SetActive(false);
    }

    public void EnableOutline(bool value)
    {
        outlineMesh.SetActive(value);
    }
}
