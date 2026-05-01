using UnityEngine;

public class WeedEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 60;
    public int touchDamage = 10;
    public float damageInterval = 1f;
    public float touchDistance = 0.7f;

    private int currentHealth;
    private Transform player;
    private PlayerHealth playerHealth;
    private float nextDamageTime;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
            playerHealth = p.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // Physics ayarı farklı olsa bile yakın temasta hasar vermeyi garanti et
        if (Vector2.Distance(transform.position, player.position) <= touchDistance)
            TryDealTouchDamage(playerHealth);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        TryDealTouchDamage(collision.gameObject.GetComponent<PlayerHealth>());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        TryDealTouchDamage(other.GetComponent<PlayerHealth>());
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
