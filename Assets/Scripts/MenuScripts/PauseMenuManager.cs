using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject SaveMenu;
    [SerializeField] private GameObject LoadMenu;

    [SerializeField] private TextMeshProUGUI PlayerNameField;

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
        HelperFunctions.LockCursor();
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
    private void OnEnable()
    {
        string name = PlayerPrefs.GetString("PlayerName", "Vizitator");
        PlayerNameField.text = name;
    }
}
