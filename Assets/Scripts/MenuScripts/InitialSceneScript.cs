using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InitialSceneScript : MonoBehaviour
{
    [Header("Single Player related buttons")]
    public GameObject SinglePlayerBtn;
    public GameObject LoadGameBtn;
    public GameObject NewGameBtn;
    public GameObject EnterGameBtn;
    public GameObject GenerateRandIDBtn;
    public TMP_InputField playerNameInputField;

    void Start()
    {
        InitiateStartingScene();
    }

    private void InitiateStartingScene()
    {
        NewGameBtn.gameObject.SetActive(false);
        LoadGameBtn.gameObject.SetActive(false);
        EnterGameBtn.gameObject.SetActive(false);
        GenerateRandIDBtn.gameObject.SetActive(false);
        playerNameInputField.gameObject.SetActive(false);
    }

    public void PressSinglePlayerButton()
    {
        SinglePlayerBtn.gameObject.SetActive(false);
        NewGameBtn.gameObject.SetActive(true);
        LoadGameBtn.gameObject.SetActive(true);
    }

    public void PressNewGameButton()
    {
        NewGameBtn.gameObject.SetActive(false);
        LoadGameBtn.gameObject.SetActive(false);
        EnterGameBtn.gameObject.SetActive(true);
        GenerateRandIDBtn.gameObject.SetActive(true);
        playerNameInputField.gameObject.SetActive(true);
    }

    public void PressEnterGameButton()
    {
        string playerName = playerNameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }

    public void PressGenerateRandIDButton()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        string id = "";
        for (int i = 0; i < 12; i++)
        {
            int index = Random.Range(0, chars.Length);
            id += chars[index];
        }
        PlayerPrefs.SetString("PlayerName", id);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}
