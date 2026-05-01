using UnityEngine;

public class RunRoomManager : MonoBehaviour
{
    public int enemiesLeft;
    public GameObject rewardPanel;

    void Start()
    {
        Time.timeScale = 1f;
        enemiesLeft = FindObjectsByType<WeedEnemy>().Length;

        if (rewardPanel != null)
            rewardPanel.SetActive(false);
    }

    public void EnemyKilled()
    {
        enemiesLeft--;
        if (enemiesLeft < 0) enemiesLeft = 0;

        if (enemiesLeft <= 0)
        {
            if (rewardPanel != null)
                rewardPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void RegisterEnemySpawn()
    {
        enemiesLeft++;
    }
}
