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
    [SerializeField] private GameObject timeExpiredUI;
    [SerializeField] private GameObject npcRoomDialogue;
    [SerializeField] private TextMeshProUGUI countdownTimer;

    private NavMeshAgent agent;
    private Animator animator;
    private int currentCheckpoint = 0;

    public float startTime = 40f;
    private float timeLeft;
    private bool isCountingEnabled = false;

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

        StartCountdownTimer();
    }

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void StartCountdownTimer()
    {
        if (isCountingEnabled)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                countdownTimer.text = timeLeft.ToString("0.0");
            }
            else
            {
                timeLeft = startTime;
            }
        }
    }

    private void ActivateTimer()
    {
        timeLeft = startTime;
        countdownTimer.gameObject.SetActive(true);
        isCountingEnabled = true;
    }

    private void DisableTimer()
    {
        countdownTimer.gameObject.SetActive(false);
        isCountingEnabled = false;
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

            if(i == 10 || i == 9)
            {
                animator.SetBool("isTalking", false);
            }
            else
            {
                animator.SetBool("isTalking", true);
            }

            if (i == 9)
            {
                // for multiplayer
                if (GameModeManager.Instance.GetGameMode() == 1)
                {
                    ActivateTimerClientRpc();
                    ShowQuiz1ClientRpc();
                    yield return new WaitForSeconds(startTime); // timer for both tests
                    HideQuiz1ClientRpc();
                    HideQuiz2ClientRpc();

                    if (PlayerScore.Instance.finishedInTime == false)
                    {
                        ShowFinalScoreClientRpc();
                    }
                    HelperFunctions.LockCursor();
                    DisableTimerClientRpc();
                    yield return new WaitForSeconds(5f);
                    HideFinalScoreClientRpc();
                   
                    if (PlayerScore.Instance.GetPlayerScore() > 1)
                    {
                        UnlockSpecialAchievementClientRpc();
                    }
                }
                // for singleplayer
                else
                {
                    ActivateTimer();
                    npcQuizUI.gameObject.SetActive(true);
                    yield return new WaitForSeconds(startTime);
                    npcQuizUI.gameObject.SetActive(false);
                    npcQuiz2UI.gameObject.SetActive(false);

                    if (PlayerScore.Instance.finishedInTime == false)
                    {
                        timeExpiredUI.gameObject.SetActive(true);
                    }
                    HelperFunctions.LockCursor();
                    DisableTimer();
                    yield return new WaitForSeconds(5f);
                    timeExpiredUI.gameObject.SetActive(false);

                    if (PlayerScore.Instance.GetPlayerScore() > 1)
                    {
                        AchievementManager.Instance.UnlockAchievement("Tur");
                    }
                }
            }
            else if(i == 10)
            {
                animator.SetBool("isTalking", false);
            }
            else
            {
                if (GameModeManager.Instance.GetGameMode() == 1)
                {
                    TriggerRoomDialogue(i);
                }
                else
                {
                    TriggerRoomDialogue(i);
                }
                yield return new WaitForSeconds(10f);
                npcRoomDialogue.SetActive(false);
                animator.SetBool("isTalking", false);
            }
            


            if (CheckPositionEquivalence(checkpoints[10]))
            {
                StartCoroutine(RotateSmoothly(180f, 1.5f)); // Rotate over 1.5 seconds
                gameObject.layer = 8; // Set layer back to IdleNpc when tour ends
            }
        }
    }

    [ClientRpc]
    private void TriggerRoomDialogueClientRpc(int room)
    {
        npcRoomDialogue.SetActive(true);
        TextMeshProUGUI dialogueText = npcRoomDialogue.GetComponentInChildren<TextMeshProUGUI>();
        switch (room)
        {
            case 0:
                dialogueText.text = "";
                break;
            case 1:
                dialogueText.text = "";
                break;
            case 2:
                dialogueText.text = "";
                break;
            case 3:
                dialogueText.text = "";
                break;
            case 4:
                dialogueText.text = "";
                break;
            case 5:
                dialogueText.text = "";
                break;
            case 6:
                dialogueText.text = "";
                break;
            case 7:
                dialogueText.text = "";
                break;
            case 8:
                dialogueText.text = "";
                break;
        }
    }

    private void TriggerRoomDialogue(int room)
    {
        npcRoomDialogue.SetActive(true);
        TextMeshProUGUI dialogueText = npcRoomDialogue.GetComponentInChildren<TextMeshProUGUI>();
        switch (room)
        {
            case 0:
                dialogueText.text = "";
                break;
            case 1:
                dialogueText.text = "";
                break;
            case 2:
                dialogueText.text = "";
                break;
            case 3:
                dialogueText.text = "";
                break;
            case 4:
                dialogueText.text = "";
                break;
            case 5:
                dialogueText.text = "";
                break;
            case 6:
                dialogueText.text = "";
                break;
            case 7:
                dialogueText.text = "";
                break;
            case 8:
                dialogueText.text = "";
                break;
            default:
                break;
        }
    }

    private IEnumerator RotateSmoothly(float angle, float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);

        float time = 0f;
        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endRotation; // ensure it ends exactly at target rotation
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
    private void HideQuiz2ClientRpc()
    {
        npcQuiz2UI.gameObject.SetActive(false);
    }

    [ClientRpc]
    private void ShowFinalScoreClientRpc()
    {
        timeExpiredUI.gameObject.SetActive(true);
    }

    [ClientRpc]
    private void HideFinalScoreClientRpc()
    {
        timeExpiredUI.gameObject.SetActive(false);
    }

    [ClientRpc]
    private void ActivateTimerClientRpc()
    {
        timeLeft = startTime;
        countdownTimer.gameObject.SetActive(true);
        isCountingEnabled = true;
    }

    [ClientRpc]
    private void DisableTimerClientRpc()
    {
        countdownTimer.gameObject.SetActive(false);
        isCountingEnabled = false;
    }

    [ClientRpc]
    private void UnlockSpecialAchievementClientRpc()
    {
        AchievementManager.Instance.UnlockAchievement("Tur");
    }

}
