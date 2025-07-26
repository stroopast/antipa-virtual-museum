using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private GameObject minimapUI;

    void Update()
    {
        GameObject[] menuObjects = GameObject.FindGameObjectsWithTag("Menu");

        bool isMenuActive = false;

        foreach (GameObject menu in menuObjects)
        {
            if (menu.activeInHierarchy)
            {
                isMenuActive = true;
                break;
            }
        }

        minimapUI.SetActive(!isMenuActive);
    }
}
