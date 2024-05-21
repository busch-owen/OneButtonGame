using UnityEngine;

public class ScoreHandler : Singleton<ScoreHandler>
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    
    private int _distanceScore;
    private int _trickScore;

    private int _highScore;

    [SerializeField] private int scoreTick;

    public override void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void FixedUpdate()
    {
        if (!_gameManager.GameStarted)
            return;

        _distanceScore += scoreTick;
        _uiManager.UpdateDistanceScore(_distanceScore);

        if (_distanceScore + _trickScore > _highScore)
        {
            _highScore = _distanceScore + _trickScore;
        }
    }

    public void AddToTrickScore(int scoreValue)
    {
        _trickScore += scoreValue;
        _uiManager.UpdateTrickScore(_trickScore);
    }

    public void ResetCurrentScores()
    {
        _trickScore = 0;
        _distanceScore = 0;
    }
}
