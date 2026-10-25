
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stopDistance = 0.8f;
    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private float attackInterval = 1f;

    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private float nextAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    private void Start()
    {
        if (player != null)
            playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (Vector2)player.position - rb.position;
        float distance = direction.magnitude;

        rb.linearVelocity = distance > stopDistance
            ? direction.normalized * speed
            : Vector2.zero;

        if (distance <= attackDistance &&
            Time.time >= nextAttackTime &&
            playerHealth != null)
        {
            playerHealth.TakeDamage(1);
            nextAttackTime = Time.time + attackInterval;
        }
    }

    // Yeni doğan düşmana oyuncuyu tanıt
    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
        playerHealth = player != null
            ? player.GetComponent<PlayerHealth>()
            : null;
    }
}
