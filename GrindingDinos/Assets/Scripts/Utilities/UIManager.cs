using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject deathMenu;
    private GameObject mainMenu;

    private void Awake()
    {
        deathMenu = GetComponentInChildren<DeathMenu>().gameObject;
        mainMenu = GetComponentInChildren<MainMenu>().gameObject;
        
        mainMenu?.SetActive(true);
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
