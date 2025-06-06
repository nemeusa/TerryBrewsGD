using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject _tutoBro;
    [SerializeField] string[] _tips;
    [SerializeField] TMP_Text _textTips;
    [SerializeField] int _indexTips;

    [SerializeField] GameObject flechitasDrinks;
    [SerializeField] GameObject flechitaPump;


    [SerializeField] Player _playerScript;
    [SerializeField] Pump _pumpScript;

    public bool _ElTuto;

    [SerializeField] GameObject clientPrefab, imposterPrefab;
    GameObject _currentClientPrefab;
    public List<Chair> allChairs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform _altureChair;

    [SerializeField] Player _player;
    Client _client;

    //public GameManager gameManager;

    //private string currentRequest;
    //public TMP_Text requestText;



    void Start()
    {
        //StartCoroutine(SpawnRoutine());
        tutoBro();
        _currentClientPrefab = clientPrefab;
        //NuevaPeticion();
    }

    void Update()
    {
        if (_playerScript._score < 200)
        {
            flechitasDrinks.SetActive(true);
        }

        if (_playerScript._score == 100)
        {
            _tutoBro.SetActive(false);
            TrySpawnClient();
            _ElTuto = true;
        }

        if (_playerScript._score == 200)
        {
            flechitasDrinks.SetActive(false) ;
            flechitaPump.SetActive(true) ;

            if (_ElTuto)
            {
                tutoBro();
                _ElTuto = false;
            }
            if (Input.GetButtonDown("Jump")) _tutoBro.SetActive(false);
            //{

            _currentClientPrefab = imposterPrefab;
                TrySpawnClient();
            //}
        }

        if (_playerScript._score == 250)
        {
            //if (_client != null)
            //{
            //    _client.LeaveChair();
            //    _client.InstantDestroy();
            //}
            TrySpawnClient();
            _ElTuto = true;
        }

        if (_playerScript._score == 300)
        {
            if (_ElTuto)
            {
                tutoBro();
                _ElTuto = false;

            }

            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("GameDay1");
            }
            //activar silla 1
        }

        if (_playerScript._score >= 600)
        {
            //activar silla 2
        }

        if (Input.GetButtonDown("Jump"))
        {
            _tutoBro.SetActive(false);
            TrySpawnClient();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            tutoBro();
        }

        
    }

    public void TrySpawnClient()
    {
        Chair freeChair = allChairs.FirstOrDefault(c => !c.isOcupped);
        //Vector3 spawn = new Vector3(spawnPoint.position.x * _randomEnter, _altureChair.position.y, spawnPoint.position.z);
        Vector3 spawn = new Vector3(spawnPoint.position.x, _altureChair.position.y, spawnPoint.position.z);

        if (freeChair != null)
        {
            GameObject clientObj = Instantiate(_currentClientPrefab, spawn, Quaternion.identity);
            Client client = clientObj.GetComponent<Client>();
            _client = client;
            //gameManager.client = client;
            //NuevaPeticion();
            //client.GetComponent<NPCRequest>().requestedItem = currentRequest;
            client.player = _player;
            client.AssignChair(freeChair);
        }
        else
        {
            Debug.Log("No hay sillas libres, no spawnea el cliente.");
        }
    }
    void tutoBro()
    {
        //_client.InstantDestroy(); 
        // if (_client != null) Destroy( _client );
        if (_client != null)
        {
            _client.LeaveChair();
            _client.InstantDestroy();
        }
        Debug.Log("aparezco");
        _tutoBro.SetActive(true);
        string charla;

        charla = _tips[_indexTips];
        _textTips.text = charla;
        _indexTips++;
    }


    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            TrySpawnClient();
        }
    }
}
