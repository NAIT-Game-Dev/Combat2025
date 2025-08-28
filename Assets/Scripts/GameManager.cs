using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tanks;
    [SerializeField] GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
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
        tanks[index].GetComponent<Health>().HealDamage(100);
    }
}
