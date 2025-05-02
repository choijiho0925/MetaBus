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

public class FlapPlaneUIManager : MonoBehaviour
{
    UIState currentState = UIState.Home;

    public FlapPlaneHomeUI homeUI;
    public FlapPlaneGameUI gameUI;
    public FlapPlaneResultUI resultUI;

    public static FlapPlaneUIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
        if (FlapPlaneManager.isStart == true)
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
        resultUI?.SetActive(currentState);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
