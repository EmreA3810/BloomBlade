using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject gameOverPanel;
    public string hubSceneName = "HubScene";

    private bool isGameOver;
    private bool hasSeenEnemy;
    private string endMessage = "OLDUN\nYeniden baslamak icin SPACE'e bas";
    private bool loadHubOnConfirm;
    private readonly GUIStyle gameOverStyle = new GUIStyle();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        isGameOver = false;
        gameOverStyle.alignment = TextAnchor.MiddleCenter;
        gameOverStyle.fontSize = 32;
        gameOverStyle.normal.textColor = Color.white;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver)
        {
            int enemyCount = FindObjectsByType<WeedEnemy>().Length;
            if (enemyCount > 0) hasSeenEnemy = true;
            if (hasSeenEnemy && enemyCount == 0) EndGame("KAZANDIN\nYeniden baslamak icin SPACE'e bas");
            return;
        }

        if (PressedSpace())
        {
            Time.timeScale = 1f;

            if (loadHubOnConfirm)
                SceneManager.LoadScene(hubSceneName);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        loadHubOnConfirm = true;
        EndGame("OLDUN\nHub'a donmek icin SPACE'e bas");
    }

    void OnGUI()
    {
        if (!isGameOver || gameOverPanel != null) return;

        GUI.Label(
            new Rect(0, 0, Screen.width, Screen.height),
            endMessage,
            gameOverStyle
        );
    }

    private void EndGame(string message)
    {
        if (isGameOver) return;

        isGameOver = true;
        endMessage = message;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    private static bool PressedSpace()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
#elif ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyDown(KeyCode.Space);
#else
        return false;
#endif
    }
}
