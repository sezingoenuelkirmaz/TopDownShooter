using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Skor: " + score);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        GUI.Label(
            new Rect(20, 50, 250, 40),
            "Skor: " + score,
            style
        );
    }
}