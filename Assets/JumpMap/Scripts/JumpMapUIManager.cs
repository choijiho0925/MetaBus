using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class JumpMapUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText; // UI�� ǥ���� �ð� �ؽ�Ʈ

    private float playTime; // ���� ���� �ð�

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
            playTime += Time.deltaTime; // ���� ���� �ð� ������Ʈ
            UpdateTimeText(); // UI ������Ʈ
        }
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(playTime / 60f);
        float seconds = playTime % 60f;
        timeText.text = $"{minutes:00}:{seconds:00.00}";
    }
}
