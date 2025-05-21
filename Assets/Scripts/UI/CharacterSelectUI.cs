using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button readyBtn;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI lobbyCodeText;

    private void Awake()
    {
        mainMenuBtn.onClick.AddListener(() =>
        {
            AntipaMuseumLobby.Instance.LeaveLobby();
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene("InitialScene");
        });

        readyBtn.onClick.AddListener(() =>
        {
            CharacterSelectReady.Instance.SetPlayerReady();
        });
    }

    private void Start()
    {
        Lobby lobby = AntipaMuseumLobby.Instance.GetLobby();

        lobbyNameText.text = "Numele Camerei: " + lobby.Name;
        lobbyCodeText.text = "Codul Camerei: " + lobby.LobbyCode;
    }

}
