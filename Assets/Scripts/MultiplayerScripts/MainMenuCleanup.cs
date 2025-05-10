using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MainMenuCleanup : MonoBehaviour
{
    private void Awake()
    {
        if(NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton);
        }

        if(MultiplayerManager.Instance != null)
        {
            Destroy(MultiplayerManager.Instance.gameObject);
        }

        if(AntipaMuseumLobby.Instance != null) 
        {
            Destroy(AntipaMuseumLobby.Instance.gameObject);
        }
    }
}
