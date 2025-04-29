using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // �ڽ��� ���� �� �� �ִ� ����ƽ ����

    public static GameManager Instance { get { return gameManager; } } // ������ �ܺη� ������ �� �ִ� ������Ƽ

    private int currentScore = 0;

    private void Awake()
    {
        gameManager = this; // ���� ������ ��ü�� ����
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
    }
}
