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
    public GameObject interactionMenu;
    public LayerMask exhibitLayer;

    private GameObject currentExhibit;

    public InteractionMenu exhibitMenu;
    void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        bool hitExhibit = Physics.Raycast(ray, out hit, interactionRange, exhibitLayer);

        if (hitExhibit)
        {
            currentExhibit = hit.collider.gameObject;
            if (!interactionMenu.activeSelf)
            {
                interactionText.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactionMenu.SetActive(true);
                interactionText.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Exhibit exhibit = currentExhibit.GetComponent<Exhibit>();

                if (exhibit != null)
                {
                    exhibitMenu.LoadExhibitData(exhibit.data);
                }
            }
        }
        else
        {
            currentExhibit = null;
            interactionText.gameObject.SetActive(false);
        }

        if (interactionMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            interactionMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(!interactionMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
