using Fusion;
using System;
using TMPro;
using Unity.Jobs;
using UnityEngine;

public class Toilet : NetworkBehaviour, IExitable, IEnterable
{
    [SerializeField] private TextMeshPro isOccupiedText;

    public static event Action OnVoterEntered;
    public static event Action OnVoterExit;

    [Networked]
    public bool IsOccupied { get; set; }

    [Networked, OnChangedRender(nameof(OnVoterInteractionControllerChanged))]
    public VoterInteractionController voterInteractionController { get; set; }

    [Networked]
    public VoterInteractionController previousVoterInteractionController { get; set; }



    public bool CanEnter { get; }

    public override void Render()
    {
        isOccupiedText.text = IsOccupied ? "Occupied" : "Unoccupied";
    }

    public void Enter(VoterInteractionController voterInteractionController)
    {
        IsOccupied = true;
        this.voterInteractionController = voterInteractionController;
        previousVoterInteractionController = voterInteractionController;

        voterInteractionController.transform.position = new Vector3(transform.position.x,
                    voterInteractionController.transform.position.y, transform.position.z);
        voterInteractionController.OnEnterToilet();
    }

    public void Exit()
    {
        IsOccupied = false;

        voterInteractionController.OnExitToilet();
        voterInteractionController = null;
    }

    private void OnVoterInteractionControllerChanged()
    {
        if (voterInteractionController != null)
        {
            if (voterInteractionController.HasInputAuthority)
            {
                OnVoterEntered?.Invoke();
            }
        }
        else if (previousVoterInteractionController != null)
        {
            if (previousVoterInteractionController.HasInputAuthority)
            {
                OnVoterExit?.Invoke();
                previousVoterInteractionController = null;
            }
        }
    }

}
