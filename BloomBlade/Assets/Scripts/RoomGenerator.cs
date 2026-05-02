using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public GameObject enemyPrefab;
    public Transform player;
    public float roomSpacing = 18f;
    public int roomCount = 3;

    private readonly List<GameObject> spawnedRooms = new List<GameObject>();

    void Start()
    {
        GenerateRooms();
    }

    private void GenerateRooms()
    {
        if (roomPrefabs == null || roomPrefabs.Length == 0) return;

        List<GameObject> pool = new List<GameObject>(roomPrefabs);
        Shuffle(pool);

        int count = Mathf.Min(roomCount, pool.Count);
        for (int i = 0; i < count; i++)
        {
            Vector3 roomPos = new Vector3(i * roomSpacing, 0f, 0f);
            GameObject room = Instantiate(pool[i], roomPos, Quaternion.identity);
            room.name = pool[i].name;
            spawnedRooms.Add(room);
        }

        SetupPlayerSpawn();
        SetupEnemySpawns();
        SetupDoors();
    }

    private void SetupPlayerSpawn()
    {
        if (player == null || spawnedRooms.Count == 0) return;

        Transform spawn = spawnedRooms[0].transform.Find("Spawn_Player");
        if (spawn != null)
            player.position = spawn.position;
    }

    private void SetupEnemySpawns()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("[RoomGenerator] Enemy Prefab atanmamış! Inspector'da Enemy Prefab alanını doldur.");
            FindAnyObjectByType<RunRoomManager>()?.FinalizeEnemyCount();
            return;
        }

        RunRoomManager roomManager = FindAnyObjectByType<RunRoomManager>();
        int spawnedCount = 0;

        foreach (GameObject room in spawnedRooms)
        {
            // Reward odasına düşman spawn etme
            if (room.name.Contains("Reward")) continue;

            Transform spawn = room.transform.Find("Spawn_Enemy");
            if (spawn == null)
            {
                Debug.LogWarning($"[RoomGenerator] '{room.name}' odasında 'Spawn_Enemy' bulunamadı. Child ismi doğru mu?");
                continue;
            }

            Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
            roomManager?.RegisterEnemySpawn();
            spawnedCount++;
        }

        Debug.Log($"[RoomGenerator] Toplam {spawnedCount} düşman spawn edildi.");
        roomManager?.FinalizeEnemyCount();
    }

    private void SetupDoors()
    {
        // TÜM odaların Door_Right collider'ını temizle ve trigger yap
        foreach (GameObject room in spawnedRooms)
        {
            Transform door = room.transform.Find("Door_Right");
            if (door == null) continue;

            // Prefabdan gelen tüm eski collider'ları sil, temiz başla
            foreach (BoxCollider2D old in door.GetComponents<BoxCollider2D>())
                Destroy(old);
        }

        // Geçiş kapılarını kur (son oda hariç)
        for (int i = 0; i < spawnedRooms.Count - 1; i++)
        {
            Transform door = spawnedRooms[i].transform.Find("Door_Right");
            Transform nextSpawn = spawnedRooms[i + 1].transform.Find("Spawn_Player");
            if (door == null || nextSpawn == null)
            {
                Debug.LogWarning($"[RoomGenerator] Oda {i}: Door_Right veya sonraki Spawn_Player bulunamadı.");
                continue;
            }

            // Yeni temiz trigger collider ekle
            BoxCollider2D col = door.gameObject.AddComponent<BoxCollider2D>();
            col.size = new Vector2(1f, 3f);
            col.isTrigger = true;

            RoomDoor roomDoor = door.GetComponent<RoomDoor>();
            if (roomDoor == null) roomDoor = door.gameObject.AddComponent<RoomDoor>();
            roomDoor.targetSpawn = nextSpawn;

            Debug.Log($"[RoomGenerator] Kapı kuruldu: Oda {i} → Oda {i + 1} ({nextSpawn.position})");
        }
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
