using UnityEngine;

public class WeedEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 60;
    public int touchDamage = 10;
    public float damageInterval = 1f;
    public float touchDistance = 0.8f;

    private int currentHealth;
    private Transform player;
    private PlayerHealth playerHealth;
    private float nextDamageTime;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Kinematic: hareket MoveTowards ile yapılır, fizik kuvveti almaz → savrulmaz
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        // Önce tag ile bul, yoksa component ile bul
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null)
        {
            PlayerHealth ph = FindAnyObjectByType<PlayerHealth>();
            if (ph != null) p = ph.gameObject;
        }

        if (p != null)
        {
            player = p.transform;
            playerHealth = p.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogWarning("[WeedEnemy] Player bulunamadı! Tag 'Player' olarak ayarlandı mı?");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Kinematic rigidbody → transform ile taşı
        Vector2 newPos = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        transform.position = newPos;

        // Mesafe tabanlı hasar (en güvenilir yöntem)
        if (Vector2.Distance(transform.position, player.position) <= touchDistance)
            TryDealTouchDamage(playerHealth);
    }

    private void TryDealTouchDamage(PlayerHealth ph)
    {
        if (ph == null) return;
        if (Time.time < nextDamageTime) return;

        ph.TakeDamage(touchDamage);
        nextDamageTime = Time.time + damageInterval;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            RunRoomManager roomManager = FindAnyObjectByType<RunRoomManager>();
            if (roomManager != null) roomManager.EnemyKilled();
            Destroy(gameObject);
        }
    }
}

