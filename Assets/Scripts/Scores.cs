using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scores : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI[] scoreText;
    int[] scores;
    // Start is called before the first frame update
    void Start()
    {
        scores = new int[4];
        MyEvents.AddScore.AddListener(IncreaseScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int index)
    {
        scores[index]++;
        scoreText[index].text = scores[index].ToString();
    }
}
