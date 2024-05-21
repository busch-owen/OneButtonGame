using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager>
{
    public bool GameStarted { get; private set; }

    private SceneController _sceneController;
    private UIManager _uiManager;
    private ScoreHandler _scoreHandler;

    private void Start()
    {
        ResetGameState();
    }

    public void StartGame()
    {
        GameStarted = true;
        _uiManager.CloseMainMenu();
        _scoreHandler.StartTickingScoreCounter();
    }

    public void DeathSequence()
    {
        _uiManager.OpenDeathMenu();
        _scoreHandler.StopTickingScoreCounter();
    }

    public void RestartGame()
    {
        //All this will do for now is reload the scene, potentially we could add a high score system or something of the sort.
        _scoreHandler.ResetCurrentScores();
        _sceneController.ReloadCurrentScene();
        Invoke(nameof(ResetGameState), 0.05f);
    }

    public void ResetGameState()
    {
        GameStarted = false;
        _sceneController = FindObjectOfType<SceneController>();
        _uiManager = FindObjectOfType<UIManager>();
        _scoreHandler = FindObjectOfType<ScoreHandler>();
    }
}
