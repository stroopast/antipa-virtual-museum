using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
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

    private void Awake()
    {
        mainMenuBtn.onClick.AddListener(() =>
        {
            AntipaMuseumLobby.Instance.LeaveLobby();
            SceneManager.LoadScene("StartScene");
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
    }

    private void Start()
    {
        playerNameInputField.text = MultiplayerManager.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) =>
        {
            MultiplayerManager.Instance.SetPlayerName(newText);
        });
    }
}
