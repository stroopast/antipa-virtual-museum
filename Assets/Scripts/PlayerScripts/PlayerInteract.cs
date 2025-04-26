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
    public LayerMask exhibitLayer;
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject TrophyPopUp;
    public GameObject AchievementsMenu;

    public ExhibitMainMenu exhibitMainMenu;
    public ExhibitInfoMenu exhibitInfoMenu;
    public ExhibitQuizMenu exhibitQuizMenu;

    private GameObject currentExhibit;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        bool hitExhibit = Physics.Raycast(ray, out hit, interactionRange, exhibitLayer);

        if (hitExhibit)
        {
            currentExhibit = hit.collider.gameObject;

            if(exhibitMainMenu.gameObject.activeSelf || exhibitInfoMenu.gameObject.activeSelf || 
                exhibitQuizMenu.gameObject.activeSelf || PauseMenu.gameObject.activeSelf ||
                TrophyPopUp.gameObject.activeSelf || MainMenu.gameObject.activeSelf || AchievementsMenu.gameObject.activeSelf)
            {
                interactionText.gameObject.SetActive(false);
            }
            else
            {
                interactionText.gameObject.SetActive(true);
            }
            
            if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.gameObject.activeSelf)
            {
                MainMenu.SetActive(true);
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
    }
}
