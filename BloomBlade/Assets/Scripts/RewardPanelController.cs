using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class RewardPanelController : MonoBehaviour
{
    public PlayerRunStats playerStats;

    void Awake()
    {
        if (playerStats == null)
            playerStats = FindAnyObjectByType<PlayerRunStats>();
    }

    void OnEnable()
    {
        if (playerStats == null)
            playerStats = FindAnyObjectByType<PlayerRunStats>();
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy) return;

        if (PressedDigit1()) PickDamageBoon();
        else if (PressedDigit2()) PickSpeedBoon();
        else if (PressedDigit3()) PickHealthBoon();
    }

    public void PickDamageBoon()
    {
        if (playerStats != null) playerStats.AddDamage(10);
        ClosePanel();
    }

    public void PickSpeedBoon()
    {
        if (playerStats != null) playerStats.AddMoveSpeed(1.5f);
        ClosePanel();
    }

    public void PickHealthBoon()
    {
        if (playerStats != null) playerStats.AddMaxHealth(30);
        ClosePanel();
    }

    private void ClosePanel()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private static bool PressedDigit1()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null &&
               (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame);
#elif ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1);
#else
        return false;
#endif
    }

    private static bool PressedDigit2()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null &&
               (Keyboard.current.digit2Key.wasPressedThisFrame || Keyboard.current.numpad2Key.wasPressedThisFrame);
#elif ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2);
#else
        return false;
#endif
    }

    private static bool PressedDigit3()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null &&
               (Keyboard.current.digit3Key.wasPressedThisFrame || Keyboard.current.numpad3Key.wasPressedThisFrame);
#elif ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3);
#else
        return false;
#endif
    }
}
