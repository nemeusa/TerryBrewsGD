using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    [SerializeField] GameObject clientPrefab;
    [SerializeField] GameObject clientGoodPrefab;
    GameObject _currentClientPrefab;
    public List<Chair> allChairs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform _altureChair;

    int clientsCounter;
    private float _randomEnter;

    [SerializeField] Player _player;

    //public GameManager gameManager;

    //private string currentRequest;
    //public TMP_Text requestText;



    private void Start()
    {
        _randomEnter = Random.Range(0, 2) == 0 ? -1 : 1;
        StartCoroutine(SpawnRoutine());
        _currentClientPrefab = clientGoodPrefab;
        //NuevaPeticion();
    }

    private void Update()
    {
        if (clientsCounter == 3) _currentClientPrefab = clientPrefab;
           
    }

    public void TrySpawnClient()
    {
        Chair freeChair = allChairs.FirstOrDefault(c => !c.isOcupped);
        //Vector3 spawn = new Vector3(spawnPoint.position.x * _randomEnter, _altureChair.position.y, spawnPoint.position.z);
        Vector3 spawn = new Vector3(spawnPoint.position.x, _altureChair.position.y, spawnPoint.position.z);

        if (freeChair != null)
        {

            clientsCounter++;
            Debug.Log(clientsCounter);
            GameObject clientObj = Instantiate(_currentClientPrefab, spawn, Quaternion.identity);
            Client client = clientObj.GetComponent<Client>();
            //gameManager.client = client;
            //NuevaPeticion();
            //client.GetComponent<NPCRequest>().requestedItem = currentRequest;
            _player._client = client;
            client.player = _player;
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
