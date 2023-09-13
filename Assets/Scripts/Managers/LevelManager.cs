using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject[] Stations;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        Stations = GameObject.FindGameObjectsWithTag("Station");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
