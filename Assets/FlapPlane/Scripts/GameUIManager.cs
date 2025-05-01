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
    UIState currentState = UIState.Home;

    public HomeUI homeUI;
    public GameUI gameUI;
    public ScoreUI scoreUI;

    public static GameUIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
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

    private void OnDestroy()
    {
        Instance = null;
    }
}
