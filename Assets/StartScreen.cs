using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0.05f;
    }

    public void StartGame()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
