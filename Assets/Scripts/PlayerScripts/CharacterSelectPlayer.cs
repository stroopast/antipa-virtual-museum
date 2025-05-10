using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject ReadyGameObject;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Button KickBtn;

    private void Awake()
    {
        KickBtn.onClick.AddListener(() =>
        {
            PlayerData playerData = MultiplayerManager.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            MultiplayerManager.Instance.KickPlayer(playerData.clientId);
        });
    }

    private void Start()
    {
        MultiplayerManager.Instance.OnPlayerDataNetworkListChanged += Instance_OnPlayerDataNetworkListChanged;
        CharacterSelectReady.Instance.OnReadyChanged += CharacterSelectReady_OnReadyChanged;

        KickBtn.gameObject.SetActive(NetworkManager.Singleton.IsServer);

        UpdatePlayer();
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }
    private void Instance_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        if(MultiplayerManager.Instance.IsPlayerIndexConnected(playerIndex))
        {
            Show();

            PlayerData playerData = MultiplayerManager.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            ReadyGameObject.SetActive(CharacterSelectReady.Instance.IsPlayerReady(playerData.clientId));

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
}
