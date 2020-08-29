using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText = null;

    private void Start()
    {
        if (highScoreText) 
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
    }

    public void HandleHighScore()
    {
        int score = FindObjectOfType<GameSession>().GetCurrentScore();
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {
            SetHighScore(score);
        }
    }
}
