using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int levelId = 0;
    public int amountOfTrains;
    public int stationCount;
    public float timeBetweenTrains;
    public float timeToWaitBeforeSpawn;
    public int timeToWaitBeforeStart = 3;

    [SerializeField] private GameObject platform1;
    [SerializeField] private GameObject station2;
    [SerializeField] private GameObject tunnel3;
    [SerializeField] private GameObject levelParent;
    
    public GameObject[] Stations;

    private Vector3 startingPos;
    private Vector2 lastPos;
    private string[][] levelBase;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        GenerateLevel();
        PositionCamera();
    }

    void GenerateLevel()
    {
        levelId = PlayerPrefs.GetInt("level");

        //full path of the level texts document
        string text = Resources.Load<TextAsset>("LevelTexts/" + levelId).ToString();
        //split by line breaks and spaces
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;
        levelBase = new string[rows][];

        for (int i = 0; i < lines.Length; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            levelBase[i] = stringsOfLine;
        }

        startingPos = levelParent.transform.position;

        //row count
        for (int y = 0; y < levelBase.Length; y++)
        {
            //column count
            for (int x = 0; x < levelBase[0].Length; x++)
            {
                switch (levelBase[y][x])
                {
                    //if 0 then move pos by 1 pixel right
                    case "0":
                        startingPos.x += 1;
                        break;

                    //if 1 then spawn platform and move pos by 1 pixel right
                    case "1":
                        Instantiate(platform1, new Vector3(startingPos.x, startingPos.y, startingPos.z), Quaternion.identity, levelParent.transform);
                        startingPos.x += 1;
                        break;

                    //if 2 then spawn station and move pos by 1 pixel right
                    case "2":
                        stationCount++;
                        Instantiate(station2, new Vector3(startingPos.x, startingPos.y, startingPos.z), Quaternion.identity, levelParent.transform);
                        startingPos.x += 1;
                        break;

                    case "3":
                        Instantiate(tunnel3, new Vector3(startingPos.x, startingPos.y, startingPos.z), Quaternion.identity, levelParent.transform);
                        startingPos.x += 1;
                        break;
                }
            }

            //keep last x for camera pos
            lastPos.x = startingPos.x;

            //reset position and move below
            startingPos.x = levelParent.transform.position.x;
            startingPos.y -= 1;

            //keep last y for camera pos
            lastPos.y = startingPos.y;

        }

        Stations = GameObject.FindGameObjectsWithTag("Station");

        amountOfTrains = stationCount;
    }

    void PositionCamera()
    {
        //position the camera
        Camera.main.transform.position = new Vector3(Mathf.CeilToInt((lastPos.x - 2) / 2), Mathf.CeilToInt(((lastPos.y) / 2) + 1), -1);
        Camera.main.orthographicSize = levelBase.Length - 2 + levelBase[0].Length - 2;
    }
}
