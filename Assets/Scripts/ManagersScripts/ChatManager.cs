using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Singleton;

    [SerializeField] GameObject chatUI;
    [SerializeField] GameObject chatMessagePrefab;
    [SerializeField] CanvasGroup chatContent;
    [SerializeField] TMP_InputField chatInput;

    void Awake()
    { ChatManager.Singleton = this; }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && GameModeManager.Instance.GetGameMode() == 1)
        {
            chatUI.gameObject.SetActive(true);
            HelperFunctions.UnlockCursor();
        }

        if (Input.GetKeyDown(KeyCode.Return) && GameModeManager.Instance.GetGameMode() == 1)
        {
            SendChatMessage(chatInput.text, MultiplayerManager.Instance.GetPlayerName());
            chatInput.text = "";
        }
    }

    public void SendChatMessage(string _message, string _fromWho = null)
    {
        if (string.IsNullOrWhiteSpace(_message)) return;

        string S = _fromWho + " > " + _message;
        SendChatMessageServerRpc(S);
    }

    void AddMessage(string msg)
    {
        GameObject CM = Instantiate(chatMessagePrefab, chatContent.transform);
        CM.GetComponent<TextMeshProUGUI>().text = msg;
    }

    [ServerRpc(RequireOwnership = false)]
    void SendChatMessageServerRpc(string message)
    {
        ReceiveChatMessageClientRpc(message);
    }

    [ClientRpc]
    void ReceiveChatMessageClientRpc(string message)
    {
        ChatManager.Singleton.AddMessage(message);
    }
}