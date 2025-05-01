using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public enum UIState
{
    Home,
    Game,
    Score,
}

public class GameUIManager : MonoBehaviour
{
    static GameUIManager instance;

    public static GameUIManager Instance { get { return instance; } }

    UIState currentState = UIState.Home;

    public HomeUI homeUI;
    public GameUI gameUI;
    public ScoreUI scoreUI;

    private void Awake()
    {
        instance = this;
        if (GameManager.isStart == true)
        {
            ChangeState(UIState.Home);
        }
        else
        {
            ChangeState(UIState.Game);
        }
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }
}
