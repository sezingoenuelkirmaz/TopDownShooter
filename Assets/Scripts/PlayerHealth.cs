using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private float flashDuration = 0.2f;

    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    public bool IsDead => currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (IsDead &&
            Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }

    // Can paketinden can kazanma
    public bool Heal(int amount)
    {
        if (IsDead || currentHealth >= maxHealth)
            return false;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        return true;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        // Hasar alma sesi
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        // Hasar alınca kısa süre pembe yan
        if (spriteRenderer != null)
        {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);

            flashCoroutine = StartCoroutine(FlashPink());
        }

        if (IsDead)
        {
            Debug.Log("GAME OVER!");
        }
    }

    private IEnumerator FlashPink()
    {
        spriteRenderer.color = Color.magenta;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }

    private void OnGUI()
    {
        // Can göstergesi
        GUIStyle healthStyle = new GUIStyle(GUI.skin.label);
        healthStyle.fontSize = 24;
        healthStyle.normal.textColor = Color.white;

        GUI.Label(
            new Rect(20, 20, 250, 40),
            "Can: " + currentHealth + " / " + maxHealth,
            healthStyle
        );

        // Ölüm ekranı
        if (IsDead)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 40;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.red;

            GUI.Label(
                new Rect(
                    Screen.width / 2 - 200,
                    Screen.height / 2 - 80,
                    400,
                    80
                ),
                "GAME OVER",
                style
            );

            GUIStyle restartStyle = new GUIStyle(GUI.skin.label);
            restartStyle.fontSize = 20;
            restartStyle.alignment = TextAnchor.MiddleCenter;

            GUI.Label(
                new Rect(
                    Screen.width / 2 - 200,
                    Screen.height / 2,
                    400,
                    40
                ),
                "Press R to Restart",
                restartStyle
            );
        }
    }
}