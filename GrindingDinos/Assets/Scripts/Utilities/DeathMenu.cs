using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private Sprite[] deathMessages;

    [SerializeField] private Image deathMessageImage;
    
    private void OnEnable()
    {
        int rand = Random.Range(0, deathMessages.Length);
        deathMessageImage.sprite = deathMessages[rand];
        deathMessageImage.SetNativeSize();
    }
}
