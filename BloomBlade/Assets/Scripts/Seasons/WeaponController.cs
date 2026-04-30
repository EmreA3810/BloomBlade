using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class WeaponController : MonoBehaviour
{
    public SpringAttack springAttack;

    public SpringState spring;
    public SummerState summer;
    public AutumnState autumn;
    public WinterState winter;

    private ISeasonState currentState;
    public float maxEnergy = 100f;
    public float energyRegenPerSecond = 12f;
    public float currentEnergy;
    public float cooldownRemaining;

    void Awake()
    {
        if (springAttack == null)
            springAttack = GetComponent<SpringAttack>();

        if (springAttack == null)
            springAttack = GetComponentInChildren<SpringAttack>();

        if (springAttack == null)
            springAttack = GetComponentInParent<SpringAttack>();
    }

    void Start()
    {
        spring = new SpringState(this);
        summer = new SummerState(this);
        autumn = new AutumnState(this);
        winter = new WinterState(this);
        currentState = spring;
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        currentEnergy = Mathf.Min(maxEnergy, currentEnergy + energyRegenPerSecond * Time.deltaTime);
        if (cooldownRemaining > 0f) cooldownRemaining -= Time.deltaTime;

        if (PressedDigit1()) currentState = spring;
        if (PressedDigit2()) currentState = summer;
        if (PressedDigit3()) currentState = autumn;
        if (PressedDigit4()) currentState = winter;

        if (PressedSpace() && currentState != null && springAttack != null)
            TryAttack();
    }

    private void TryAttack()
    {
        if (cooldownRemaining > 0f) return;

        float energyCost = GetEnergyCost();
        if (currentEnergy < energyCost) return;

        currentEnergy -= energyCost;
        cooldownRemaining = GetCooldown();
        currentState.Attack();
    }

    private float GetEnergyCost()
    {
        if (currentState == spring) return 10f;
        if (currentState == summer) return 22f;
        if (currentState == autumn) return 16f;
        if (currentState == winter) return 12f;
        return 10f;
    }

    private float GetCooldown()
    {
        if (currentState == spring) return 0.25f;
        if (currentState == summer) return 0.6f;
        if (currentState == autumn) return 0.4f;
        if (currentState == winter) return 0.35f;
        return 0.35f;
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

    private static bool PressedDigit4()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null &&
               (Keyboard.current.digit4Key.wasPressedThisFrame || Keyboard.current.numpad4Key.wasPressedThisFrame);
#elif ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4);
#else
        return false;
#endif
    }
}
