using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class HubRunStarter : MonoBehaviour
{
    public string runSceneName = "RunScene";

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
