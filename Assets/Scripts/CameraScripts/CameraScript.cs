using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private void Start()
    {
        AttachCameraToPlayer();
    }

    private void AttachCameraToPlayer()
    {
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Transform lookAtTarget = player.transform.Find("LookAt");
        gameObject.GetComponent<CinemachineFreeLook>().Follow = player;
        gameObject.GetComponent<CinemachineFreeLook>().LookAt = lookAtTarget;
        //gameObject.GetComponent<CinemachineFreeLook>(). = lookAtTarget.GetComponent<Transform>();
    }
}
