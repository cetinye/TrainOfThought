using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public GameObject train;

    private int amountOfTrains;
    private float timeBetweenTrains;
    private float timeToWaitBeforeSpawn;
    private GameObject spawnedTrain;
    private Color colorTrain;
    private Color colorVagon;
    private int index = 0;

    private LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;

        amountOfTrains = levelManager.stationCount;
        timeBetweenTrains = levelManager.timeBetweenTrains;
        timeToWaitBeforeSpawn = levelManager.timeToWaitBeforeSpawn;

        index = 0;
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
            spawnedTrain = GameObject.Instantiate(train, transform.position, Quaternion.identity) as GameObject;
            //align train with tunnel
            spawnedTrain.transform.rotation = this.transform.rotation;
            ColorTrain(spawnedTrain);
            yield return new WaitForSeconds(timeBetweenTrains);
        }

        yield return null;
    }

    void ColorTrain(GameObject sTrain)
    {
        //apply statin colors to trains
        colorTrain = LevelManager.instance.Stations[index].GetComponent<Station>().colorHouse;
        colorVagon = LevelManager.instance.Stations[index].GetComponent<Station>().colorRoof;
        index++;

        sTrain.transform.GetChild(0).GetComponent<Renderer>().material.color = colorTrain;
        sTrain.transform.GetChild(1).GetComponent<Renderer>().material.color = colorVagon;
    }
}
