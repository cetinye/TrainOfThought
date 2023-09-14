using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int amountOfTrains;
    public float timeBetweenTrains;
    public float timeToWaitBeforeSpawn;
    public GameObject[] Stations;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        Stations = GameObject.FindGameObjectsWithTag("Station");
    }
}
