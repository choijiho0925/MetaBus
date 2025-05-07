using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class JumpMapUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText; // UI에 표시할 시간 텍스트

    private float playTime; // 게임 진행 시간

    public static bool isGamePlaying = false;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            isGamePlaying = true;
        }

        if (isGamePlaying)
        {
            playTime += Time.deltaTime; // 게임 진행 시간 업데이트
            UpdateTimeText(); // UI 업데이트
        }
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(playTime / 60f);
        float seconds = playTime % 60f;
        timeText.text = $"{minutes:00}:{seconds:00.00}";
    }
}
