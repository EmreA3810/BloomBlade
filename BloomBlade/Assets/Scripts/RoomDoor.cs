using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public Transform targetSpawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (targetSpawn == null) return;

        other.transform.position = targetSpawn.position;
    }
}
