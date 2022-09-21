using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _minutesText;
    [SerializeField] private TextMeshProUGUI _secondsText;
    [SerializeField] private TextMeshProUGUI _millisText;

    void Update()
    {
        var elapsedTime = Time.timeSinceLevelLoad;
        var minutes = string.Format("{0:00}", TimeSpan.FromSeconds(elapsedTime).Minutes);
        var seconds = string.Format("{0:00}", TimeSpan.FromSeconds(elapsedTime).Seconds);
        var millis = string.Format("{0:00}", (int)TimeSpan.FromSeconds(elapsedTime).Milliseconds * 0.1f);
        _minutesText.text = minutes;
        _secondsText.text = seconds;
        _millisText.text = millis;
    }
}
