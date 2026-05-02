using UnityEngine;

public class RunRoomManager : MonoBehaviour
{
    public int enemiesLeft;
    public GameObject rewardPanel;

    // RoomGenerator bu flag'i true yaptıktan sonra EnemyKilled tetikler
    private bool setupDone = false;

    void Start()
    {
        Time.timeScale = 1f;
        enemiesLeft = 0; // RoomGenerator RegisterEnemySpawn ile dolduracak

        if (rewardPanel != null)
            rewardPanel.SetActive(false);
    }

    /// <summary>
    /// RoomGenerator tüm spawn'ları bitirdikten sonra bu metodu çağırır.
    /// Bundan önce EnemyKilled etkisiz olur.
    /// </summary>
    public void FinalizeEnemyCount()
    {
        setupDone = true;
        Debug.Log($"[RunRoomManager] Setup tamamlandı. Toplam düşman: {enemiesLeft}");

        // Hiç düşman yoksa (sadece Reward odası gibi), zaten 0 → reward direkt açılmasın
        // Bu kontrolü kasıtlı olarak yapmıyoruz, oyun tasarımına göre değiştirilebilir
    }

    public void EnemyKilled()
    {
        if (!setupDone) return; // henüz kurulum bitmedi, sayma

        enemiesLeft--;
        if (enemiesLeft < 0) enemiesLeft = 0;

        Debug.Log($"[RunRoomManager] Düşman öldü. Kalan: {enemiesLeft}");

        if (enemiesLeft <= 0)
        {
            Debug.Log("[RunRoomManager] Tüm düşmanlar öldü! Reward açılıyor.");
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
