using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] private GameObject healthPickupPrefab;
    [SerializeField] private GameObject currentPickup;
    [SerializeField] private float respawnDelay = 10f;

    // Can paketinin çıkabileceği alan
    [SerializeField] private float minX = -6f;
    [SerializeField] private float maxX = 6f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    private float timer;

    private void Update()
    {
        if (currentPickup != null)
            return;

        timer += Time.deltaTime;

        if (timer >= respawnDelay)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            currentPickup = Instantiate(
                healthPickupPrefab,
                randomPosition,
                Quaternion.identity
            );

            timer = 0f;
        }
    }
}