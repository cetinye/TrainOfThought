using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state = GameState.Idle;
    public int correctScore;
    public int failScore;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.Idle;
    }

    // Update is called once per frame
    public void CheckGameStatus()
    {
        if (correctScore == LevelManager.instance.amountOfTrains)
        {
            int levelId = LevelManager.instance.levelId;
            LevelManager.instance.levelId = levelId + 1;
            if (LevelManager.instance.levelId > 4)
            {
                levelId = 0;
                LevelManager.instance.levelId = 0;
            }
            PlayerPrefs.SetInt("level", LevelManager.instance.levelId);
            state = GameState.Success;
        }
        
        else if (correctScore + failScore == LevelManager.instance.amountOfTrains)
        {
            state = GameState.Failed;
        }
    }

    public enum GameState
    {
        Idle,
        Success,
        Failed,
        Playing
    }
}
