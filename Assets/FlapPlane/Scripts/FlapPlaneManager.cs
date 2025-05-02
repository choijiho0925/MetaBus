using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class FlapPlaneManager : MonoBehaviour
{
    private int currentScore = 0;
    private int bestScore = 0;
    
    public int CurrentScore { get => currentScore; }
    public int BestScore { get => bestScore; }

    public static bool IsStart { get; set; } = true; // 처음 시작 여부를 결정한다.
    public bool isGameOver = false; // 게임 오버 여부를 결정한다.

    private const string BestScoreKey = "BestScore";

    FlapPlaneUIManager gameUIManager;

    public static FlapPlaneManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        if (IsStart == true)
        {
            Time.timeScale = 0.0f;
        }
    }

    private void Start()
    {
        gameUIManager = FlapPlaneUIManager.Instance;

        bestScore = PlayerPrefs.GetInt(BestScoreKey);
        UpdateScore(currentScore);
    }

    public void UpdateScore(int score)
    {
        gameUIManager?.gameUI.SetUI(score);
    }

    public void GameOver()
    {
        isGameOver = true;
        if (bestScore < currentScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }
        gameUIManager?.resultUI.SetUI(currentScore, bestScore);
    }

    public void PlayGame()
    {

        Time.timeScale = 1.0f;
        gameUIManager?.ChangeState(UIState.Game);
        IsStart = false;
        if (isGameOver)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScore(currentScore);
    }

    private void OnDestroy()
    {
        Instance = null;        
    }
}