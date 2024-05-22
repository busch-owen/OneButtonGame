using UnityEngine;

public class ScoreHandler : Singleton<ScoreHandler>
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    
    private int _distanceScore;
    private int _trickScore;
    private int _totalScore;

    private static int _highScore;

    [SerializeField] private int scoreTick;
    [SerializeField] private float scoreTickRate;

    [SerializeField] private float scoreGoalThreshold;

    private SpeedController _speedController;

    public override void Awake()
    {
        base.Awake();
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _speedController = FindObjectOfType<SpeedController>();
    }
    
    public void AddToTrickScore(int scoreValue)
    {
        _trickScore += scoreValue;
        _uiManager.UpdateTrickScore(_trickScore);
    }

    public void AddToDistanceScore()
    {
        _distanceScore += scoreTick;
        _uiManager.UpdateDistanceScore(_distanceScore);

        _totalScore = _distanceScore + _trickScore;
        UpdateHighScore();
        if (_distanceScore >= scoreGoalThreshold)
        {
            _speedController.SpeedupTimer();
        }
    }

    public void StartTickingScoreCounter()
    {
        CancelInvoke(nameof(AddToDistanceScore));
        InvokeRepeating(nameof(AddToDistanceScore), 0, scoreTickRate);
    }

    public void StopTickingScoreCounter()
    {
        CancelInvoke(nameof(AddToDistanceScore));
    }

    public void PostTotalScore()
    {
        _uiManager.UpdateTotalScore(_totalScore);
    }

    public void UpdateHighScore()
    {
        if (_totalScore > _highScore)
        {
            _highScore = _totalScore;
        }
    }

    public void MultiplyTickRate(float rate)
    {
        scoreTickRate *= rate;
        scoreGoalThreshold *= rate;
        StartTickingScoreCounter();
    }
        
    public void ResetCurrentScores()
    {
        _trickScore = 0;
        _distanceScore = 0;
        _uiManager.PostHighScore(_highScore);
    }
}
