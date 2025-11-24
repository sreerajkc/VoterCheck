using Fusion;
using System;
using TMPro;
using UnityEngine;

public class Officer : NetworkBehaviour, IEnterable, IExitable
{
    [SerializeField] private TextMeshPro IsCheckingText;

    [Networked, OnChangedRender(nameof(OnIsCheckingChanged))]
    public bool IsChecking { get; set; }

    [Networked]
    public VoterInteractionController currentlyCheckingVoter { get; set; }

    [Networked]
    private TickTimer checkingTime { get; set; }

    public override void Render()
    {
        if (checkingTime.IsRunning)
        {
            IsCheckingText.text = $"Checking:{(int)checkingTime.RemainingTime(Runner)}";
        }
        else
        {
            IsCheckingText.text = "Not checking";
        }
    }

    public void Enter(VoterInteractionController voterInteractionController)
    {
        if (!IsChecking)
        {
            IsChecking = true;
            currentlyCheckingVoter = voterInteractionController;
            checkingTime = TickTimer.CreateFromSeconds(Runner, 30);
        }
    }

    public void Exit()
    {

    }

    private void OnIsCheckingChanged()
    {

    }
}
