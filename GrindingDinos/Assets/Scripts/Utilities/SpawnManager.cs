using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    Transform _spawnPoint;
    [SerializeField]
    List<GameObject> _obstaclePrefabs;

    void Start()
    {
    }

    void HandleSpawning()
    {
        
    }
}
