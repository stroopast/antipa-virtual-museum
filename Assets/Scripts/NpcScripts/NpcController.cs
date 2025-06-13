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
        if(CheckPoistionEquivalence(checkpoints[9]))
        {
            gameObject.layer = 8; // Set layer back to IdleNpc when Npc finish the guided tour
        }
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
            yield return StartCoroutine(RotateSmoothly(180f));
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator RotateSmoothly(float angleDegrees, float duration = 1f)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, angleDegrees, 0f);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
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
