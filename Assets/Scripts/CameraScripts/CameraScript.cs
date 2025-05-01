using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class CameraScript : NetworkBehaviour
{
    private CinemachineFreeLook FreeLookCamera;
    private void Start()
    {
        AttachCameraToPlayer();
    }

    public void AttachCameraToPlayer()
    {
        if(!IsOwner) { return; }
        FreeLookCamera = GameObject.FindWithTag("FreeLookCamera").GetComponent<CinemachineFreeLook>();
        FreeLookCamera.Follow = gameObject.GetComponent<Transform>();
        FreeLookCamera.LookAt = gameObject.GetComponent<Transform>().Find("LookAt");
        //Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //Transform lookAtTarget = player.transform.Find("LookAt");
        //gameObject.GetComponent<CinemachineFreeLook>().Follow = player;
        //gameObject.GetComponent<CinemachineFreeLook>().LookAt = lookAtTarget;
    }

}
