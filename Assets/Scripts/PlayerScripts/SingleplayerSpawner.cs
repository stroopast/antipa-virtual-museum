using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Singleplayer : MonoBehaviour
{
    public GameObject playerPrefab;

    void Awake()
    {
        if (playerPrefab != null && GameModeManager.Instance.GetGameMode() == 0)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
    }
}
