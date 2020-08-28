using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config parameters
    [Range(0.25f,4)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 50;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoPlay = false;
    [SerializeField] float minBallSpeedPerUnit = 2f;

    // state varables
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } 
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnLevelWasLoaded(int level)
    {
        UpdateScoreText();
    }
    // Update is called once per frame
    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        UpdateScoreText();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool isAutoPlayEnabled()
    {
        return autoPlay;
    }
    public float GetMinimumBallSpeedPerUnit()
    {
        return minBallSpeedPerUnit;
    }
}
