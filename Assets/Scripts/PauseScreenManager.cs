using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void PauseGame()
    {
        _panel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        _panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        _panel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
