using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;
    [SerializeField] private GameObject tankEnemyPrefab;

    [SerializeField] private Transform player;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnDistance = 7f;

    private float timer;

    private void Update()
    {
        if (player == null || enemyPrefab == null)
            return;

        PlayerHealth health = player.GetComponent<PlayerHealth>();

        if (health != null && health.IsDead)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;

        if (direction == Vector2.zero)
            direction = Vector2.right;

        Vector3 position = player.position +
            (Vector3)(direction * spawnDistance);

        float chance = Random.value;
        GameObject selectedPrefab = enemyPrefab;

        if (chance < 0.2f && tankEnemyPrefab != null)
        {
            selectedPrefab = tankEnemyPrefab;
        }
        else if (chance >= 0.2f && chance < 0.5f &&
                 fastEnemyPrefab != null)
        {
            selectedPrefab = fastEnemyPrefab;
        }

        GameObject enemy = Instantiate(
            selectedPrefab,
            position,
            Quaternion.identity
        );

        EnemyFollow follow = enemy.GetComponent<EnemyFollow>();

        if (follow != null)
            follow.SetPlayer(player);
    }
}