using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject fakeDoor;
    public GameObject realDoor;
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void UIEnable()
    {
        transform.Find("UI").gameObject.SetActive(true);
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void fakeDoorOpen()
    {
        fakeDoor.SetActive(true);
    }
    public void fakeDoorClose()
    {
        fakeDoor.SetActive(false);
    }
    public void realDoorOpen()
    {
        realDoor.SetActive(true);
    }
    public void realDoorClose()
    {
        realDoor.SetActive(false);
    }
    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
