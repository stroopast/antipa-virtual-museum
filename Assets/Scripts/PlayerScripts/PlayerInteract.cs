using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEditor;
using UnityEngine;


public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionRange = 7f;
    public TextMeshProUGUI interactText;
    public LayerMask exhibitLayer;
    public GameObject MainMenu;
    public GameObject pauseMenuUI;
    public GameObject TrophyPopUp;
    public GameObject AchievementsMenu;

    public ExhibitMainMenuUI exhibitMainMenu;
    public ExhibitInfoMenuUI exhibitInfoMenu;
    public ExhibitQuizMenuUI exhibitQuizMenu;

    private GameObject currentExhibit;

    private void Start()
    {
        InitiateScript();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        bool hitExhibit = Physics.Raycast(ray, out hit, interactionRange, exhibitLayer);

        if (hitExhibit)
        {
            currentExhibit = hit.collider.gameObject;

            if(exhibitMainMenu.gameObject.activeSelf || exhibitInfoMenu.gameObject.activeSelf || 
                exhibitQuizMenu.gameObject.activeSelf || pauseMenuUI.gameObject.activeSelf ||
                TrophyPopUp.gameObject.activeSelf || MainMenu.gameObject.activeSelf || AchievementsMenu.gameObject.activeSelf)
            {
                interactText.gameObject.SetActive(false);
            }
            else
            {
                interactText.gameObject.SetActive(true);
            }
            
            if (Input.GetKeyDown(KeyCode.E) && !pauseMenuUI.gameObject.activeSelf && !exhibitQuizMenu.gameObject.activeSelf && !exhibitInfoMenu.gameObject.activeSelf)
            {
                MainMenu.SetActive(true);
                interactText.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Exhibit exhibit = currentExhibit.GetComponent<Exhibit>();
                Quiz quiz = currentExhibit.GetComponent<Quiz>();

                if (exhibit != null)
                {
                    exhibitMainMenu.LoadExhibitData(exhibit.data);
                    exhibitInfoMenu.LoadExhibitAdditionalInfo(exhibit.data);
                    exhibitQuizMenu.LoadQuizData(quiz.quiz, exhibit.data);
                }
            }
        }
        else
        {
            currentExhibit = null;
            interactText.gameObject.SetActive(false);
        }
    }

    void InitiateScript()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.name == "Main Camera" && obj.scene.IsValid())
            {
                playerCamera = obj.GetComponent<Camera>();
            }
            if (obj.name == "InteractText" && obj.scene.IsValid())
            {
                interactText = obj.GetComponent<TextMeshProUGUI>();
            }
            if (obj.name == "ExhibitMainMenuUI" && obj.scene.IsValid())
            {
                MainMenu = obj;
                exhibitMainMenu = obj.GetComponent<ExhibitMainMenuUI>();
            }
            if (obj.name == "PauseMenuUI" && obj.scene.IsValid())
            {
                pauseMenuUI = obj;
            }
            if (obj.name == "WinTrophyMenu" && obj.scene.IsValid())
            {
                TrophyPopUp = obj;
            }
            if (obj.name == "AchievementsMenuUI" && obj.scene.IsValid())
            {
                AchievementsMenu = obj;
            }
            if (obj.name == "ExhibitInfoMenuUI" && obj.scene.IsValid())
            {
                exhibitInfoMenu = obj.GetComponent<ExhibitInfoMenuUI>();
            }
            if (obj.name == "ExhibitQuizMenuUI" && obj.scene.IsValid())
            {
                exhibitQuizMenu = obj.GetComponent<ExhibitQuizMenuUI>();
            }
        }

        exhibitLayer = LayerMask.GetMask("Exhibit");
    }
}
