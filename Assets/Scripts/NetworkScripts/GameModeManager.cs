using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public bool isMultiplayer = false;

    [SerializeField] Button MultiPlayerBtn;
    [SerializeField] Button SingleplayerBtn;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        MultiPlayerBtn.onClick.AddListener(OnClickMultiPlayerBtn);
        SingleplayerBtn.onClick.AddListener(OnClickSinglePlayerBtn);
    }

    private void OnClickMultiPlayerBtn()
    {
        isMultiplayer = true;
    }

    private void OnClickSinglePlayerBtn()
    {
        isMultiplayer = false;
    }
}
