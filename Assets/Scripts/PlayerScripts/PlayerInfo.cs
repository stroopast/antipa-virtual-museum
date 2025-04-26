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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        string name = PlayerPrefs.GetString("PlayerName", "Vizitator");
        playerName.text = name;
    }

    public void UpdateNameOnLoad(string name)
    {
        playerName.text = name;
    }
}
