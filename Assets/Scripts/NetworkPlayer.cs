using Fusion;
using System;
using TMPro;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local;
    public event Action onLocalPlayerSpawned;
    public bool IsLocal => Object.HasInputAuthority;

    [SerializeField] private TextMeshPro nameText;

    public override void Spawned()
    {
        if (IsLocal)
        {
            Local = this;
            onLocalPlayerSpawned?.Invoke();
        }

        nameText.text = Object.InputAuthority.ToString();
    }
}
