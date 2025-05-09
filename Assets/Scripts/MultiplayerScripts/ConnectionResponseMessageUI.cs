using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionResponseMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MessageText;
    [SerializeField] private Button CloseButton;
    private void Awake()
    {
        CloseButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        MultiplayerManager.Instance.OnFailedToJoinGame += MultiplayerManager_OnFailedToJoinGame;

        Hide();
    }
    private void MultiplayerManager_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        Show();

        MessageText.text = NetworkManager.Singleton.DisconnectReason;

        if(MessageText.text == "")
        {
            MessageText.text = "Failed to connect";
        }

        NetworkManager.Singleton.Shutdown();
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
        MultiplayerManager.Instance.OnFailedToJoinGame -= MultiplayerManager_OnFailedToJoinGame;
    }
}
