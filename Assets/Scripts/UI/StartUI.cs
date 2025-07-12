using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Button singleplayerBtn;
    [SerializeField] private Button multiplayerBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitGameBtn;

    [SerializeField] private GameObject startSinglePlayerUI;
    [SerializeField] private GameObject settingsUI;


    private void Awake()
    {
        singleplayerBtn.onClick.AddListener(() =>
        {
            GameModeManager.Instance.SetGameMode(0);
            gameObject.SetActive(false);
            startSinglePlayerUI.gameObject.SetActive(true);
        });

        multiplayerBtn.onClick.AddListener(() =>
        {
            GameModeManager.Instance.SetGameMode(1);
            SceneManager.LoadScene("LobbyScene");
        });

        settingsBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            settingsUI.gameObject.SetActive(true);
        });
    }
}
