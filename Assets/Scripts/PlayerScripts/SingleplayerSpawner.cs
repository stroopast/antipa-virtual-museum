using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SingleplayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform manPrefab;
    [SerializeField] private Transform womanPrefab;

    private void Awake()
    {
        if (GameModeManager.Instance.GetGameMode() == 1) return;

        switch(GameModeManager.Instance.GetPlayerGender())
        {
            case "female":
                Instantiate(womanPrefab, transform.position, Quaternion.identity);
                break;
            case "male":
                Instantiate(manPrefab, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
