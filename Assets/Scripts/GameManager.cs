using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tanks;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] List<int> validSpawnZones;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject lobbyPanel;
    [SerializeField] GameObject replayButton;

    [SerializeField] GameObject[] scorePanels;
    [SerializeField] TMPro.TextMeshProUGUI[] scoreText;
    [SerializeField] TMPro.TextMeshProUGUI timeText;
    [SerializeField] TMPro.TextMeshProUGUI timerText;
    int[] scores;

    float gameTime;
    [SerializeField] float maxGameTime = 60;

    LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1600, 1000, true);
        MyEvents.GameOver.AddListener(GameOver);
        MyEvents.Replay.AddListener(Replay);

        scores = new int[4];
        MyEvents.AddScore.AddListener(IncreaseScore);
        MyEvents.ActivateScores.AddListener(ActivateScoreBoards);
        validSpawnZones = new List<int>();

        playerMask = LayerMask.GetMask("Player");
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

    public void AddTank(GameObject tank)
    {
        tanks.Add(tank);
    }

    // Respawn tank when reduced to 0 health
    public void Respawn(int index)
    {
        // Choose a non occupied spawn zone to respawn in
        validSpawnZones.Clear();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Add all non occupied spawn zones to a list
            if (!Physics.CheckBox(spawnPoints[i].transform.position, new Vector3(5,5,5), Quaternion.identity, playerMask))
            {
                validSpawnZones.Add(i);
            }
        }

        // Choose randomly from the list of non occupied spawn zones
        tanks[index].transform.position = spawnPoints[validSpawnZones[UnityEngine.Random.Range(0, validSpawnZones.Count)]].transform.position;
        tanks[index].transform.rotation = Quaternion.identity;
        tanks[index].GetComponent<Health>().HealDamage(100);
    }

    public void GameOver()
    {
        MyEvents.TogglePause.Invoke();
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(replayButton);
    }

    public void Replay()
    {
        // On a replay set all tanks to the original spawn zones and reset heath and rotation.
        for (int i = 0; i < tanks.Count; i++)
        {
            tanks[i].transform.position = spawnPoints[i].transform.position;
            tanks[i].transform.rotation = Quaternion.identity;
            tanks[i].GetComponent<Health>().HealDamage(100);
        }

        MyEvents.TogglePause.Invoke();
        MyEvents.ActivateScores.Invoke(tanks.Count);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenLobby()
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            Destroy(tanks[i].gameObject);
        }
        tanks.Clear();
        gameOverPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        Time.timeScale = 1;
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

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReplayGame()
    {
        MyEvents.Replay.Invoke();
    }

    public void ReturnToLobby()
    {
        OpenLobby();
    }
}
