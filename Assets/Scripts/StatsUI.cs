using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI levelId;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject sucessScreen;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private float timeRemaining = 2;
    [SerializeField] private int countdownTime = 3;


    private GameManager gameManager;

    private float minutes;
    private float seconds;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

        levelId.text = "Level: " + LevelManager.instance.levelId;

        countdownTime = LevelManager.instance.timeToWaitBeforeStart;
        StartCoroutine(StartCountdown());
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
        //timer continue if game is playing
        if (timeRemaining > 0 && gameManager.state == GameManager.GameState.Playing)
        {
            timeRemaining -= Time.deltaTime;
        }
        //stop timer if time ran out
        else if (timeRemaining <= 0 && gameManager.state == GameManager.GameState.Playing)
        {
            gameManager.state = GameManager.GameState.Failed;
            timeRemaining = 0;
        }

        minutes = Mathf.FloorToInt(timeRemaining / 60);
        seconds = Mathf.FloorToInt(timeRemaining % 60);

        //make timer in 0:00 format
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
        levelId.text = "Level: " + LevelManager.instance.levelId;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.text = "GO !";
        GameManager.instance.state = GameManager.GameState.Playing;
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);
    }
}
