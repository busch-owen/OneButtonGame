public class GameManager : Singleton<GameManager>
{
    public bool GameStarted { get; private set; }

    private SceneController _sceneController;
    private UIManager _uiManager;

    private void Start()
    {
        _sceneController = FindObjectOfType<SceneController>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void StartGame()
    {
        GameStarted = true;
        _uiManager.CloseMainMenu();
    }

    public void DeathSequence()
    {
        _uiManager.OpenDeathMenu();
    }

    public void RestartGame()
    {
        //All this will do for now is reload the scene, potentially we could add a high score system or something of the sort.
        _sceneController.ReloadCurrentScene();
    }
}
