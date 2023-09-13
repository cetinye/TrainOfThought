using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public GameObject train;
    public int amountOfTrains;
    public float timeBetweenTrains;

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
        for (int i = 0; i < amountOfTrains; i++)
        {
            Instantiate(train, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenTrains);
        }

        yield return null;
    }
}
