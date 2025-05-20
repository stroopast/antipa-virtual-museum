using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button createLobbyBtn;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button quickJoinBtn;
    [SerializeField] private Button joinCodeBtn;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private Transform lobbyContainer;
    [SerializeField] private Transform lobbyTemplate;

    private void Awake()
    {
        mainMenuBtn.onClick.AddListener(() =>
        {
            AntipaMuseumLobby.Instance.LeaveLobby();
            SceneManager.LoadScene("InitialScene");
        });

        createLobbyBtn.onClick.AddListener(() =>
        {
            lobbyCreateUI.Show();
        });

        quickJoinBtn.onClick.AddListener(() =>
        {
            AntipaMuseumLobby.Instance.QuickJoin();
        });

        joinCodeBtn.onClick.AddListener(() =>
        {
            AntipaMuseumLobby.Instance.JoinWithCode(joinCodeInputField.text);
        });

        lobbyTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        playerNameInputField.text = MultiplayerManager.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) =>
        {
            MultiplayerManager.Instance.SetPlayerName(newText);
        });

        AntipaMuseumLobby.Instance.OnLobbyListChanged += AntipaMuseumLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void AntipaMuseumLobby_OnLobbyListChanged(object sender, AntipaMuseumLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach(Transform child in lobbyContainer)
        {
            if (child == lobbyTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Lobby lobby in lobbyList)
        {
            Transform lobbyTransform = Instantiate(lobbyTemplate, lobbyContainer);
            lobbyTransform.gameObject.SetActive(true);
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }

    private void OnDestroy()
    {
        AntipaMuseumLobby.Instance.OnLobbyListChanged -= AntipaMuseumLobby_OnLobbyListChanged;
    }
}
