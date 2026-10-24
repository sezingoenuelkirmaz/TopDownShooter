using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 12f;

    void Update()
    {
        bool fire =
            (Mouse.current != null &&
             Mouse.current.leftButton.wasPressedThisFrame) ||
            (Keyboard.current != null &&
             Keyboard.current.spaceKey.wasPressedThisFrame);

        if (fire && firePoint != null && bulletPrefab != null)
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                firePoint.rotation
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = (Vector2)firePoint.right * bulletSpeed;

            Destroy(bullet, 3f);
        }
    }
}