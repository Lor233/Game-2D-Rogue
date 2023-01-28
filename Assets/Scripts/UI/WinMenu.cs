using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject MenuUI;

    public void Resume()
    {
        SoundManager.instance.PlayBattle();
        MenuUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        StartCoroutine(PauseCo(0.5f));
    }

    IEnumerator PauseCo(float time)
    {
        yield return new WaitForSeconds(time);
        MenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        SoundManager.instance.PlayWin();
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }

}
