using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    void Awake()
    {
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player Prefab not assigned in the inspector!");
        }
    }
}
