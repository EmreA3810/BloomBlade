using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public Transform targetSpawn;

    // Çift tetiklenmeyi önlemek için cooldown
    private float lastTeleportTime = -10f;
    private const float cooldown = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        // "Player" tag'i varsa veya PlayerHealth/PlayerController bileşeni varsa geç
        bool isPlayer = other.CompareTag("Player")
                        || other.GetComponent<PlayerHealth>() != null
                        || other.GetComponent<PlayerController>() != null;

        if (!isPlayer) return;
        if (targetSpawn == null)
        {
            Debug.LogWarning("[RoomDoor] targetSpawn atanmamış!", this);
            return;
        }

        if (Time.time - lastTeleportTime < cooldown) return;
        lastTeleportTime = Time.time;

        Debug.Log($"[RoomDoor] Oyuncu {name} kapısından geçti → {targetSpawn.position}");
        other.transform.position = targetSpawn.position;

        // Rigidbody varsa velocityyi sıfırla (kayma önleme)
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }
}
