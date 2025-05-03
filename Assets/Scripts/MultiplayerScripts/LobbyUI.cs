using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button CreateGameBtn;
    [SerializeField] private Button JoinGameBtn;

    private void Awake()
    {
        CreateGameBtn.onClick.AddListener(() =>
        {
            MultiplayerManager.Instance.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene("CharacterSelectScene", LoadSceneMode.Single);
        });

        JoinGameBtn.onClick.AddListener(() =>
        {
            MultiplayerManager.Instance.StartClient();
        });
    }
}
