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

            EnemyHealth health = other.GetComponent<EnemyHealth>();

            if (health != null)
            {
                // Canı olan düşmana 1 hasar ver
                health.TakeDamage(1);
            }
            else
            {
                // Normal ve hızlı düşman tek mermide ölür
                if (ScoreManager.Instance != null)
                    ScoreManager.Instance.AddScore(1);

                EnemyDeath death = other.GetComponent<EnemyDeath>();

                if (death == null)
                    death = other.gameObject.AddComponent<EnemyDeath>();

                death.Die();
            }

            Destroy(gameObject);
        }
    }
}