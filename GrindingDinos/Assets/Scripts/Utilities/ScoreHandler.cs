using UnityEngine;

public class ScoreHandler : Singleton<ScoreHandler>
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    
    private int _distanceScore;
    private int _trickScore;
    private int _totalScore;

    private int _highScore;

    [SerializeField] private int scoreTick;
    [SerializeField] private int scoreTickRate;

    public override void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
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
        if (_totalScore > _highScore)
        {
            _highScore = _totalScore;
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

    public void ResetCurrentScores()
    {
        _trickScore = 0;
        _distanceScore = 0;
    }
}
