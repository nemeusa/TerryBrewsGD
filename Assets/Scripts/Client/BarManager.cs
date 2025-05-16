using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    [SerializeField] GameObject clientPrefab;
    public List<Chair> allChairs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform _altureChair;
    private float _randomEnter;

    public GameManager gameManager;

    //private string currentRequest;
    //public TMP_Text requestText;



    private void Start()
    {
        _randomEnter = Random.Range(0, 2) == 0 ? -1 : 1;
        StartCoroutine(SpawnRoutine());
        //NuevaPeticion();
    }

    public void TrySpawnClient()
    {
        Chair freeChair = allChairs.FirstOrDefault(c => !c.isOcupped);
        //Vector3 spawn = new Vector3(spawnPoint.position.x * _randomEnter, _altureChair.position.y, spawnPoint.position.z);
        Vector3 spawn = new Vector3(spawnPoint.position.x, _altureChair.position.y, spawnPoint.position.z);

        if (freeChair != null)
        {
            GameObject clientObj = Instantiate(clientPrefab, spawn, Quaternion.identity);
            Client client = clientObj.GetComponent<Client>();
            gameManager.client = client;
            //NuevaPeticion();
            //client.GetComponent<NPCRequest>().requestedItem = currentRequest;
            client.AssignChair(freeChair);
        }
        else
        {
            Debug.Log("No hay sillas libres, no spawnea el cliente.");
        }
    }


    //public void NuevaPeticion()
    //{
    //    string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };
    //    currentRequest = opciones[Random.Range(0, opciones.Length)];
    //    requestText.text = "El cliente quiere: " + currentRequest;
    //}

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            TrySpawnClient();
        }
    }

}
