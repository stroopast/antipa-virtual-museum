using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public List<GameObject> otherMenus;
    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (var menu in otherMenus)
            {
                if(menu.gameObject.activeSelf)
                {
                    return;
                }
            }

            if(PauseMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                PauseMenu.gameObject.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PauseMenu.gameObject.SetActive(true);
            }
        }
    }
}
