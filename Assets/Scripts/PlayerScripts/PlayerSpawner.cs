using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    void Awake()
    {
        if (playerPrefab != null && GameModeManager.Instance.isMultiplayer == false)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
    }
}
