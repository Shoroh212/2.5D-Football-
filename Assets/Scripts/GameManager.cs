using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using System.ComponentModel.Design.Serialization;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float _timeLeft = 120f; // 2 минуты
    private bool _isRunning = true;



    [SerializeField] private GameObject _endGamePanel;

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
            SceneManager.LoadScene(0);

        }

        UpdateTimerUI();
    }

    public async Task RestartGameAsync()
    {
        await Task.Delay(2000); 
        _isRunning = true;
        _timeLeft = 120f;
        _endGamePanel.SetActive(false);
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        await Task.Delay(100);
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(_timeLeft / 60);
        int seconds = Mathf.FloorToInt(_timeLeft % 60);

        timerText.text = $"{minutes:0}:{seconds:00}";
    }
}
