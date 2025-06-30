using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcController : NetworkBehaviour
{
    [SerializeField] private Transform[] checkpoints;
    [SerializeField] private GameObject npcQuizUI;
    [SerializeField] private GameObject npcQuiz2UI;
    [SerializeField] private GameObject finalScoreUI;

    private NavMeshAgent agent;
    private Animator animator;
    private int currentCheckpoint = 0;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(-19.4799023f, 0.250999987f, -153.390015f);

        if (IsAtStartingPosition(targetPosition))
        {
            gameObject.layer = 8;
        }
        else
        {
            gameObject.layer = 7;
        }
    }

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void RequestStartTourFromClient()
    {
        TriggerGuidedTourServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void TriggerGuidedTourServerRpc(ServerRpcParams serverRpcParams = default)
    {
        StartCoroutine(WaitForCheckpoint());
    }

    public void TriggerGuidedTour()
    {
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

            if(i == 0) // when reaches the last checkpoint trigger quizzes (i == 9)
            {
                if (GameModeManager.Instance.GetGameMode() == 1)
                {
                    ShowQuiz1ClientRpc();
                    yield return new WaitForSeconds(20f); // timer for first test
                    HideQuiz1ClientRpc();
                    yield return new WaitForSeconds(2f);

                    if (!IsQuiz2Opened())
                    {
                        ShowQuiz2ClientRpc();
                    }
                    yield return new WaitForSeconds(80f); // Timer for second test
                    HideQuiz2ClientRpc();

                    if(!IsFinalScoreOpened())
                    {
                        ShowFinalScoreClientRpc();
                    }
                    HideFinalScoreClientRpc();

                    HelperFunctions.LockCursor();
                }
                else
                {
                    npcQuizUI.gameObject.SetActive(true);
                    yield return new WaitForSeconds(10f); // timer for first test
                    npcQuizUI.gameObject.SetActive(false);
                    yield return new WaitForSeconds(1f);

                    if (!IsQuiz2Opened())
                    {
                        npcQuiz2UI.gameObject.SetActive(true);
                    }
                    yield return new WaitForSeconds(80f); // Timer for second test
                    npcQuiz2UI.gameObject.SetActive(false);

                    if (!IsFinalScoreOpened())
                    {
                        finalScoreUI.gameObject.SetActive(true);
                    }
                    finalScoreUI.gameObject.SetActive(false);

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

    private bool IsAtStartingPosition(Vector3 target)
    {
        float tolerance = 0.1f; // small tolerance to allow float comparison
        return Vector3.Distance(transform.position, target) < tolerance;
    }

    [ClientRpc]
    private void ShowQuiz1ClientRpc()
    {
        npcQuizUI.gameObject.SetActive(true);
    }

    [ClientRpc]
    private void HideQuiz1ClientRpc()
    {
        npcQuizUI.gameObject.SetActive(false);
    }

    [ClientRpc]
    private void ShowQuiz2ClientRpc()
    {
        npcQuiz2UI.gameObject.SetActive(true);
    }

    [ClientRpc]
    private void HideQuiz2ClientRpc()
    {
        npcQuiz2UI.gameObject.SetActive(false);
    }

    [ClientRpc]
    private void ShowFinalScoreClientRpc()
    {
        finalScoreUI.gameObject.SetActive(true);
    }

    [ClientRpc]
    private void HideFinalScoreClientRpc()
    {
        finalScoreUI.gameObject.SetActive(false);
    }
    private bool IsQuiz2Opened()
    {
        return npcQuiz2UI.gameObject.activeSelf;
    }

    private bool IsFinalScoreOpened()
    {
        return finalScoreUI.gameObject.activeSelf;
    }
}
