using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HandleInputScript : MonoBehaviour
{
    public List<GameObject> Menus = new List<GameObject>();

    private void Start()
    {
        FindAllMenus();
    }

    private void Update()
    {
        HandleInput();
    }

    public bool AreMenusActive()
    {
        foreach (var menu in Menus)
        {
            if (menu != null && menu.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    void FindAllMenus()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Menu") && obj.scene.IsValid())
            {
                Menus.Add(obj);
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool anyMenuClosed = false;

            // Close all active menus
            foreach (var menu in Menus)
            {
                if (menu != null && menu.activeSelf)
                {
                    if (menu.TryGetComponent(out ExhibitQuizMenuUI quizMenu))
                    {
                        quizMenu.ExitQuiz();
                    }
                    else
                    {
                        menu.SetActive(false);
                    }
                    anyMenuClosed = true;
                }
            }

            if (anyMenuClosed)
            {
                HelperFunctions.LockCursor();
            }
            else
            {
                // Open Pause Menu if nothing else was open
                var pauseMenu = Menus.Find(menu => menu.name == "PauseMenuUI");
                if (pauseMenu != null)
                {
                    HelperFunctions.UnlockCursor();
                    pauseMenu.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            if (!AreMenusActive())
            {
                var achievementsMenu = Menus.Find(menu => menu.name == "AchievementsMenuUI");
                if (achievementsMenu != null)
                {
                    HelperFunctions.UnlockCursor();
                    achievementsMenu.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            foreach (var menu in Menus)
            {
                if (menu != null && menu.activeSelf && menu.TryGetComponent(out ExhibitQuizMenuUI quizMenu))
                {
                    quizMenu.CheckAnswer();
                    break;
                }
            }
        }
    }
}
