using System.Collections;
using System.IO;
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
    public GameObject LoadGameMenu;
    public List<GameObject> DeleteLoadPopUpVect;
    public TMP_InputField playerNameInputField;

    [Header("MultiPlayer related buttons")]
    public GameObject MultiPlayerBtn;
    public GameObject CreateLobbyBtn;
    public GameObject ListLobbiesBtn;
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

    // Singleplayer buttons

    public void PressSinglePlayerButton()
    {
        SinglePlayerBtn.gameObject.SetActive(false);
        MultiPlayerBtn.gameObject.SetActive(false);
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

    public void PressLoadGameButton()
    {
        NewGameBtn.gameObject.SetActive(false);
        LoadGameBtn.gameObject.SetActive(false);
        LoadGameMenu.gameObject.SetActive(true);
    }

    public void LoadFromSlot(int slot)
    {
        if(File.Exists(SaveManager.Instance.GetSlotPath(slot)))
        {
            PlayerPrefs.SetInt("SlotToLoad", slot);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameScene");     
        }
        else
        {
            Debug.Log("Nu exista salvare pe acest slot!");
        }
    }

    public void PressRemoveSlotButton(int slot)
    {
        string path = SaveManager.Instance.GetSlotPath(slot);
        if (File.Exists(path))
        {
            LoadGameMenu.gameObject.SetActive(false);
            DeleteLoadPopUpVect[slot - 1].gameObject.SetActive(true);
        }
    }

    public void PressNoButtonOnLoadDelete(int slot)
    {
        LoadGameMenu.gameObject.SetActive(true);
        DeleteLoadPopUpVect[slot - 1].gameObject.SetActive(false);
    }



    // MultiPlayer buttons

    public void PressMultiPlayerButton()
    {
        SinglePlayerBtn.gameObject.SetActive(false);
        MultiPlayerBtn.gameObject.SetActive(false);
        CreateLobbyBtn.gameObject.SetActive(true);
        ListLobbiesBtn.gameObject.SetActive(true);
    }
}
