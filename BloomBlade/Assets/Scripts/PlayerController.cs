using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public PlayerRunStats runStats;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (runStats == null) runStats = GetComponent<PlayerRunStats>();
    }

    void Update()
    {
        Vector2 input = Vector2.zero;

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) input.x -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) input.y -= 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) input.y += 1f;
        }
#elif ENABLE_LEGACY_INPUT_MANAGER
        // WASD ve ok tuşları ile 8 yön hareketi
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
#endif

        moveInput = Vector2.ClampMagnitude(input, 1f);
    }

    void FixedUpdate()
    {
        float finalMoveSpeed = moveSpeed + (runStats != null ? runStats.bonusMoveSpeed : 0f);
        rb.linearVelocity = moveInput.normalized * finalMoveSpeed;
    }
}
