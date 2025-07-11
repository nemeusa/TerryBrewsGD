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
            if (clientsCounter >= _goodClients) client.randomBlock = true;
            if (clientsCounter == _goodClients)
            {
                client.randomBlock = true;
                client.imposter = true;
            }
            _player._client = client;
            client.player = _player;
            client._barManeger = this;
            client.AssignChair(freeChair);
        }
        else
        {
            //Debug.Log("No hay sillas libres, no spawnea el cliente.");
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
