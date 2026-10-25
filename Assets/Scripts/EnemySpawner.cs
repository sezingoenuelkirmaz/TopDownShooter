
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
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

        GameObject enemy = Instantiate(
            enemyPrefab,
            position,
            Quaternion.identity
        );

        EnemyFollow follow = enemy.GetComponent<EnemyFollow>();

        if (follow != null)
            follow.SetPlayer(player);
    }
}
