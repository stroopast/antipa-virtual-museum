using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private TextMeshProUGUI PlayerNameField;

    private void Start()
    {
        FindPlayerNameTextField();
        string name = PlayerPrefs.GetString("PlayerName", "Vizitator");
        PlayerNameField.text = name;
    }

    private void FindPlayerNameTextField()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.name == "PlayerNameText" && obj.scene.IsValid())
            {
                PlayerNameField = obj.GetComponent<TextMeshProUGUI>();
            }
        }
    }
}
