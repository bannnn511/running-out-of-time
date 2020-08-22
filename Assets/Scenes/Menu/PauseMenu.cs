using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool isPausing =false;
    public GameObject PauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
          Pause();
        }
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPausing = false;
        Camera.main.GetComponent<AudioSource>().UnPause();
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPausing = true;
          Camera.main.GetComponent<AudioSource>().Pause();
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
        Resume();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
