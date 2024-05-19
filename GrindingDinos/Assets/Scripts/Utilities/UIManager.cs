using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject mainMenu;

    private void Awake()
    {
        if (!deathMenu)
            return;
        
        deathMenu?.SetActive(false);
    }
    
    public void OpenDeathMenu()
    {
        if (!deathMenu)
            return;
        
        deathMenu?.SetActive(true);
        Debug.Log("you ded. Press space to restart");
    }

    public void CloseMainMenu()
    {
        if (!mainMenu)
            return;
        
        mainMenu?.SetActive(false);
    }
}
