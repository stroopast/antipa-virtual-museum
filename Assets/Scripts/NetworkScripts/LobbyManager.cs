using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] Button CreateLobbyBtn;
    [SerializeField] Button ListLobbiesBtn;

    private Lobby hostLobby;
    private float heartbeatTimer;
    private async void Start()
    {
        CreateButtonListeners();

        //await UnityServices.InitializeAsync();

        //AuthenticationService.Instance.SignedIn += () =>
        //{
        //    Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        //};
        //await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        //HandleLobbyHeartbeat();
    }

    //Used to keep the lobby active after 30 seconds of incativity
    private async void HandleLobbyHeartbeat()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if(heartbeatTimer < 0)
            {
                float heartbeatTimerMax = 15;
                heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }

    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int maxPlayers = 4;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            hostLobby = lobby;
            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.MaxPlayers);
        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOption = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            Debug.Log("Lobbies found: " + queryResponse.Results.Count);
            foreach(Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);
            Debug.Log("Joined lobby with code " + lobbyCode);
        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }



    private void CreateButtonListeners()
    {
        CreateLobbyBtn.onClick.AddListener(CreateLobby);
        ListLobbiesBtn.onClick.AddListener(ListLobbies);
    }
}
