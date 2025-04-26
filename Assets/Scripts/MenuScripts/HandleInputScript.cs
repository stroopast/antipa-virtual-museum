using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HandleInputScript : MonoBehaviour
{
    public GameObject ExhibitMainMenu;
    public GameObject ExhibitInfoMenu;
    public GameObject ExhibitQuizMenu;
    public GameObject AchievementsMenu;
    public GameObject WinTrophyMenu;
    public GameObject PauseMenu;
    public GameObject SaveMenu;
    public GameObject LoadMenu;
    public List<GameObject> DeleteLoadPopUpMenus;

    private void Update()
    {
        HandleInput();
    }

    bool AreMenusActive()
    {
        GameObject[] menus = GameObject.FindGameObjectsWithTag("Menu");

        foreach (var menu in menus)
        {
            if (menu.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                PauseMenu.gameObject.SetActive(false);
            }
            // Check if other menus are active, if no active menus open pause menu
            else if (!AreMenusActive())
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PauseMenu.gameObject.SetActive(true);
            }

            if (ExhibitMainMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ExhibitMainMenu.gameObject.SetActive(false);
            }

            if (ExhibitInfoMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ExhibitInfoMenu.gameObject.SetActive(false);
            }

            if (ExhibitQuizMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ExhibitQuizMenu.GetComponent<ExhibitQuizMenu>().ExitQuiz();
            }

            if (AchievementsMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                AchievementsMenu.gameObject.SetActive(false);
            }

            if (WinTrophyMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                WinTrophyMenu.gameObject.SetActive(false);
            }

            if (SaveMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SaveMenu.gameObject.SetActive(false);
            }

            if (LoadMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                LoadMenu.gameObject.SetActive(false);
            }
            foreach (var menu in DeleteLoadPopUpMenus)
            {
                if(menu.gameObject.activeSelf)
                {
                    menu.gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            if(!AreMenusActive())
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                AchievementsMenu.gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(ExhibitQuizMenu.gameObject.activeSelf)
            {
                ExhibitQuizMenu.GetComponent<ExhibitQuizMenu>().CheckAnswer();
            }
        }
    }
}
