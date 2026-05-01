using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool showHealthUI = true;

    private readonly GUIStyle hpTextStyle = new GUIStyle();

    void Start()
    {
        currentHealth = maxHealth;
        hpTextStyle.fontSize = 20;
        hpTextStyle.normal.textColor = Color.white;
    }

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

    void OnGUI()
    {
        if (!showHealthUI) return;

        float x = 20f;
        float y = 20f;
        float width = 220f;
        float height = 22f;
        float ratio = maxHealth > 0 ? (float)currentHealth / maxHealth : 0f;

        GUI.color = Color.black;
        GUI.Box(new Rect(x, y, width, height), GUIContent.none);

        GUI.color = Color.green;
        GUI.Box(new Rect(x, y, width * ratio, height), GUIContent.none);

        GUI.color = Color.white;
        GUI.Label(new Rect(x, y + 24f, 220f, 28f), $"HP: {currentHealth} / {maxHealth}", hpTextStyle);
    }
}
