using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI playerName;

    private void Start()
    {
        string name = PlayerPrefs.GetString("PlayerName", "Vizitator");
        playerName.text = name;
    }
}
