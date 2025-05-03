using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour
{
    public static Player LocalInstance { get; private set; }
    public static event EventHandler OnAnyPlayerSpawned;

    [SerializeField] private List<Vector3> SpawnPositionList;
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
        }

        transform.position = SpawnPositionList[(int)OwnerClientId];

        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);
    }
}
