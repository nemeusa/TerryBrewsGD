using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject _chairScript1;
    [SerializeField] GameObject _chairScript2;
    [SerializeField] Player _playerScript;
    [SerializeField] BarManager _barScript;
    [SerializeField] Pump _pumpScript;

    public bool _empiezaElTuto;


    // Start is called before the first frame update
    void Start()
    {
        _chairScript1.SetActive(false);
        _chairScript2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerScript._score >= 149)
        {
            Debug.Log("Aparecen los impostores");
        }

        if (_playerScript._score >= 500)
        {
            _chairScript1.SetActive(true);
        }

        if (_playerScript._score >= 600)
        {
            _chairScript2.SetActive(true);
        }

        if (_empiezaElTuto)
        {
            _barScript.TrySpawnClient();
        }

    }

    
}
