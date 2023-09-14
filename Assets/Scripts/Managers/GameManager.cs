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
        state = GameState.Playing;
    }

    // Update is called once per frame
    public void CheckGameStatus()
    {
        if (correctScore == LevelManager.instance.amountOfTrains)
        {
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
