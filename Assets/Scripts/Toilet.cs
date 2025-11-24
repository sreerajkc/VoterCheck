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

    [Networked, OnChangedRender(nameof(OnIsOccupiedChange))]
    public bool IsOccupied { get; set; }

    [Networked]
    public VoterInteractionController voterInteractionController { get; set; }
    public bool CanEnter { get; }

    public override void Render()
    {
        isOccupiedText.text = IsOccupied ? "Occupied" : "Unoccupied";
    }

    public void Enter(VoterInteractionController voterInteractionController)
    {
        if (!IsOccupied)
        {
            this.voterInteractionController = voterInteractionController;

            voterInteractionController.transform.position = new Vector3(transform.position.x,
                        voterInteractionController.transform.position.y, transform.position.z + 10);
            voterInteractionController.DisableInput();

            IsOccupied = true;
        }
    }

    public void Exit()
    {
        if (IsOccupied)
        {
            IsOccupied = false;
            voterInteractionController.EnableInput();
        }
    }

    private void OnIsOccupiedChange()
    {
        if (IsOccupied)
        {
            if (voterInteractionController != null && voterInteractionController.HasInputAuthority)
            {
                OnVoterEntered?.Invoke();
            }
        }
        else
        {
            if (voterInteractionController != null && voterInteractionController.HasInputAuthority)
            {
                OnVoterExit?.Invoke();
                voterInteractionController = null;
            }
        }
    }

}
