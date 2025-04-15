using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEditor;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionRange = 7f;
    public TextMeshProUGUI interactionText;
    public GameObject mainMenu;
    public GameObject infoMenu;
    public GameObject quizMenu;
    public LayerMask exhibitLayer;

    private GameObject currentExhibit;

    public ExhibitMainMenu exhibitMainMenu;
    public ExhibitInfoMenu exhibitInfoMenu;
    public ExhibitQuizMenu exhibitQuizMenu;

    public GameObject AchievementsMenu;
    private void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        bool hitExhibit = Physics.Raycast(ray, out hit, interactionRange, exhibitLayer);

        if (hitExhibit)
        {
            currentExhibit = hit.collider.gameObject;
            if (!mainMenu.activeSelf)
            {
                interactionText.gameObject.SetActive(true);
            }

            if(exhibitMainMenu.gameObject.activeSelf || exhibitInfoMenu.gameObject.activeSelf || exhibitQuizMenu.gameObject.activeSelf)
            {
                //nothing
                interactionText.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                mainMenu.SetActive(true);
                interactionText.gameObject.SetActive(false);
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
            interactionText.gameObject.SetActive(false);
        }

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            AchievementsMenu.gameObject.SetActive(true);
        }
    }
}
