using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;

    public TextMeshProUGUI playerName;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitiateScript();
        string name = PlayerPrefs.GetString("PlayerName", "Vizitator");
        playerName.text = name;
    }

    public void UpdateNameOnLoad(string name)
    {
        playerName.text = name;
    }

    void InitiateScript()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.name == "PlayerNameText" && obj.scene.IsValid())
            {
                playerName = obj.GetComponent<TextMeshProUGUI>();
            }
        }
    }
}
