using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Start()
    {
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Transform lookAtTarget = player.transform.Find("LookAt");
        gameObject.GetComponent<CinemachineFreeLook>().Follow = player;
        gameObject.GetComponent<CinemachineFreeLook>().LookAt = lookAtTarget;
    }
}
