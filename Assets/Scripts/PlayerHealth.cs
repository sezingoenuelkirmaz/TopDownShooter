
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;

    private int currentHealth;

    public bool IsDead => currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
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

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        // Hasar alma sesi
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        // Oyuncu öldüğünde
        if (IsDead)
        {
            Debug.Log("GAME OVER!");
        }
    }

    private void OnGUI()
    {
        // Can göstergesi
        GUI.Label(
            new Rect(20, 20, 200, 30),
            "Can: " + currentHealth + " / " + maxHealth
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
