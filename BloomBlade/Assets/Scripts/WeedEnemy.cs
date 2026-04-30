using UnityEngine;

public class WeedEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 60;
    public int touchDamage = 10;
    public float damageInterval = 1f;

    private int currentHealth;
    private Transform player;
    private float nextDamageTime;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (Time.time < nextDamageTime) return;

        PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
        if (ph != null) ph.TakeDamage(touchDamage);

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
