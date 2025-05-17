using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    private short gameMode = 0; // 0 -> singleplayer    1 -> multiplayer
    private string playerName;
    private string playerGender;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        Debug.Log("NUME Jucator: " + playerName);
        Debug.Log("SEX Jucator: " + playerGender);
    }

    public void SetGameMode(short mode)
    {
        gameMode = mode;
    }

    public short GetGameMode()
    {
        return gameMode;
    }

    public void SetPlayerGender(string gender)
    {
        playerGender = gender;
    }

    public string GetPlayerGender()
    {
        return playerGender;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
