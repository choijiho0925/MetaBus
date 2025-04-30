using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // �ڽ��� ���� �� �� �ִ� ����ƽ ����

    public static GameManager Instance { get { return gameManager; } } // ������ �ܺη� ������ �� �ִ� ������Ƽ

    private int currentScore = 0;

    GameUIManager gameUIManager;

    public GameUIManager GameUIManager { get { return gameUIManager; } }

    private void Awake()
    {
        gameManager = this; // ���� ������ ��ü�� ����
        gameUIManager = FindObjectOfType<GameUIManager>();
        if(GameUIManager.isPlaying == true)
        {
            Time.timeScale = 0.0f;
        }
    }

    private void Start()
    {
        gameUIManager.UpdateScore(0);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameUIManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("FlapPlane");
        gameUIManager.descriptionPanel.SetActive(false);
        GameUIManager.isPlaying = false;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        gameUIManager.UpdateScore(currentScore);
    }
}
