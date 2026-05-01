using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class HubRunStarter : MonoBehaviour
{
    public string runSceneName = "RunScene";
    public bool showHubHint = true;
    private readonly GUIStyle hintStyle = new GUIStyle();

    void Awake()
    {
        hintStyle.alignment = TextAnchor.MiddleCenter;
        hintStyle.fontSize = 28;
        hintStyle.normal.textColor = Color.white;
    }

    public void StartRun()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(runSceneName);
    }

    void Update()
    {
        if (PressedSpace())
            StartRun();
    }

    void OnGUI()
    {
        if (!showHubHint) return;

        GUI.Label(
            new Rect(0, 0, Screen.width, Screen.height),
            "HUB\nRun baslatmak icin SPACE'e bas",
            hintStyle
        );
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
