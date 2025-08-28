using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scores : MonoBehaviour
{
    [SerializeField] GameObject[] scorePanels;
    [SerializeField] TMPro.TextMeshProUGUI[] scoreText;
    int[] scores;
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
        
    }

    public void ActivateScoreBoards(int value)
    {
        for (int i = 0; i < value; i++)
        {
            scorePanels[i].SetActive(true);
        }
    }
    public void IncreaseScore(int index)
    {
        scores[index]++;
        scoreText[index].text = scores[index].ToString();
    }
}
