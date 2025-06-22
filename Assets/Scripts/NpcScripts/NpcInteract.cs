using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask idleNpcLayer;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private GameObject NpcMenuUI;

    private float interactionRange = 7f;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        bool hitNpc = Physics.Raycast(ray, out hit, interactionRange, idleNpcLayer);

        if (hitNpc)
        {
            interactText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(GameModeManager.Instance.GetGameMode() == 1)
                {
                    gameObject.GetComponent<NpcController>().RequestStartTourFromClient();
                }
                else
                {
                    gameObject.GetComponent<NpcController>().TriggerGuidedTour();
                }
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }
}
