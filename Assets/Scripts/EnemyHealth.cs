using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;

            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddScore(1);

            EnemyDeath death = GetComponent<EnemyDeath>();

            if (death == null)
                death = gameObject.AddComponent<EnemyDeath>();

            death.Die();
        }
    }
}