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
        if (enemyPrefab == null) return;

        RunRoomManager roomManager = FindAnyObjectByType<RunRoomManager>();
        foreach (GameObject room in spawnedRooms)
        {
            Transform spawn = room.transform.Find("Spawn_Enemy");
            if (spawn == null) continue;
            if (room.name.Contains("Reward")) continue;

            Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
            if (roomManager != null) roomManager.RegisterEnemySpawn();
        }
    }

    private void SetupDoors()
    {
        for (int i = 0; i < spawnedRooms.Count - 1; i++)
        {
            Transform door = spawnedRooms[i].transform.Find("Door_Right");
            Transform nextSpawn = spawnedRooms[i + 1].transform.Find("Spawn_Player");
            if (door == null || nextSpawn == null) continue;

            BoxCollider2D collider = door.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                collider = door.gameObject.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(1.2f, 2f);
            }
            collider.isTrigger = true;

            RoomDoor roomDoor = door.GetComponent<RoomDoor>();
            if (roomDoor == null) roomDoor = door.gameObject.AddComponent<RoomDoor>();
            roomDoor.targetSpawn = nextSpawn;
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
