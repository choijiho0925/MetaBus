using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // 자신을 참조 할 수 있는 스태틱 변수

    public static GameManager Instance { get { return gameManager; } } // 변수를 외부로 가져갈 수 있는 프로퍼티

    private int currentScore = 0;

    private void Awake()
    {
        gameManager = this; // 가장 최초의 객체를 설정
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
