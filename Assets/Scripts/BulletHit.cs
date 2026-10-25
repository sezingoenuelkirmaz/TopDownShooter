
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private bool hasHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (other.CompareTag("Enemy"))
        {
            hasHit = true;

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(1);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
