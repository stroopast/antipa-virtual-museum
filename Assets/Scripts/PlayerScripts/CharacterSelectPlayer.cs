using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyGameObject;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Button kickBtn;
    [SerializeField] private TextMeshPro playerNameText;

    private void Awake()
    {
        kickBtn.onClick.AddListener(() =>
        {
            PlayerData playerData = MultiplayerManager.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            AntipaMuseumLobby.Instance.KickPlayer(playerData.playerId.ToString());
            MultiplayerManager.Instance.KickPlayer(playerData.clientId);
        });
    }

    private void Start()
    {
        MultiplayerManager.Instance.OnPlayerDataNetworkListChanged += MultiplayerManager_OnPlayerDataNetworkListChanged;
        CharacterSelectReady.Instance.OnReadyChanged += CharacterSelectReady_OnReadyChanged;

        kickBtn.gameObject.SetActive(NetworkManager.Singleton.IsServer);

        if(playerIndex == 0)
        {
            kickBtn.gameObject.SetActive(false);
        }

        UpdatePlayer();
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }
    private void MultiplayerManager_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        if(MultiplayerManager.Instance.IsPlayerIndexConnected(playerIndex))
        {
            Show();

            PlayerData playerData = MultiplayerManager.Instance.GetPlayerDataFromPlayerIndex(playerIndex);

            readyGameObject.SetActive(CharacterSelectReady.Instance.IsPlayerReady(playerData.clientId));

            playerNameText.text = playerData.playerName.ToString();

            playerVisual.SetPlayerGender(MultiplayerManager.Instance.GetPlayerGender(playerData.genderId));
        }
        else
        {
            Hide();
        }
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
        MultiplayerManager.Instance.OnPlayerDataNetworkListChanged -= MultiplayerManager_OnPlayerDataNetworkListChanged;
    }
}
