using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionRange = 7f;
    public TextMeshProUGUI interactionText;
    public GameObject interactionMenu;
    public LayerMask exhibitLayer;

    private GameObject currentExhibit;

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
            }
        }
        else
        {
            currentExhibit = null;
            interactionText.gameObject.SetActive(false);
            interactionMenu.SetActive(false);
        }

        if (interactionMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            interactionMenu.SetActive(false);

        }
    }
}
