using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public GameObject train;
    public int amountOfTrains;
    public float timeBetweenTrains;
    public float timeToWaitBeforeSpawn;

    private GameObject spawnedTrain;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTrains();
    }

    private void SpawnTrains()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeToWaitBeforeSpawn);

        for (int i = 0; i < amountOfTrains; i++)
        {
            spawnedTrain = Instantiate(train, transform.position, Quaternion.identity);
            //align train with tunnel
            spawnedTrain.transform.rotation = this.transform.rotation;
            yield return new WaitForSeconds(timeBetweenTrains);
        }

        yield return null;
    }
}
