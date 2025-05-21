using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyNameText;

    private Lobby lobby;

    private int lobbyPlayers;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            AntipaMuseumLobby.Instance.JoinWithId(lobby.Id);
        });
    }

    private void Update()
    {
        CalculateLobbyPlayers();
        lobbyNameText.text = lobby.Name + " (Jucători: " + lobbyPlayers + "/" + lobby.MaxPlayers + ")";
    }

    public void SetLobby(Lobby lobby)
    {
        this.lobby = lobby;
    }

    private void CalculateLobbyPlayers()
    {
        lobbyPlayers = lobby.MaxPlayers - lobby.AvailableSlots;
    }
}
