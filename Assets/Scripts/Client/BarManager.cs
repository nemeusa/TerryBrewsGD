using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    [SerializeField] GameObject clientPrefab;
    public List<Chair> allChairs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform _altureChair;
    private float _randomEnter;

    private void Start()
    {
        _randomEnter = Random.Range(0, 2) == 0 ? -1 : 1;
        StartCoroutine(SpawnRoutine());
    }

    public void TrySpawnClient()
    {
        Chair freeChair = allChairs.FirstOrDefault(c => !c.isOcupped);
        //spawnPoint.position = new Vector3(0, 0, 0);
        //spawnPoint.position = new Vector3(spawnPoint.position.x * _randomEnter, _altureChair.position.y, spawnPoint.position.z);
        Vector3 spawn = new Vector3(spawnPoint.position.x * _randomEnter, _altureChair.position.y, spawnPoint.position.z);

        if (freeChair != null)
        {
            GameObject clientObj = Instantiate(clientPrefab, spawn, Quaternion.identity);
            Client client = clientObj.GetComponent<Client>();
            client.AssignChair(freeChair);
        }
        else
        {
            Debug.Log("No hay sillas libres, no spawnea el cliente.");
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
