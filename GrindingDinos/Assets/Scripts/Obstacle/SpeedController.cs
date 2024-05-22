using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float moveSpeed = -0.1f;

    [SerializeField] private float speedIncreaseRate;
    [SerializeField] private float thresholdIncreaseRate;

    private ScoreHandler _scoreHandler;

    private void Awake()
    {
        _scoreHandler = FindObjectOfType<ScoreHandler>();
    }

    public void SpeedupTimer()
    {
        moveSpeed /= speedIncreaseRate;
        Debug.Log("Multiplied");
        _scoreHandler.MultiplyTickRate(speedIncreaseRate, thresholdIncreaseRate);
    }
}
