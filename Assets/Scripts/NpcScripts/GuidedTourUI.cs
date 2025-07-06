using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidedTourUI : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    private void Awake()
    {
        yesBtn.onClick.AddListener(() =>
        {
            if (GameModeManager.Instance.GetGameMode() == 1)
            {
                npc.gameObject.GetComponent<NpcController>().RequestStartTourFromClient();
            }
            else
            {
                npc.gameObject.GetComponent<NpcController>().TriggerGuidedTour();
            }
            gameObject.SetActive(false);
            HelperFunctions.LockCursor();
        });

        noBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            HelperFunctions.LockCursor();
        });
    }
}
