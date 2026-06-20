using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float _timeLeft = 120f; // 2 минуты
    private bool _isRunning = true;

    private void Update()
    {
        if (!_isRunning)
            return;

        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0)
        {
            _timeLeft = 0;
            _isRunning = false;

            Debug.Log("Время вышло!");
            // Здесь можно завершить матч
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(_timeLeft / 60);
        int seconds = Mathf.FloorToInt(_timeLeft % 60);

        timerText.text = $"{minutes:0}:{seconds:00}";
    }
}
