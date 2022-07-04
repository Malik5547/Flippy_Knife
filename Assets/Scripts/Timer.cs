using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{

    private TextMeshProUGUI _timerText;
    private float _currentTime;
    private bool paused = false;


    private void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        _currentTime = 0;
        DisplayTime();
    }

    private void Update()
    {
        if (!paused)
        {
            _currentTime += Time.deltaTime;
            DisplayTime();
        }
    }

    private void DisplayTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _timerText.SetText(time.ToString(@"mm\:ss"));
    }
    
    public void PauseTimer()
    {
        paused = true;
    }

    public void ResumeTimer()
    {
        paused = false;
    }

    public void ResetTimer()
    {
        _currentTime = 0;
    }

}
