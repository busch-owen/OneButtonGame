using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject deathMenu;
    private GameObject mainMenu;

    [SerializeField] private TMP_Text distanceScoreText, trickScoreText;

    private void Awake()
    {
        deathMenu = GetComponentInChildren<DeathMenu>().gameObject;
        mainMenu = GetComponentInChildren<MainMenu>().gameObject;
        
        mainMenu?.SetActive(true);
        deathMenu?.SetActive(false);
    }

    public void UpdateDistanceScore(int newValue)
    {
        distanceScoreText.text = "Distance: " + newValue + "m";
    }
    
    public void UpdateTrickScore(int newValue)
    {
        trickScoreText.text = "Trick Score: " + newValue;
    }
    
    public void OpenDeathMenu()
    {
        deathMenu?.SetActive(true);
    }

    public void CloseMainMenu()
    {
        mainMenu?.SetActive(false);
    }
}
