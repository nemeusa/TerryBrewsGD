using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    //[SerializeField] GameObject _clientPrefab;
    // [SerializeField] GameObject clientGoodPrefab;
    [SerializeField] GameObject[] _currentClientPrefab;
    public List<Chair> allChairs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform spawnPointRight;
    [SerializeField] Transform _altureChair;
    Vector3 _spawn;

    [SerializeField] int _goodClients;
    [SerializeField] int _badClients;

    int clientsCounter;
    private float _randomEnter;

    [SerializeField] Player _player;

    //public GameManager gameManager;

    //private string currentRequest;
    //public TMP_Text requestText;

    [Header("Tutorial")]
    public bool tutorial;
    public int indexGood = 0;
    public int indexBad = 0;
    public int indexBebida = 0;
    [SerializeField] bool _imposterZickZack;
    bool _clientZack;
    [SerializeField] bool _tutoTienda;

    private void Start()
    {
        _randomEnter = Random.Range(0, 2) == 0 ? -1 : 1;
        StartCoroutine(SpawnRoutine());
        //_currentClientPrefab = clientGoodPrefab;
        //NuevaPeticion();
    }

    private void Update()
    {
        //if (clientsCounter >= _goodClients) client.randomBlock = true;
           
    }

    public void TrySpawnClient()
    {
        Chair freeChair = allChairs.FirstOrDefault(c => !c.isOcupped);
        //Vector3 spawn = new Vector3(spawnPoint.position.x * _randomEnter, _altureChair.position.y, spawnPoint.position.z);

        //if(Random.Range(0, 100) <= 50)
        _spawn = new Vector3(spawnPoint.position.x, _altureChair.position.y, spawnPoint.position.z);
        //else
        //_spawn = new Vector3(spawnPointRight.position.x, _altureChair.position.y, spawnPointRight.position.z);

        if (freeChair != null)
        {

            clientsCounter++;
           // Debug.Log(clientsCounter);
            GameObject clientObj = Instantiate(_currentClientPrefab[UnityEngine.Random.Range(0, _currentClientPrefab.Length)], _spawn, Quaternion.identity);
            Client client = clientObj.GetComponent<Client>();
            //if (clientsCounter >= _goodClients) client.randomBlock = true;
            clientOrImposter(client);
            _player._client = client;
            client.player = _player;
            client._barManeger = this;
            client.AssignChair(freeChair);
            client.chair = freeChair;
        }
        else
        {
            //Debug.Log("No hay sillas libres, no spawnea el cliente.");
        }

        if(_tutoTienda) TutorialTienda();
    }

    void clientOrImposter(Client client)
    {
        if (clientsCounter >= _goodClients) client.RandomImposter();
        if (clientsCounter <= _badClients)
        {
            client.imposter = true;
        }

        if (_imposterZickZack)
        {
            _clientZack = !_clientZack;
            if (_clientZack) client.imposter = true;
            else client.imposter = false;
        }
    }

    void TutorialTienda()
    {
        if (_player._score <= 150 && _player._currentAmmo <= 0)
        {
            _player.ReloadOneBullet();
        }
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
