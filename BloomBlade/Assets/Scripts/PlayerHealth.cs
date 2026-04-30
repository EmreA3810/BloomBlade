using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start() => currentHealth = maxHealth;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (GameManager.Instance != null)
                GameManager.Instance.GameOver();
            gameObject.SetActive(false); // oyuncu kaybolsun
        }
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
}
