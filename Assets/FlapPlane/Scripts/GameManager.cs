using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // 자신을 참조 할 수 있는 스태틱 변수

    public static GameManager Instance { get { return gameManager; } } // 변수를 외부로 가져갈 수 있는 프로퍼티

    public static bool isStart = true; // 처음 시작 여부를 결정한다.

    private int currentScore = 0;
    private int bestScore = 0;

    public int CurrentScore { get => currentScore; }
    public int BestScore { get => bestScore; }

    private const string BestScoreKey = "BestScore";

    GameUIManager gameUIManager;

    public GameUIManager GameUIManager { get { return gameUIManager; } }

    private void Awake()
    {
        gameManager = this;

        gameUIManager = FindObjectOfType<GameUIManager>();

        if (isStart == true)
        {
            Time.timeScale = 0.0f;
        }
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt(BestScoreKey);
        UpdateScore(currentScore);
    }

    public void UpdateScore(int score)
    {
        GameUIManager.Instance.gameUI.SetUI(score);
    }

    public void GameOver()
    {
        if (bestScore < currentScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }
        GameUIManager.Instance.scoreUI.SetUI();
    }

    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        GameUIManager.Instance.ChangeState(UIState.Game);
        SceneManager.LoadScene("FlapPlane");
        isStart = false;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScore(currentScore);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FlapPlane");
        isStart = true;
    }
}