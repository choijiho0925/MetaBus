using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : BaseUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public void SetUI()
    {
        scoreText.text = GameManager.Instance.CurrentScore.ToString();
        bestScoreText.text = GameManager.Instance.BestScore.ToString();
    }
}
