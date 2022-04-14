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
        var minutes = String.Format("{0:00}", TimeSpan.FromSeconds(elapsedTime).Minutes);
        var seconds = String.Format("{0:00}", TimeSpan.FromSeconds(elapsedTime).Seconds);
        var millis = String.Format("{0:00}", (int)TimeSpan.FromSeconds(elapsedTime).Milliseconds / 10);
        _minutesText.text = minutes;
        _secondsText.text = seconds;
        _millisText.text = millis;
    }
}
