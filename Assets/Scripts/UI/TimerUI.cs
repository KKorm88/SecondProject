using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public event Action TimeEnd;

    [SerializeField]
    private TextMeshProUGUI _outputText;
    private string _format;

    [field: SerializeField]
    public float GameDurationSecond { get; private set; }

    public float TimerSecond { get; private set; }

    private bool _timerEnd;

    private bool _isRunning;

    private void Start()
    {
        _format = _outputText.text;
        _timerEnd = false;

        _isRunning = false;

        int totalSeconds = (int)GameDurationSecond;
        _outputText.text = string.Format(_format, totalSeconds / 60, totalSeconds % 60);
    }

    public void StartTimer()
    {
        TimerSecond = 0f;
        _timerEnd = false;
        _isRunning = true;
        int time = (int)GameDurationSecond;
        _outputText.text = string.Format(_format, time / 60, time % 60);
    }

    private void Update()
    {
        //if (_timerEnd)
        if (!_isRunning || _timerEnd)
            return;

        TimerSecond += Time.deltaTime;
        if (TimerSecond >= GameDurationSecond)
        {
            TimeEnd?.Invoke();
            _timerEnd = true;
            _isRunning = false;
        }
        int time = (int)(GameDurationSecond - TimerSecond);
        _outputText.text = string.Format(_format, time / 60, time % 60);
    }
}
