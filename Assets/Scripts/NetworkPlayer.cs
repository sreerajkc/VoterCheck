using Fusion;
using System;

public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local;
    public event Action onLocalPlayerSpawned;
    public bool IsLocal => Object.HasInputAuthority;

    public override void Spawned()
    {
        if (IsLocal)
        {
            Local = this;
            onLocalPlayerSpawned?.Invoke();
        }

    }
}
