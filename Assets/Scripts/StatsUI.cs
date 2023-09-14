using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject sucessScreen;
    [SerializeField] private GameObject failScreen;

    public float timeRemaining = 2;

    private GameManager gameManager;

    private float minutes;
    private float seconds;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        UpdateScore();

        if(gameManager.state == GameManager.GameState.Success)
            ShowEndScreen(GameManager.GameState.Success);

        if (gameManager.state == GameManager.GameState.Failed)
            ShowEndScreen(GameManager.GameState.Failed);
    }

    private void UpdateTime()
    {
        if (timeRemaining > 0 && gameManager.state == GameManager.GameState.Playing)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining <= 0 && gameManager.state == GameManager.GameState.Playing)
        {
            gameManager.state = GameManager.GameState.Failed;
            timeRemaining = 0;
        }

        minutes = Mathf.FloorToInt(timeRemaining / 60);
        seconds = Mathf.FloorToInt(timeRemaining % 60);

        time.text = "TIME " + string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private void UpdateScore()
    {
        score.text = "CORRECT " + gameManager.correctScore + " of " + LevelManager.instance.amountOfTrains;
    }

    public void ShowEndScreen(GameManager.GameState gState)
    {
        TextMeshProUGUI timeLeftText = sucessScreen.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI scoreText = sucessScreen.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

        //update success panel
        timeLeftText.text = "Time Left " + string.Format("{0:0}:{1:00}", minutes, seconds);
        scoreText.text = "Correct " + gameManager.correctScore + " of " + LevelManager.instance.amountOfTrains;

        //copy to fail panel
        failScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = timeLeftText.text;
        failScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = scoreText.text;

        if (gState == GameManager.GameState.Success)
            sucessScreen.gameObject.SetActive(true);

        else if (gState == GameManager.GameState.Failed)
            failScreen.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
