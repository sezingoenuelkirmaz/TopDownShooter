using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private bool isDying = false;

    public void Die()
    {
        if (isDying) return;

        isDying = true;

        // Ölürken takip etmesin ve saldırmasın
        EnemyFollow enemyFollow = GetComponent<EnemyFollow>();
        if (enemyFollow != null)
            enemyFollow.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Başka mermilerle tekrar çarpışmasın
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        StartCoroutine(ShrinkAndDestroy());
    }

    private IEnumerator ShrinkAndDestroy()
    {
        Vector3 originalScale = transform.localScale;
        float duration = 0.25f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float size = Mathf.Clamp01(1f - elapsed / duration);
            transform.localScale = originalScale * size;

            yield return null;
        }

        Destroy(gameObject);
    }
}