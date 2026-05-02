using UnityEngine;

/// <summary>
/// Main Camera'ya ekle. Player Transform'unu sürükle.
/// Smooth follow + Z sabit kalır (2D için zorunlu).
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Player'ı sürükle buraya
    public float smoothSpeed = 8f; // Yüksek = daha sert takip
    public Vector2 offset = Vector2.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z  // Z'yi değiştirme!
        );

        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
    }
}
