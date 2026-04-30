using UnityEngine;

public class PlayerRunStats : MonoBehaviour
{
    public int bonusDamage;
    public float bonusMoveSpeed;

    public void AddDamage(int amount)
    {
        bonusDamage += amount;
    }

    public void AddMoveSpeed(float amount)
    {
        bonusMoveSpeed += amount;
    }

    public void AddMaxHealth(int amount)
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.IncreaseMaxHealth(amount);
    }
}
