using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcController : NetworkBehaviour
{
    [SerializeField] private Transform[] checkpoints;
    [SerializeField] private GameObject NpcQuizUI;
    [SerializeField] private GameObject NpcQuiz2UI;
    //[SerializeField] private TextMeshProUGUI waitingText;
    private NavMeshAgent agent;
    private Animator animator;
    private int currentCheckpoint = 0;

    private void Update()
    {
        //if (GameModeManager.Instance.GetGameMode() == 1)
        //{
        //    if (currentCheckpoint == 0 && NpcQuizUI.activeSelf && NpcQuiz2UI.activeSelf)
        //    {
        //        waitingText.text = "Așteaptă ca ceilalti jucători sa termine testul";
        //    }
        //    else
        //    {
        //        waitingText.text = "";
        //    }
        //}
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void RequestStartTourFromClient()
    {
        if (IsOwner || IsClient)
        {
            RequestStartTourServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestStartTourServerRpc()
    {
        TriggerGuidedTour();
    }

    public void TriggerGuidedTour()
    {
        //if (!IsServer) return;

        gameObject.layer = 7;
        StartCoroutine(WaitForCheckpoint());
    }

    private IEnumerator WaitForCheckpoint()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            currentCheckpoint = i;
            agent.SetDestination(checkpoints[i].position);
            animator.SetBool("isWalking", true);

            while (agent.pathPending || agent.remainingDistance > 0.2f)
            {
                yield return null;
            }

            animator.SetBool("isWalking", false);

            if(i == 0)
            {
                if(GameModeManager.Instance.GetGameMode() == 1)
                {
                    ShowQuiz1ClientRpc();
                    yield return new WaitForSeconds(10f);
                    HideQuiz1ClientRpc();
                    yield return new WaitForSeconds(1f);

                    ShowQuiz2ClientRpc();
                    yield return new WaitForSeconds(10f);
                    HideQuiz2ClientRpc();
                    HelperFunctions.LockCursor();
                }
                else
                {
                    NpcQuizUI.gameObject.SetActive(true);
                    yield return new WaitForSeconds(10f);
                    NpcQuizUI.gameObject.SetActive(false);
                    yield return new WaitForSeconds(1f);
                    if(!NpcQuiz2UI.gameObject.activeSelf)
                    {
                        NpcQuiz2UI.gameObject.SetActive(true);
                    }
                    yield return new WaitForSeconds(10f);
                    NpcQuiz2UI.gameObject.SetActive(false);
                    HelperFunctions.LockCursor();
                }
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }

            if (CheckPositionEquivalence(checkpoints[10]))
            {
                gameObject.layer = 8; // Set layer back to IdleNpc when tour ends
            }
        }
    }


    private bool CheckPositionEquivalence(Transform pos)
    {
        if (pos == null) return false;

        Vector3 currentPos = transform.position;

        float distanceXZ = Vector2.Distance(
            new Vector2(currentPos.x, currentPos.z),
            new Vector2(pos.position.x, pos.position.z)
        );

        if (distanceXZ < 0.1f)
        {
            return true;
        }

        return false;
    }

    [ClientRpc]
    private void ShowQuiz1ClientRpc()
    {
        NpcQuizUI.gameObject.SetActive(true);
    }

    [ClientRpc]
    private void HideQuiz1ClientRpc()
    {
        NpcQuizUI.gameObject.SetActive(false);
    }

    [ClientRpc]
    private void ShowQuiz2ClientRpc()
    {
        NpcQuiz2UI.gameObject.SetActive(true);
    }

    [ClientRpc]
    private void HideQuiz2ClientRpc()
    {
        NpcQuiz2UI.gameObject.SetActive(false);
    }
}
