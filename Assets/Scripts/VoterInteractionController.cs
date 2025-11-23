using DG.Tweening;
using Fusion;
using System;
using UnityEngine;

public class VoterInteractionController : NetworkBehaviour
{
    private IEnterable currentEnterable { get; set; }

    private IInputActivater inputActivater;

    private void Start()
    {
        inputActivater = GetComponent<IInputActivater>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        if (other.TryGetComponent<IEnterable>(out IEnterable enterable))
        {
            currentEnterable = enterable;
        }
    }

    public void OnEnterToilet()
    {
        inputActivater.DisableInput();
        transform.forward = Vector3.forward;
    }

    public void OnExitToilet()
    {
        DOVirtual.DelayedCall(1, inputActivater.EnableInput);
    }

    public void EnterCurrentEnterable()
    {
        if (currentEnterable != null)
        {
            currentEnterable.Enter(this);
        }
    }
    
    public void ExitCurrentExitable()
    {
        if (currentEnterable is IExitable currentExitable)
        {
            currentExitable.Exit();
            currentEnterable =null;
        }
    }
}
