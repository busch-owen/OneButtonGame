using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int PlayerLives { get; private set; }

    public void TakeDamage(int damage)
    {
        PlayerLives -= damage;
    }
}
