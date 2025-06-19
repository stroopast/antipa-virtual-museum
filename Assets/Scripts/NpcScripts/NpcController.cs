using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class NpcController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpoints;
    private NavMeshAgent agent;
    private Animator animator;

    private void Update()
    {
        
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void TriggerGuidedTour()
    {
        gameObject.layer = 7; // Set layer to MovingNpc when player starts the guided tour

        StartCoroutine(WaitForCheckpoint());
    }

    //private IEnumerator WaitForCheckpoint()
    //{
    //    for (int i = 0; i < checkpoints.Length; i++)
    //    {
    //        agent.SetDestination(checkpoints[i].position);

    //        animator.SetBool("isWalking", true);

    //        while (agent.pathPending || agent.remainingDistance > 0.2f)
    //        {
    //            yield return null;
    //        }

    //        animator.SetBool("isWalking", false);
    //        yield return new WaitForSeconds(1f);

    //        if (CheckPoistionEquivalence(checkpoints[10]))
    //        {
    //            gameObject.layer = 8; // Set layer back to IdleNpc when Npc finish the guided tour
    //        }
    //    }
    //}

    private IEnumerator WaitForCheckpoint()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            agent.SetDestination(checkpoints[i].position);
            animator.SetBool("isWalking", true);

            while (agent.pathPending || agent.remainingDistance > 0.2f)
            {
                yield return null;
            }

            animator.SetBool("isWalking", false);

            Quaternion turnAroundRotation = Quaternion.Euler(0f, transform.eulerAngles.y + 180f, 0f);
            yield return StartCoroutine(SmoothRotate(turnAroundRotation));

            yield return new WaitForSeconds(1f);

            //Smoothly rotate to face the next checkpoint (if not last)
            if (i + 1 < checkpoints.Length)
            {
                Vector3 directionToNext = (checkpoints[i + 1].position - transform.position).normalized;
                Quaternion faceNextCheckpoint = Quaternion.LookRotation(new Vector3(directionToNext.x, 0f, directionToNext.z));
                yield return StartCoroutine(SmoothRotate(faceNextCheckpoint));
            }

            if (CheckPoistionEquivalence(checkpoints[10]))
            {
                gameObject.layer = 8; // Set layer back to IdleNpc when tour ends
            }
        }
    }

    private IEnumerator SmoothRotate(Quaternion targetRotation, float duration = 0.5f)
    {
        Quaternion startRotation = transform.rotation;
        float time = 0f;

        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    private bool CheckPoistionEquivalence(Transform pos)
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
}
