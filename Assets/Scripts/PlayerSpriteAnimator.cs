using UnityEngine;

public class PlayerSpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] downFrames;
    [SerializeField] private Sprite[] sideFrames;
    [SerializeField] private Sprite[] upFrames;
    [SerializeField] private float frameTime = 0.15f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Sprite[] currentFrames;
    private int frameIndex;
    private float timer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentFrames = downFrames;
    }

    void Update()
    {
        Vector2 velocity = rb.linearVelocity;

        // Steht still: erstes Bild der letzten Richtung zeigen
        if (velocity.sqrMagnitude < 0.01f)
        {
            frameIndex = 0;
            timer = 0f;
            spriteRenderer.sprite = currentFrames[0];
            return;
        }

        // Richtung wählen
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            currentFrames = sideFrames;
            spriteRenderer.flipX = velocity.x < 0;
        }
        else
        {
            currentFrames = velocity.y > 0 ? upFrames : downFrames;
            spriteRenderer.flipX = false;
        }

        // Nächstes Bild nach frameTime Sekunden
        timer += Time.deltaTime;
        if (timer >= frameTime)
        {
            timer = 0f;
            frameIndex = (frameIndex + 1) % currentFrames.Length;
        }

        spriteRenderer.sprite = currentFrames[frameIndex];
    }
}
