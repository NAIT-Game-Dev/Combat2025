using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tanks;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject lobbyPanel;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1600, 1000, true);
        MyEvents.GameOver.AddListener(GameOver);
        MyEvents.Replay.AddListener(Replay);
        MyEvents.OpenLobby.AddListener(OpenLobby);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTank(GameObject tank)
    {
        tanks.Add(tank);
    }
    public void Respawn(int index)
    {
        tanks[index].transform.position = spawnPoints[index].transform.position;
        tanks[index].transform.rotation = Quaternion.identity;
        tanks[index].GetComponent<Health>().HealDamage(100);
    }

    public void GameOver()
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            tanks[i].gameObject.GetComponent<TankController>().enabled = false;
        }
        gameOverPanel.SetActive(true);
    }

    public void Replay()
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            Respawn(i);
            tanks[i].gameObject.GetComponent<TankController>().enabled = true;
        }
        MyEvents.ActivateScores.Invoke(tanks.Count);
        gameOverPanel.SetActive(false);
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
    }
}
