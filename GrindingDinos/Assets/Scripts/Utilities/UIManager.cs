using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject mainMenu;

    private void Awake()
    {
        deathMenu?.SetActive(false);
    }
    
    public void OpenDeathMenu()
    {
        deathMenu?.SetActive(true);
        Debug.Log("you ded. Press space to restart");
    }

    public void CloseMainMenu()
    {
        mainMenu?.SetActive(false);
    }
}
