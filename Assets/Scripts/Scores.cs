using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Scores : MonoBehaviour
{
    [SerializeField] GameObject[] scorePanels;
    [SerializeField] TMPro.TextMeshProUGUI[] scoreText;
    [SerializeField] TMPro.TextMeshProUGUI timeText;
    [SerializeField] TMPro.TextMeshProUGUI timerText;
    int[] scores;

    float gameTime;
    [SerializeField] float maxGameTime = 60;
    [SerializeField] GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        scores = new int[4];
        MyEvents.AddScore.AddListener(IncreaseScore);
        MyEvents.ActivateScores.AddListener(ActivateScoreBoards);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            if (gameTime < 0)
            {
                gameTime = 0;
            }
            UpdateTime();
            if (Gamepad.current != null)
            {
                if (Gamepad.current.startButton.wasPressedThisFrame)
                {
                    if (Time.timeScale > 0)
                    {
                        Time.timeScale = 0;
                        gameOverPanel.SetActive(true);
                    }
                    else
                    {
                        Time.timeScale = 1;
                        gameOverPanel.SetActive(false);
                    }
                    MyEvents.TogglePause.Invoke();
                }
            }
        }
    }

    public void ActivateScoreBoards(int value)
    {
        for (int i = 0; i < scorePanels.Length; i++)
        {
            if (i < value)
            {
                scorePanels[i].SetActive(true);
            }
            else
            {
                scorePanels[i].SetActive(false);
            }
        }
        StartGame();
    }
    public void IncreaseScore(int index)
    {
        scores[index]++;
        scoreText[index].text = scores[index].ToString();
    }
    public void StartGame()
    {
        gameTime = maxGameTime;
        UpdateTime();
        ResetScores();
    }

    void UpdateTime()
    {
        if (gameTime > 0)
        {
            timerText.text = gameTime.ToString("##");
            timeText.text = "Time:";
        }
        else
        {
            timeText.text = "Game Over";
            MyEvents.GameOver.Invoke();
        }
    }

    void ResetScores()
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            scores[i] = 0;
            scoreText[i].text = scores[i].ToString();
        }
    }
}
