using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class MinimapScript : NetworkBehaviour
{
    private Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
    private void Start()
    {
        AttachCameraToPlayer();
    }

    public void AttachCameraToPlayer()
    {
        //if (!IsOwner && GameModeManager.Instance.GetGameMode() == 1) { return; }
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
}
