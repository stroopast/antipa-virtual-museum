using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject SaveMenu;
    public GameObject LoadMenu;
    
    public void OpenSaveMenu()
    {
        SaveMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenLoadMenu()
    {
        LoadMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ReturnBackToGame()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    public void BackToStartMenu()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Destroy(AchievementManager.Instance.gameObject);
        Destroy(SaveManager.Instance.gameObject);
        Destroy(player);
        SceneManager.LoadScene("StartScene");
    }
}
