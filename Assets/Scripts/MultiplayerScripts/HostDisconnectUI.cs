using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HostDisconnectUI : MonoBehaviour
{
    [SerializeField] private Button PlayAgainButton;

    private void Awake()
    {
        PlayAgainButton.onClick.AddListener(() => {
            SceneManager.LoadScene("StartScene");
        });
    }


    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        Hide();
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        Debug.Log($"Client {clientId} disconnected. Server ID is {NetworkManager.ServerClientId}");
        if (clientId == NetworkManager.ServerClientId)
        {
            Debug.Log("Host disconnected. Showing disconnect UI.");
            // Server is shutting down
            Show();
            HelperFunctions.UnlockCursor();
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
        NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
    }
}
