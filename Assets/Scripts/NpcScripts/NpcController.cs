using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField] private Transform checkpoint;
    private NavMeshAgent agent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            agent.SetDestination(checkpoint.position);
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }
}
