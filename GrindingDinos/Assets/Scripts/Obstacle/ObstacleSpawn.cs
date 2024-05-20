using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{

    public List<GameObject> obstacles = new List<GameObject>();
    public float spawnTimerLow;
    public float spawnTimerHigh;
    private float spawnTimer;
    public GameManager gameManager;
    public GameObject speedController;

    private bool timerActive;

    private void Start()
    {

    }

    void FixedUpdate()
    {
        if (gameManager.GameStarted && timerActive == false)
        {
            StartCoroutine(SpawnTimer());
        }
    }

    IEnumerator SpawnTimer()
    {
        timerActive = true;
        spawnTimer = UnityEngine.Random.Range(spawnTimerLow, spawnTimerHigh);
        yield return new WaitForSeconds(spawnTimer);
        int obstacleChosen = UnityEngine.Random.Range(0, obstacles.Count);
        GameObject newObstacle = Instantiate(obstacles[obstacleChosen], transform);
        newObstacle.GetComponent<ObstacleMover>().ObstacleCreated(gameManager, speedController);
        timerActive = false;
    }
}
