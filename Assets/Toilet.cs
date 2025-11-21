using System;
using UnityEngine;

public class Toilet : MonoBehaviour, IExitable, IEnterable
{
    public static event Action<VoterInteractionController> OnVoterEntered;
    public static event Action<VoterInteractionController> OnVoterExit;

    private bool isOccupied = false;
    private VoterInteractionController voterInteractionController;
    public bool IsOccupied => isOccupied;

    private void Update()
    {
        if (isOccupied && Input.GetButtonDown("Cancel"))
        {
            Exit();
        }
    }

    public void Enter(VoterInteractionController voterInteractionController)
    {
        OnVoterEntered?.Invoke(voterInteractionController);

        isOccupied = true;
        this.voterInteractionController = voterInteractionController;
        voterInteractionController.transform.position = new Vector3(transform.position.x, 
            voterInteractionController.transform.position.y,transform.position.z);
        voterInteractionController.OnEnterToilet();
    }

    public void Exit()
    {
        OnVoterExit?.Invoke(voterInteractionController);

        isOccupied = false;
        voterInteractionController.OnExitToilet();
        voterInteractionController = null;
    }
}
