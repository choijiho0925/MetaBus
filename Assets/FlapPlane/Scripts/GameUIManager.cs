using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameScoreText;
    public TextMeshProUGUI bestGameScore;

    public Image restartPanel;

    public GameObject descriptionPanel;

    public static bool isPlaying = true;

    void Start()
    {
        if (isPlaying)
        {
            descriptionPanel.SetActive(true);
        } 
        restartPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown && isPlaying == true)
        {
            descriptionPanel.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void SetRestart()
    {
        restartPanel.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        gameScoreText.text = score.ToString();
    }
}
