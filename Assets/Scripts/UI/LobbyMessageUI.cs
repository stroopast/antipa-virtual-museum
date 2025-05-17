using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button CloseButton;
    private void Awake()
    {
        CloseButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        MultiplayerManager.Instance.OnFailedToJoinGame += MultiplayerManager_OnFailedToJoinGame;
        AntipaMuseumLobby.Instance.OnCreateLobbyStarted += AntipaMuseumLobby_OnCreateLobbyStarted;
        AntipaMuseumLobby.Instance.OnCreateLobbyFailed += AntipaMuseumLobby_OnCreateLobbyFailed;
        AntipaMuseumLobby.Instance.OnJoinStarted += AntipaMuseumLobby_OnJoinStarted;
        AntipaMuseumLobby.Instance.OnQuickJoinFailed += AntipaMuseumLobby_OnQuickJoinFailed;
        AntipaMuseumLobby.Instance.OnRegularJoinFailed += AntipaMuseumLobby_OnRegularJoinFailed;
        Hide();
    }

    private void AntipaMuseumLobby_OnJoinStarted(object sender, System.EventArgs e)
    {
        ShowMessage("Conectare la cameră...");
    }

    private void AntipaMuseumLobby_OnQuickJoinFailed(object sender, System.EventArgs e)
    {
        ShowMessage("Nu s-a putut găsi o cameră pentru alăturare rapidă!");
    }

    private void AntipaMuseumLobby_OnRegularJoinFailed(object sender, System.EventArgs e)
    {
        ShowMessage("Nu s-a putut conecta la cameră!");
    }

    private void AntipaMuseumLobby_OnCreateLobbyStarted(object sender, System.EventArgs e)
    {
        ShowMessage("Se creează cameră...");
    }

    private void AntipaMuseumLobby_OnCreateLobbyFailed(object sender, System.EventArgs e)
    {
        ShowMessage("Nu s-a putut creea o cameră!");
    }

    private void MultiplayerManager_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        if(NetworkManager.Singleton.DisconnectReason == "")
        {
            ShowMessage("Nu s-a putut conecta");
        }
        else
        {
            ShowMessage("NetworkManager.Singleton.DisconnectReason");
        }
        //NetworkManager.Singleton.Shutdown();
    }

    private void ShowMessage(string message)
    {
        Show();
        messageText.text = message;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        MultiplayerManager.Instance.OnFailedToJoinGame  -= MultiplayerManager_OnFailedToJoinGame;
        AntipaMuseumLobby.Instance.OnCreateLobbyStarted -= AntipaMuseumLobby_OnCreateLobbyStarted;
        AntipaMuseumLobby.Instance.OnCreateLobbyFailed  -= AntipaMuseumLobby_OnCreateLobbyFailed;
        AntipaMuseumLobby.Instance.OnJoinStarted        -= AntipaMuseumLobby_OnJoinStarted;
        AntipaMuseumLobby.Instance.OnQuickJoinFailed    -= AntipaMuseumLobby_OnQuickJoinFailed;
        AntipaMuseumLobby.Instance.OnRegularJoinFailed  -= AntipaMuseumLobby_OnRegularJoinFailed;
    }
}
