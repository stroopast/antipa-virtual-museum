using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class VirtualMuseumManager : NetworkBehaviour
{
    public static VirtualMuseumManager Instance { get; private set; }

    [SerializeField] private Transform playerPrefab;

    [SerializeField] private Transform playerPrefabMale;
    [SerializeField] private Transform playerPrefabFemale;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
        }
    }

    private void SceneManager_OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Transform playerTransform = Instantiate(playerPrefab);
            playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
    }
}
