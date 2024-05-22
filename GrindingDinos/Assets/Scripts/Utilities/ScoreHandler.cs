using UnityEngine;
using UnityEngine.Serialization;

public class ScoreHandler : Singleton<ScoreHandler>
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    
    private int _distanceScore;
    private int _trickScore;
    private int _totalScore;

    private static int _highScore;

    [SerializeField] private int scoreTick;
    [SerializeField] private float distanceScoreTickRate;

    [SerializeField] private float scoreGoalThreshold;

    private SpeedController _speedController;

    public override void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _speedController = FindObjectOfType<SpeedController>();
    }
    
    public void AddToTrickScore(int scoreValue)
    {
        _trickScore += scoreValue;
        _uiManager.UpdateTrickScore(_trickScore);
    }

    private void AddToDistanceScore()
    {
        _distanceScore += scoreTick;
        _uiManager.UpdateDistanceScore(_distanceScore);

        _totalScore = _distanceScore + _trickScore;
        UpdateHighScore();
        if (_distanceScore > scoreGoalThreshold)
        {
            _speedController.SpeedupTimer();
        }
    }

    public void StartTickingScoreCounter()
    {
        InvokeRepeating(nameof(AddToDistanceScore), 0.01f, distanceScoreTickRate);
    }

    public void StopTickingScoreCounter()
    {
        CancelInvoke(nameof(AddToDistanceScore));
    }

    public void PostTotalScore()
    {
        _uiManager.UpdateTotalScore(_totalScore);
    }

    private void UpdateHighScore()
    {
        if (_totalScore > _highScore)
        {
            _highScore = _totalScore;
        }
    }

    public void MultiplyTickRate(float speedRate, float thresRate)
    {
        distanceScoreTickRate *= speedRate;
        scoreGoalThreshold *= thresRate;
        StopTickingScoreCounter();
        StartTickingScoreCounter();
    }
        
    public void ResetCurrentScores()
    {
        _trickScore = 0;
        _distanceScore = 0;
        _uiManager.PostHighScore(_highScore);
    }
}
