using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private float flashDuration = 0.2f;

    // UI (Canvas): Herzen
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private GameObject gameOverPanel;

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

        UpdateHearts();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
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
        UpdateHearts();
        return true;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHearts();

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

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }
    }

    private IEnumerator FlashPink()
    {
        spriteRenderer.color = Color.magenta;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }

    private void UpdateHearts()
    {
        if (hearts == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
