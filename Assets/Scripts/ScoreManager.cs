
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
        GUI.Label(
            new Rect(20, 50, 200, 30),
            "Skor: " + score
        );
    }
}
